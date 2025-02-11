using System.Linq;
using VisualNovelEngine.Engine.EngineEditor.Interface;

namespace VisualNovelEngine.Engine.EngineEditor.Component.Command
{
    /// <summary>
    /// 
    /// </summary>
    public class ShowInspectorCommand : ICommand
    {
        internal Editor Editor { get; set; }
        private readonly InspectorWindow Window;
        internal int EnabledRowComponentCount;
        internal int XPosition { get; set; }
        internal int YPosition { get; set; }

        public ShowInspectorCommand(Editor editor, int enabledRowComponentCount, int xPos, int yPos)
        {
            Editor = editor;
            EnabledRowComponentCount = enabledRowComponentCount;
            XPosition = xPos;
            YPosition = yPos;
            Window = new InspectorWindow(editor, xPos, yPos, EnabledRowComponentCount);
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        public void Execute()
        {
            //Search for the first selected component and set it as the active component.
            //Convert the selected component to a component.
            Window.SetActiveComponent((Component)Editor.ActiveScene.ComponentList.FirstOrDefault(x =>
            {
                Component component = (Component)x;
                return component.IsSelected;
            }));
            Window.Active = true;
            Editor.ActiveScene.InspectorWindow = Window;
        }
    }
}
using System.Linq;
using Raylib_cs;
using TemplateGame.Interface;
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
        private IEvent? Action { get; set; } = null;
        internal int EnabledRowComponentCount;
        internal int XPosition { get; set; }
        internal int YPosition { get; set; }

        public ShowInspectorCommand(Editor editor, int enabledRowComponentCount)
        {
            Editor = editor;
            EnabledRowComponentCount = enabledRowComponentCount;
            XPosition = Raylib.GetScreenWidth() / 2 - Editor.InspectorWindowWidth / 2;
            YPosition = Raylib.GetScreenHeight() / 2 - Editor.InspectorWindowHeight / 2;
            Window = new InspectorWindow(editor, XPosition, YPosition, EnabledRowComponentCount);
        }

        public ShowInspectorCommand(Editor editor, IEvent eventData, int enabledRowComponentCount)
        {
            Editor = editor;
            Action = eventData;
            EnabledRowComponentCount = enabledRowComponentCount;
            XPosition = Raylib.GetScreenWidth() / 2 - Editor.InspectorWindowWidth / 2;
            YPosition = Raylib.GetScreenHeight() / 2 - Editor.InspectorWindowHeight / 2;
            Window = new InspectorWindow(editor, XPosition, YPosition, EnabledRowComponentCount);
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        public void Execute()
        {
            //Set event to active event in the inspector.
            if (Window != null)
            {
                Window.Active = true;
                Window.SetActiveComponent(Action);
                Editor.ActiveScene.InspectorWindow = Window;
                return;
            }
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
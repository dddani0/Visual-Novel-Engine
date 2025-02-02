using VisualNovelEngine.Engine.EngineEditor.Interface;

namespace VisualNovelEngine.Engine.EngineEditor.Component.Command
{
    /// <summary>
    /// 
    /// </summary>
    public class ShowInspectorCommand : ICommand
    {
        Editor Editor { get; set; }
        private readonly InspectorWindow Window;

        public ShowInspectorCommand(Editor editor, int enabledRowComponentCount, int xPos, int yPos, IComponent[] components)
        {
            Editor = editor;
            Window = new InspectorWindow(editor, xPos, yPos, enabledRowComponentCount, components) { Active = true };
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        public void Execute()
        {
            Editor.ActiveScene.InspectorWindow = Window;
        }
    }
}
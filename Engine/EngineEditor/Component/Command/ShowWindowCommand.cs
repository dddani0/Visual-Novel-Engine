using VisualNovelEngine.Engine.EngineEditor.Interface;

namespace VisualNovelEngine.Engine.EngineEditor.Component.Command
{
    /// <summary>
    /// 
    /// </summary>
    public class ShowWindowCommand : ICommand
    {
        private readonly InspectorWindow Window;

        public ShowWindowCommand(Editor editor, int enabledRowComponentCount, int xPos, int yPos, IComponent[] components)
        {
            Window = new InspectorWindow(editor, xPos, yPos, editor.ComponentWidth, editor.ComponentHeight, editor.ComponentBorderWidth, enabledRowComponentCount, editor.BaseColor, editor.BorderColor, components);
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        public void Execute()
        {
            Window.Active = true;
        }
    }
}
using VisualNovelEngine.Engine.EngineEditor.Interface;

namespace VisualNovelEngine.Engine.EngineEditor.Component.Command
{
    /// <summary>
    /// Represents an empty command.
    /// </summary>
    public class DeleteComponentCommand : ICommand
    {
        private readonly Editor Editor;
        private readonly Component Component;

        public DeleteComponentCommand(Editor editor, Component component)
        {
            Editor = editor;
            Component = component;
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        public void Execute()
        {
            Editor.ComponentList.Remove(Component);
        }
    }
}
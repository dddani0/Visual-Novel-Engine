using VisualNovelEngine.Engine.Editor.Interface;

namespace VisualNovelEngine.Engine.Editor.Component.Command
{
    /// <summary>
    /// Deletes a single selected component
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
            Editor.ActiveScene.ComponentList.Remove(Component);
        }
    }
}
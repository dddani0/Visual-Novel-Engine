using VisualNovelEngine.Engine.Editor.Interface;

namespace VisualNovelEngine.Engine.Editor.Component.Command
{
    /// <summary>
    /// The command to close the error window.
    /// </summary>
    class CloseErrorWindowCommand : ICommand
    {
        /// <summary>
        /// The editor.
        /// </summary>
        private readonly Editor Editor;
        /// <summary>
        /// Creates a new instance of <see cref="CloseErrorWindowCommand"/>.
        /// </summary>
        /// <param name="editor"></param>
        public CloseErrorWindowCommand(Editor editor)
        {
            Editor = editor;
        }
        /// <summary>
        /// Executes the command.
        /// </summary>
        public void Execute()
        {
            Editor.ErrorWindow = null;
        }
    }
}
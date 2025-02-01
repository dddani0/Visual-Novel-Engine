using VisualNovelEngine.Engine.EngineEditor.Interface;

namespace VisualNovelEngine.Engine.EngineEditor.Component.Command
{
    /// <summary>
    /// Represents a command that saves the editor file.
    /// </summary>
    public class SaveEditorFileCommand : ICommand
    {
        private readonly Editor Editor;

        public SaveEditorFileCommand(Editor editor)
        {
            Editor = editor;
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        public void Execute()
        {
            Editor.Save();
        }
    }
}
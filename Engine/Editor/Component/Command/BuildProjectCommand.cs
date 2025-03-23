using VisualNovelEngine.Engine.Editor.Interface;

namespace VisualNovelEngine.Engine.Editor.Component.Command
{
    /// <summary>
    /// Build the current project.
    /// </summary>
    public class BuildProjectCommand : ICommand
    {
        /// <summary>
        /// The editor.
        /// </summary>
        Editor Editor { get; set; }
        /// <summary>
        /// Creates a new instance of <see cref="BuildProjectCommand"/>.
        /// </summary>
        /// <param name="editor"></param>
        public BuildProjectCommand(Editor editor)
        {
            Editor = editor;
        }
        /// <summary>
        /// Executes the command.
        /// </summary>
        public void Execute()
        {
            Editor.Build();
        }
    }
}
using VisualNovelEngine.Engine.Editor.Interface;

namespace VisualNovelEngine.Engine.Editor.Component
{
    /// <summary>
    /// Represents a command to change the scene.
    /// </summary>
    public class ChangeSceneCommand : ICommand
    {
        /// <summary>
        /// The editor.
        /// </summary>
        private Editor Editor { get; set; }
        /// <summary>
        /// The scene button.
        /// </summary>
        internal Button SceneButton { get; set; }
        /// <summary>
        /// Creates a new instance of <see cref="ChangeSceneCommand"/>.
        /// </summary>
        /// <param name="editor"></param>
        public ChangeSceneCommand(Editor editor)
        {
            Editor = editor;
        }
        /// <summary>
        /// Executes the command.
        /// </summary>
        public void Execute()
        {
            if (SceneButton.Text == Editor.ActiveScene.Name) return;
            Scene newActiveSceen = Editor.SceneList.First(x => SceneButton.Text.Equals(x.Name));
            Editor.ActiveScene = newActiveSceen;
        }
    }
}
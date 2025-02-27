using VisualNovelEngine.Engine.EngineEditor.Interface;

namespace VisualNovelEngine.Engine.EngineEditor.Component
{
    public class ChangeSceneCommand : ICommand
    {
        private Editor Editor { get; set; }
        internal Button SceneButton { get; set; }
        public ChangeSceneCommand(Editor editor)
        {
            Editor = editor;
        }
        public void Execute()
        {
            if (SceneButton.Text == Editor.ActiveScene.Name) return;
            Scene newActiveSceen = Editor.SceneList.First(x => SceneButton.Text.Equals(x.Name));
            Editor.ActiveScene = newActiveSceen;
        }
    }
}
using VisualNovelEngine.Engine.EngineEditor.Component;
using VisualNovelEngine.Engine.EngineEditor.Interface;

namespace EngineEditor.Component.Command
{
    public class DeleteSceneCommand : ICommand
    {
        Editor Editor { get; set; }
        public DeleteSceneCommand(Editor editor)
        {
            Editor = editor;
        }

        public void Execute()
        {
            if (Editor.SceneList.Count < 2) return;
            Scene selectedScene = Editor.ActiveScene;
            Button sceneButton = Editor.SceneBar.ButtonComponentList.First(sceneButton => sceneButton.Text.Contains(selectedScene.Name));
            //Remove scene from scrollbar
            Editor.SceneBar.ButtonComponentList.Remove(sceneButton);
            Editor.SceneBar.Scrollbar.Components.Remove(sceneButton);
            //delete scene from scene list
            Editor.SceneList.Remove(selectedScene);
            //Remove miniwindow from the rendering list
            //HOGYAN KELL?
            //Change scene to the first scene
            Editor.ActiveScene = Editor.SceneList[0];
            //Update the scenebar
            Editor.SceneBar.UpdateComponentPosition();
        }
    }
}
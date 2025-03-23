using VisualNovelEngine.Engine.Editor.Component;
using VisualNovelEngine.Engine.Editor.Interface;

namespace EngineEditor.Component.Command
{
    /// <summary>
    /// Command to delete a scene.
    /// </summary>
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
            foreach (MiniWindow miniWindow in Editor.MiniWindow)
            {
                //check if the miniwindow's componentlist's first element is a label
                if (miniWindow.ComponentList[0] is Label label)
                {
                    //check if the label's text is the same as the scene's name
                    if (label.Text.Contains("configuration"))
                    {
                        Editor.MiniWindow.Remove(miniWindow);
                        break;
                    }
                }
            }
            //Error window delete
            Editor.ErrorWindow = null;
            //Change scene to the first scene
            Editor.ActiveScene = Editor.SceneList[0];
            //Update the scenebar
            Editor.SceneBar.UpdateComponentPosition();
        }
    }
}
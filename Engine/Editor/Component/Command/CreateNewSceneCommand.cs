using VisualNovelEngine.Engine.Editor.Component.Command;
using VisualNovelEngine.Engine.Editor.Interface;

namespace VisualNovelEngine.Engine.Editor.Component
{
    /// <summary>
    /// Create a new empty scene
    /// </summary>
    public class CreateNewSceneCommand : ICommand
    {
        private Editor Editor { get; set; }
        public CreateNewSceneCommand(Editor editor)
        {
            Editor = editor;
        }
        public void Execute()
        {
            //Create new scene
            Scene newScene = new Scene(Editor, $"({Editor.SceneBar.ButtonComponentList.Count})New Scene", null, null);
            //Jump to new scene
            Editor.ActiveScene = newScene;
            Editor.SceneList.Add(newScene);
            //Add new scene to the scene bar
            Button newSceneButton = new(Editor, Editor.SceneBar.ButtonComponentList.Count * Editor.ComponentWidth, 0, newScene.Name, false, Editor.ComponentWidth, Editor.ComponentHeight, Editor.ComponentBorderWidth, Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, new ChangeSceneCommand(Editor), Button.ButtonType.Trigger);
            ChangeSceneCommand changeSceneCommand = (ChangeSceneCommand)newSceneButton.Command;
            changeSceneCommand.SceneButton = newSceneButton;
            Editor.SceneBar.ButtonComponentList.Insert(Editor.SceneBar.ButtonComponentList.Count - 1, newSceneButton);
            Editor.SceneBar.Scrollbar.Components.Insert(Editor.SceneBar.Scrollbar.Components.Count - 1, newSceneButton);
            //Update scene bar
            Editor.SceneBar.UpdateComponentPosition();
        }
    }
}
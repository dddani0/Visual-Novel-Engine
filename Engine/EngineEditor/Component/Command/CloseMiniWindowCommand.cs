using TemplateGame.Component;
using VisualNovelEngine.Engine.EngineEditor.Interface;

namespace VisualNovelEngine.Engine.EngineEditor.Component.Command
{
    public class CloseMiniWindowCommand : ICommand
    {
        private readonly Editor Editor;
        private MiniWindow MiniWindow { get; set; }
        /// <summary>
        /// Constructor for toolbar associated purposes
        /// </summary>
        /// <param name="editor"></param>
        /// <param name="buttonName"></param>
        /// <param name="buttons"></param>
        public CloseMiniWindowCommand(Editor editor, MiniWindow miniWindow)
        {
            Editor = editor;
            MiniWindow = miniWindow;
        }

        public void Execute()
        {
            //Save variables
            if (MiniWindow.HasVariableComponent is true)
            {
                for (int i = 0; i < Editor.GameVariables.Count; i++)
                {
                    Variable variable = Editor.GameVariables[i];
                    TextField nameTextField = (TextField)MiniWindow.VariableComponentList[i * 3];
                    TextField valueTextField = (TextField)MiniWindow.VariableComponentList[i * 3 + 1];
                    DropDown typeDropDown = (DropDown)MiniWindow.VariableComponentList[i * 3 + 2];
                    variable.Name = nameTextField.Text;
                    variable.Value = valueTextField.Text;
                    variable.Type = typeDropDown.Button.VariableType;
                }
            }
            if (MiniWindow.HasSceneComponent)
            {
                Scene activeScene = Editor.ActiveScene;
                String newSceneName = ((TextField)MiniWindow.ComponentList[2]).Text;
                //Search through the editor's scenebar's button list
                Editor.SceneBar.ButtonComponentList.First(x => x.Text == activeScene.Name).Text = newSceneName;
                //change scene name in the scene list
                Editor.SceneList.First(x => x.Name == activeScene.Name).Name = newSceneName;
                //change active scene name
                activeScene.Name = newSceneName;
            }
            Editor.MiniWindow.Remove(MiniWindow);
        }
    }
}
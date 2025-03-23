using Raylib_cs;
using VisualNovelEngine.Engine.Game.Component;
using VisualNovelEngine.Engine.Editor.Interface;

namespace VisualNovelEngine.Engine.Editor.Component.Command
{
    /// <summary>
    /// Command to close a mini window
    /// </summary>
    public class CloseMiniWindowCommand : ICommand
    {
        /// <summary>
        /// The editor.
        /// </summary>
        private readonly Editor Editor;
        /// <summary>
        /// The mini window.
        /// </summary>
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
        /// <summary>
        /// Executes the command.
        /// </summary>
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
                //Save background type
                activeScene.BackgroundOption = ((DropDown)MiniWindow.ComponentList[3]).Button.SceneBackgroundOption;
                //Save background options cimlet
                if (activeScene.BackgroundOption == VisualNovelEngine.Engine.Game.Component.Scene.BackgroundOption.SolidColor)
                {
                    //activeScene.BackgroundColor = (MiniWindow.ComponentList[4]).Button.Color;
                    TextField colorTextField = (TextField)MiniWindow.ComponentList[5];
                    activeScene.BackgroundColor = new Color()
                    {
                        R = byte.Parse(colorTextField.Text.Split(',')[0]),
                        G = byte.Parse(colorTextField.Text.Split(',')[1]),
                        B = byte.Parse(colorTextField.Text.Split(',')[2]),
                        A = 255
                    };
                }
                else if (activeScene.BackgroundOption == VisualNovelEngine.Engine.Game.Component.Scene.BackgroundOption.Image)
                {
                    TextField imageTextField = (TextField)MiniWindow.ComponentList[5];
                    activeScene.BackgroundImage = new Sprite(imageTextField.Text).ImageTexture;
                }
                else if (activeScene.BackgroundOption == VisualNovelEngine.Engine.Game.Component.Scene.BackgroundOption.GradientVertical ||
                         activeScene.BackgroundOption == VisualNovelEngine.Engine.Game.Component.Scene.BackgroundOption.GradientHorizontal)
                {
                    TextField color1TextField = (TextField)MiniWindow.ComponentList[5];
                    TextField color2TextField = (TextField)MiniWindow.ComponentList[6];
                    activeScene.BackgroundGradientColor =
                    [
                        new Color()
                        {
                            R = byte.Parse(color1TextField.Text.Split(',')[0]),
                            G = byte.Parse(color1TextField.Text.Split(',')[1]),
                            B = byte.Parse(color1TextField.Text.Split(',')[2]),
                            A = 255
                        },
                        new Color()
                        {
                            R = byte.Parse(color2TextField.Text.Split(',')[0]),
                            G = byte.Parse(color2TextField.Text.Split(',')[1]),
                            B = byte.Parse(color2TextField.Text.Split(',')[2]),
                            A = 255
                        }
                    ];
                }
                //remove the added buttons
                if (Editor.ActiveScene.BackgroundOption == VisualNovelEngine.Engine.Game.Component.Scene.BackgroundOption.SolidColor ||
                    Editor.ActiveScene.BackgroundOption == VisualNovelEngine.Engine.Game.Component.Scene.BackgroundOption.Image)
                {
                    MiniWindow.ComponentList.RemoveRange(4, 2);
                }
                else
                {
                    MiniWindow.ComponentList.RemoveRange(4, 3);
                }
            }
            Editor.MiniWindow.Remove(MiniWindow);
        }
    }
}
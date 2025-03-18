using VisualNovelEngine.Engine.EngineEditor.Component.Command;
using VisualNovelEngine.Engine.EngineEditor.Interface;

namespace VisualNovelEngine.Engine.EngineEditor.Component
{
    public class SelectDropDownButtonCommand : ICommand
    {
        Editor Editor { get; set; }
        internal DropDown DropDown { get; set; }
        private Button Button { get; set; }
        public SelectDropDownButtonCommand(Editor editor, DropDown dropDown, Button button)
        {
            Editor = editor;
            DropDown = dropDown;
            Button = button;
        }
        public void Execute()
        {
            if (Button.Component != null)
            {
                DropDown.Button.Component = Button.Component;
                DropDown.Button.Text = Button.Text;
                DropDown.Button.Command.Execute();
            }
            if (Button.PositionType != null)
            {
                DropDown.Button.PositionType = Button.PositionType;
                DropDown.Button.Text = Button.Text;
            }
            if (Button.SceneBackgroundOption != null)
            {
                DropDown.Button.SceneBackgroundOption = Button.SceneBackgroundOption;
                DropDown.Button.Text = Button.Text;
            }
            if (Button.VariableType != null)
            {
                DropDown.Button.VariableType = Button.VariableType;
                DropDown.Button.Text = Button.Text;
            }
            if (Button.Action != null)
            {
                DropDown.Button.Action = Button.Action;
                DropDown.Button.Text = Button.Text;
            }
            //Specific dropdown for the scene background option
            if (Button.Text.ToLower().Contains("solid")
            || Button.Text.ToLower().Contains("image")
            || Button.Text.ToLower().Contains("gradi"))
            {
                DropDown.Button.SceneBackgroundOption = Button.SceneBackgroundOption;
                DropDown.Button.Text = Button.Text;
                foreach (var item in Editor.MiniWindow)
                {
                    if (item.HasSceneComponent)
                    {
                        //remove attribute
                        if (Editor.ActiveScene.BackgroundOption == TemplateGame.Component.Scene.BackgroundOption.SolidColor ||
                            Editor.ActiveScene.BackgroundOption == TemplateGame.Component.Scene.BackgroundOption.Image)
                        {
                            item.ComponentList.RemoveRange(4, 2);
                        }
                        else
                        {
                            item.ComponentList.RemoveRange(4, 3);
                        }
                        Editor.ActiveScene.BackgroundOption = Button.SceneBackgroundOption;
                        item.FetchActiveSceneAttributes();
                    }
                }
            }

            DropDown.IsSelected = false;
        }
    }
}
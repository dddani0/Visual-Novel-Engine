using VisualNovelEngine.Engine.EngineEditor.Component.Command;
using VisualNovelEngine.Engine.EngineEditor.Interface;

namespace VisualNovelEngine.Engine.EngineEditor.Component
{
    public class SelectDropDownButtonCommand : ICommand
    {
        internal DropDown DropDown { get; set; }
        private Button Button { get; set; }
        public SelectDropDownButtonCommand(DropDown dropDown, Button button)
        {
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
            DropDown.IsSelected = false;
        }
    }
}
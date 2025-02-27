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
            //EBBŐL MÉG KÉSÖBB BAJ LEHET?!
            DropDown.Button.Component = Button.Component;
            DropDown.Button.Text = Button.Text;
            DropDown.Button.Command.Execute();
            DropDown.IsSelected = false;
        }
    }
}
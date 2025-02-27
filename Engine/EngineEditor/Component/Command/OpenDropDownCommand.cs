using VisualNovelEngine.Engine.EngineEditor.Component.Command;
using VisualNovelEngine.Engine.EngineEditor.Interface;

namespace VisualNovelEngine.Engine.EngineEditor.Component
{
    class OpenDropDownCommand : ICommand
    {
        private DropDown DropDown { get; set; }
        internal ShowSideWindowCommand ShowSideWindowCommand { get; set; }
        public OpenDropDownCommand(Editor editor, DropDown dropDown)
        {
            DropDown = dropDown;
            ShowSideWindowCommand = new(editor, DropDown.Button, null);
        }
        public void Execute()
        {
            DropDown.UpdateComponentList();
            ShowSideWindowCommand.Buttons = [.. DropDown.FilteredButtonList];
            ShowSideWindowCommand.DependentButton = DropDown.Button;
            ShowSideWindowCommand.Execute();
        }
    }
}
namespace EngineComponents.Actions
{
    class TextBoxDisableAction : IEvent
    {
        readonly TextBox TextBox;
        public TextBoxDisableAction(TextBox textbox)
        {
            TextBox = textbox;
        }

        public void PerformEvent()
        {
            if (TextBox.IsFinished) return; //Only disable active textbox!
            if (TextBox.IsDisabled()) return; //already disabled
            TextBox.ToggleEnability();
        }
    }
}

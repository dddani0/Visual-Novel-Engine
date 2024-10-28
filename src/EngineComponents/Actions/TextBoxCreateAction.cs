namespace EngineComponents.Actions
{
    class TextBoxCreateAction : IEvent
    {
        readonly TextBox TextBox;
        public TextBoxCreateAction(TextBox textbox)
        {
            TextBox = textbox;
        }

        public void PerformEvent() => TextBox.ToggleEnability();
    }
}

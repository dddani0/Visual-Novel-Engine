using EngineComponents.Interfaces;

namespace EngineComponents.Actions
{
    /// <summary>
    /// Initializes a new textbox.
    /// </summary>
    class TextBoxCreateAction : IEvent, IButtonEvent
    {
        readonly TextBox TextBox;
        public TextBoxCreateAction(TextBox textbox)
        {
            TextBox = textbox;
        }
        public void PerformEvent()
        {
            if (TextBox.IsDisabled())
            {
                TextBox.ToggleEnability();
            }
            TextBox.WriteToScreen();
        }
    }
}

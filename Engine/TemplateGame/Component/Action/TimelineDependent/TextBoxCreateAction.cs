using TemplateGame.Interface;

namespace TemplateGame.Component.Action.TimelineDependent
{
    /// <summary>
    /// Initializes a new textbox.
    /// </summary>
    class TextBoxCreateAction : IAction, IButtonEvent
    {
        internal TextBox TextBox;
        public TextBoxCreateAction(TextBox textbox)
        {
            TextBox = textbox;
        }
        public void PerformAction()
        {
            if (TextBox.IsDisabled())
            {
                TextBox.ToggleEnability();
            }
            TextBox.Render();
        }
    }
}

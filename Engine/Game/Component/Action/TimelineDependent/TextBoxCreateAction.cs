using VisualNovelEngine.Engine.Game.Interface;

namespace VisualNovelEngine.Engine.Game.Component.Action.TimelineDependent
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
            if (TextBox.Enabled() is false)
            {
                TextBox.ToggleEnability();
            }
            TextBox.Render();
        }
    }
}

using VisualNovelEngine.Engine.Editor.Interface;

namespace VisualNovelEngine.Engine.Editor.Component.Command
{
    /// <summary>
    /// Scrolls the scrollbar forward
    /// </summary>
    public class ScrollForwardCommand : ICommand
    {
        private Scrollbar Scrollbar { get; set; }
        public ScrollForwardCommand(Scrollbar scrollbar)
        {
            Scrollbar = scrollbar;
        }
        public void Execute()
        {
            Scrollbar.ScrollForward();
        }
    }
}
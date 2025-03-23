using VisualNovelEngine.Engine.Editor.Interface;

namespace VisualNovelEngine.Engine.Editor.Component.Command
{
    /// <summary>
    /// Scrolls the scrollbar backwards
    /// </summary>
    public class ScrollBackwardsCommand : ICommand
    {
        private Scrollbar Scrollbar { get; set; }
        public ScrollBackwardsCommand(Scrollbar scrollbar)
        {
            Scrollbar = scrollbar;
        }
        public void Execute()
        {
            Scrollbar.ScrollBackward();
        }
    }
}
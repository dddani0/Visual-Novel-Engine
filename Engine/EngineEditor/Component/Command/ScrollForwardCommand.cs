using VisualNovelEngine.Engine.EngineEditor.Interface;

namespace VisualNovelEngine.Engine.EngineEditor.Component.Command
{
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
using VisualNovelEngine.Engine.EngineEditor.Interface;

namespace VisualNovelEngine.Engine.EngineEditor.Component.Command
{
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
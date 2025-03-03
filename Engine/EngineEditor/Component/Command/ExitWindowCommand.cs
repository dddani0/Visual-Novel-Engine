using Raylib_cs;
using VisualNovelEngine.Engine.EngineEditor.Interface;

namespace VisualNovelEngine.Engine.EngineEditor.Component.Command
{
    public class ExitWindowCommand : ICommand
    {
        public void Execute()
        {
            Program.Exit = true;
        }
    }
}
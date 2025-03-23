using Raylib_cs;
using VisualNovelEngine.Engine.Editor.Interface;

namespace VisualNovelEngine.Engine.Editor.Component.Command
{
    /// <summary>
    /// Exits the window
    /// </summary>
    public class ExitWindowCommand : ICommand
    {
        public void Execute()
        {
            Program.Exit = true;
        }
    }
}
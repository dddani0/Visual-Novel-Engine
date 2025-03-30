using Raylib_cs;
using VisualNovelEngine.Engine.Editor.Interface;

namespace VisualNovelEngine.Engine.Editor.Component.Command
{
    /// <summary>
    /// Exits the window
    /// </summary>
    public class ExitWindowCommand : ICommand
    {
        Editor Editor { get; set; }
        public ExitWindowCommand(Editor editor)
        {
            Editor = editor;
        }
        public void Execute()
        {
            Editor.Engine.ChangeState(Engine.Component.EngineState.Exit);
        }
    }
}
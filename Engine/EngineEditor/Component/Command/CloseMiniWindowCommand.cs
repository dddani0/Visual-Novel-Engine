using VisualNovelEngine.Engine.EngineEditor.Interface;

namespace VisualNovelEngine.Engine.EngineEditor.Component.Command
{
    public class CloseMiniWindowCommand : ICommand
    {
        private readonly Editor Editor;
        private MiniWindow MiniWindow { get; set; }
        /// <summary>
        /// Constructor for toolbar associated purposes
        /// </summary>
        /// <param name="editor"></param>
        /// <param name="buttonName"></param>
        /// <param name="buttons"></param>
        public CloseMiniWindowCommand(Editor editor, MiniWindow miniWindow)
        {
            Editor = editor;
            MiniWindow = miniWindow;
        }

        public void Execute()
        {
            if (!Editor.MiniWindow.Contains(MiniWindow)) return;
            Editor.MiniWindow.Remove(MiniWindow);
        }
    }
}
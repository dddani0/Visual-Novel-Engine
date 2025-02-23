using VisualNovelEngine.Engine.EngineEditor.Interface;

namespace VisualNovelEngine.Engine.EngineEditor.Component.Command
{
    class CloseShowWindowCommand : ICommand
    {
        private readonly Editor Editor;
        public CloseShowWindowCommand(Editor editor)
        {
            Editor = editor;
        }

        public void Execute()
        {
            Editor.ErrorWindow = null;
        }
    }
}
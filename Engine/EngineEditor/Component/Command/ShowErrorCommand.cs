using EngineEditor.Component;
using VisualNovelEngine.Engine.EngineEditor.Interface;
using static EngineEditor.Component.ErrorWindow;

namespace VisualNovelEngine.Engine.EngineEditor.Component
{
    class ShowErrorCommand : ICommand
    {
        Editor Editor { get; set; }
        ErrorWindow ErrorWindow { get; set; }
        public ShowErrorCommand(Editor editor, string errorMessage, Button[] button)
        {
            Editor = editor;
            ErrorWindow = new ErrorWindow(Editor, errorMessage, button, 400, 200);
        }

        public void Execute()
        {
            Editor.ErrorWindow = ErrorWindow;
        }
    }
}
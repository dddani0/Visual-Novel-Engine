using EngineEditor.Component;
using VisualNovelEngine.Engine.Editor.Interface;
using static EngineEditor.Component.ErrorWindow;

namespace VisualNovelEngine.Engine.Editor.Component
{
    /// <summary>
    /// Shows an error message
    /// </summary>
    class ShowErrorCommand : ICommand
    {
        Editor Editor { get; set; }
        internal ErrorWindow ErrorWindow { get; set; }
        internal string ErrorMessage { get; set; }

        public ShowErrorCommand(Editor editor, string errorMessage, Button[] button)
        {
            Editor = editor;
            ErrorMessage = errorMessage;
            ErrorWindow = new ErrorWindow(Editor, errorMessage, button, 400, 200);
        }

        public void Execute()
        {
            Editor.ErrorWindow = ErrorWindow;
        }
    }
}
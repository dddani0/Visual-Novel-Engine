using Raylib_cs;
using VisualNovelEngine.Engine.Editor.Component;
using VisualNovelEngine.Engine.Editor.Component.Command;
using VisualNovelEngine.Engine.Editor.Interface;

namespace EngineEditor.Component
{
    /// <summary>
    /// Represents a window that displays an error or warning message.
    /// </summary>
    public class ErrorWindow : IWindow
    {
        /// <summary>
        /// Represents the editor.
        /// </summary>
        Editor Editor { get; set; }
        /// <summary>
        /// The error message to display.
        /// </summary>
        internal string Message { get; set; }
        /// <summary>
        /// The x position of the window.
        /// </summary>
        internal int XPosition { get; set; }
        /// <summary>
        /// The y position of the window.
        /// </summary>
        internal int YPosition { get; set; }
        /// <summary>
        /// The width of the window.
        /// </summary>
        internal int Width { get; set; }
        /// <summary>
        /// The height of the window.
        /// </summary>
        internal int Height { get; set; }
        /// <summary>
        /// The cancel button.
        /// </summary>
        internal Button? CancelButton { get; set; }
        /// <summary>
        /// The buttons to display.
        /// </summary>
        internal Button[] Buttons { get; set; }
        /// <summary>
        /// Creates a new error window.
        /// </summary>
        /// <param name="editor"></param>
        /// <param name="errorMessage"></param>
        /// <param name="buttons"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public ErrorWindow(Editor editor, string errorMessage, Button[] buttons, int width, int height)
        {
            Editor = editor;
            Message = errorMessage;
            Width = width;
            Height = height;
            XPosition = Raylib.GetScreenWidth() / 2 - Width / 2;
            YPosition = Raylib.GetScreenHeight() / 2 - Height * (3 / 4);
            CancelButton = new Button(Editor, XPosition, YPosition + Height - Editor.ButtonHeight, "Cancel", Editor.ButtonWidth, Editor.ButtonHeight, 2, Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, new CloseErrorWindowCommand(Editor), Button.ButtonType.Trigger);
            Buttons = buttons;
            UpdateButtonPositions();
        }
        /// <summary>
        /// Updates the positions of the buttons.
        /// </summary>
        private void UpdateButtonPositions()
        {
            for (var i = 0; i < Buttons.Length; i++)
            {
                Buttons[i].XPosition = XPosition + Editor.ButtonWidth * (i + 1);
                Buttons[i].YPosition = YPosition + Height - Editor.ButtonHeight;
            }
        }
        /// <summary>
        /// Shows the error window.
        /// </summary>
        public void Show()
        {
            Raylib.DrawRectangle(XPosition, YPosition, Width, Height, Editor.BaseColor);
            Raylib.DrawRectangleLines(XPosition, YPosition, Width, Height, Editor.TextColor);
            Raylib.DrawText(Message, XPosition, YPosition, 20, Editor.TextColor);
            CancelButton?.Render();
            foreach (var button in Buttons)
            {
                button.Render();
            }
        }
    }
}
using Raylib_cs;
using VisualNovelEngine.Engine.EngineEditor.Component;
using VisualNovelEngine.Engine.EngineEditor.Component.Command;
using VisualNovelEngine.Engine.EngineEditor.Interface;

namespace EngineEditor.Component
{
    public class ErrorWindow : IWindow
    {
        Editor Editor { get; set; }
        internal string ErrorMessage { get; set; }
        private int XPosition { get; set; }
        private int YPosition { get; set; }
        internal int Width { get; set; }
        internal int Height { get; set; }
        internal Button? CancelButton { get; set; }
        internal Button[] Buttons { get; set; }
        public ErrorWindow(Editor editor, string errorMessage, Button[] buttons, int width, int height)
        {
            Editor = editor;
            ErrorMessage = errorMessage;
            Width = width;
            Height = height;
            XPosition = Raylib.GetScreenWidth() / 2 - Width / 2;
            YPosition = Raylib.GetScreenHeight() / 2 - Height * (3 / 4);
            CancelButton = new Button(Editor, XPosition, YPosition + Height - Editor.ButtonHeight, "Cancel", true, Editor.ButtonWidth, Editor.ButtonHeight, 2, Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, new CloseShowWindowCommand(Editor), Button.ButtonType.Trigger);
            Buttons = buttons;
            UpdateButtonPositions();
        }

        private void UpdateButtonPositions()
        {
            for (var i = 0; i < Buttons.Length; i++)
            {
                Buttons[i].XPosition = XPosition + Editor.ButtonWidth * (i + 1);
                Buttons[i].YPosition = YPosition + Height - Editor.ButtonHeight;
            }
        }

        public void Show()
        {
            Raylib.DrawRectangle(XPosition, YPosition, Width, Height, Editor.BaseColor);
            Raylib.DrawRectangleLines(XPosition, YPosition, Width, Height, Editor.TextColor);
            Raylib.DrawText(ErrorMessage, XPosition, YPosition, 20, Editor.TextColor);
            CancelButton?.Render();
            foreach (var button in Buttons)
            {
                button.Render();
            }
        }
    }
}
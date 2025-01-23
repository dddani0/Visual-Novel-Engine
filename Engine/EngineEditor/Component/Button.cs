using System.Numerics;
using EngineEditor.Interface;
using Raylib_cs;

namespace EngineEditor.Component
{
    /// <summary>
    /// Represents a button inside the editor.
    /// </summary>
    public class Button : IButton, IComponent, ITool
    {
        public int XPosition { get; set; }
        public int YPosition { get; set; }
        internal string Text { get; set; }
        internal int Width { get; set; }
        internal int Height { get; set; }
        internal int BorderWidth { get; set; }
        private Color Color { get; set; }
        private Color BorderColor { get; set; }
        private Color HoverColor { get; set; }
        internal bool Active { get; set; } = true;
        internal bool Selected { get; set; }
        private bool IsExecuted { get; set; }
        private bool IsHover { get; set; }
        internal Editor Editor { get; set; }
        private ICommand Command { get; set; }

        public Button(Editor editor, int xPosition, int yPosition, string text, int width, int height, int borderWidth, Color color, Color borderColor, Color hoverColor, ICommand command)
        {
            Editor = editor;
            XPosition = xPosition;
            YPosition = yPosition;
            //If length is greater than 5, set the text to the first 4 and add three dots to the end.
            Text = text.Length > 6 ? $"{text[..5]}..." : text;
            Width = width;
            Height = height;
            BorderWidth = borderWidth;
            Color = color;
            BorderColor = borderColor;
            HoverColor = hoverColor;
        }

        public void Render()
        {
            if (Active is false) return;
            Update();
            Raylib.DrawRectangle(XPosition - Width / 2, YPosition - Height / 2, Width, Height, IsHover ? HoverColor : Color);
            Raylib.DrawRectangleLines(XPosition - Width / 2, YPosition - Height / 2, Width, Height, BorderColor);
            Raylib.DrawText(Text, XPosition - Raylib.MeasureText(Text, 20) / 2, YPosition - 10, 20, Color.Black);
        }

        public void Update()
        {
            IsHover = Raylib.CheckCollisionPointRec(new Vector2(Raylib.GetMouseX(), Raylib.GetMouseY()), new Rectangle(XPosition - Width / 2, YPosition - Height / 2, Width, Height));
            Click();
        }

        internal void AddCommand(ICommand command)
        {
            Command = command;
        }
        public void Click()
        {
            if (Selected)
            {
                if (IsExecuted is false)
                {
                    Command.Execute();
                    IsExecuted = true;
                }
            }
            if (IsHover && Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                Selected = true;
                Command.Execute();
            }
            else if (IsHover is false && Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                Selected = false;
                IsExecuted = false;
            }
        }
    }
}
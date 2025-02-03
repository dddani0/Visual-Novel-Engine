using System.Text.RegularExpressions;
using Raylib_cs;
using TemplateGame.Component;
using VisualNovelEngine.Engine.EngineEditor.Interface;

namespace VisualNovelEngine.Engine.EngineEditor.Component
{
    /// <summary>
    /// Represents a text field that can be rendered on the screen.
    /// A text field can display a single instance of text.
    /// No limit to the row count.
    /// </summary>
    public class TextField : IComponent
    {
        private Editor Editor { get; set; }
        public int XPosition { get; set; }
        public int YPosition { get; set; }
        internal int Width { get; set; }
        internal int Height { get; set; }
        internal int BorderWidth { get; set; }
        internal string? Text { get; set; }
        internal bool Editing { get; set; }
        internal bool IsHover { get; set; }
        internal Font Font { get; set; }
        private Color Color { get; set; }
        private Color BorderColor { get; set; }

        public TextField(Editor editor, int xPosition, int yPosition, int width, int height, int borderWidth, string text, Font font)
        {
            Editor = editor;
            XPosition = xPosition;
            YPosition = yPosition;
            Width = width;
            Height = height;
            BorderWidth = Editor.ComponentBorderWidth;
            Text = text;
            Font = font;
            Color = Editor.BaseColor;
            BorderColor = Editor.BorderColor;
        }

        public void Render()
        {
            Update();
            Raylib.DrawRectangle(XPosition, YPosition, Width, Height, Color);
            Raylib.DrawRectangleLines(XPosition, YPosition, Width, Height, BorderColor);
            Raylib.DrawText(Text, XPosition + 5, YPosition + 5, 12, Color.Black);
        }

        public void Update()
        {
            IsHover = Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), new Rectangle(XPosition, YPosition, Width, Height));
            if (IsHover && Raylib.IsMouseButtonPressed(MouseButton.Left)) Editing = true;
            else if (IsHover is false && Raylib.IsMouseButtonPressed(MouseButton.Left)) Editing = false;
            if (Editing)
            {
                if (Game.IsKeyPressed(KeyboardKey.Backspace))
                {
                    if (Text.Length > 0)
                    {
                        Text = Text.Remove(Text.Length - 1);
                    }
                }
                else if (Game.IsKeyPressed(KeyboardKey.Enter)) Text += '\n';
                else if (Raylib.GetKeyPressed() > 0)
                {
                    Text = Regex.Unescape(Text + ((char)Raylib.GetCharPressed()).ToString()).Replace('\0', ' ');
                }
            }
        }
    }
}
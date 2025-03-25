using System.Text.RegularExpressions;
using Raylib_cs;
using VisualNovelEngine.Engine.Editor.Component.Command;
using VisualNovelEngine.Engine.Editor.Interface;

namespace VisualNovelEngine.Engine.Component
{
    /// <summary>
    /// Represents a command to open the path window.
    /// </summary>
    class InputField : IComponent
    {
        public int XPosition { get; set; }
        public int YPosition { get; set; }
        internal int Width { get; set; }
        internal int Height { get; set; }
        internal Button? Button { get; set; }
        internal string Text { get; set; }
        internal Color Color { get; set; }
        internal Color TextColor { get; set; }
        internal Color HoverColor { get; set; }
        internal bool Hover { get; set; }
        internal bool Active { get; set; }
        public InputField(int x, int y, string text, string buttonText, int width, int height, Color color, Color textColor, Color hoverColor, ICommand command)
        {
            XPosition = x;
            YPosition = y;
            Text = text;
            Width = width;
            Height = height;
            Color = color;
            TextColor = textColor;
            HoverColor = hoverColor;
            Button = new Button(buttonText, XPosition, YPosition + 60, 200, 50, Color.White, Color.Gray, command);
        }

        public InputField(int x, int y, string text, int width, int height, Color color, Color textColor, Color hoverColor)
        {
            XPosition = x;
            YPosition = y;
            Text = text;
            Width = width;
            Height = height;
            Color = color;
            TextColor = textColor;
            HoverColor = hoverColor;
        }

        public void Render()
        {
            Update();
            Raylib.DrawRectangle(XPosition, YPosition, Width, Height, Hover ? HoverColor : Color);
            Raylib.DrawText(Text, XPosition + 5, YPosition + 5, 20, TextColor);
            Button?.Render();
        }

        public void Update()
        {
            if (Active)
            {
                if (VisualNovelEngine.Engine.Game.Component.Game.IsKeyPressed(KeyboardKey.Backspace))
                {
                    if (Text.Length > 0)
                    {
                        Text = Text.Remove(Text.Length - 1);
                    }
                }
                else if (VisualNovelEngine.Engine.Game.Component.Game.IsKeyPressed(KeyboardKey.Enter)) Active = false;
                else if (Raylib.GetKeyPressed() > 0 && !Raylib.IsKeyPressed(KeyboardKey.LeftShift) && !Raylib.IsKeyPressed(KeyboardKey.RightShift))
                {
                    Text = Regex.Unescape(Text + ((char)Raylib.GetCharPressed()).ToString()).Replace('\0', ' ');
                }
                if (Raylib.IsMouseButtonPressed(MouseButton.Left)) Active = false;
            }
            else
            {
                Hover = Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), new Rectangle(XPosition, YPosition, Width, Height));
                if (Hover && Raylib.IsMouseButtonPressed(MouseButton.Left))
                {
                    Active = true;
                    Hover = true;
                }
            }
        }
    }
}
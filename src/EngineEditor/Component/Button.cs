using EngineEditor.Interface;
using Raylib_cs;
namespace EngineEditor.Component
{
    public class Button : IButton, ITool
    {
        internal int XPosition { get; set; }
        internal int YPosition { get; set; }
        internal string Text { get; set; }
        internal int Width { get; set; }
        internal int Height { get; set; }
        internal int BorderWidth { get; set; }
        Color Color { get; set; }
        Color BorderColor { get; set; }
        Color HoverColor { get; set; }
        internal bool Active { get; set; } = true;
        private bool IsHover { get; set; }

        public Button(int xPosition, int yPosition, string text, int width, int height, int borderWidth, Color color, Color borderColor, Color hoverColor)
        {
            XPosition = xPosition;
            YPosition = yPosition;
            Text = text;
            Width = width;
            Height = height;
            BorderWidth = borderWidth;
            Color = color;
            BorderColor = borderColor;
            HoverColor = hoverColor;
            Create();
        }

        public void Render()
        {
            if (Active is false) return;
            Raylib.DrawRectangle(XPosition - Width / 2, YPosition - Height / 2, Width, Height, Color);
            Raylib.DrawRectangleLines(XPosition - Width / 2, YPosition - Height / 2, Width, Height, BorderColor);
        }

        public void Create()
        {
        }

        public void Destroy()
        {
        }

        public void Update()
        {
            Click();
        }
        public void Click()
        {

        }
    }
}
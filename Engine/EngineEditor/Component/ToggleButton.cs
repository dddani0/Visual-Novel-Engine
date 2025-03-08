using Raylib_cs;
using VisualNovelEngine.Engine.EngineEditor.Component;
using VisualNovelEngine.Engine.EngineEditor.Interface;

namespace Namespace
{
    public class ToggleButton : IComponent
    {
        private Editor Editor { get; set; }
        public int XPosition { get; set; }
        public int YPosition { get; set; }
        internal int Size { get; set; }
        internal int BorderWidth { get; set; }
        internal string? Text { get; set; }
        internal bool IsHover { get; set; }
        internal bool IsToggled { get; set; }
        private Color Color { get; set; }
        private Color BorderColor { get; set; }
        private Color HoverColor { get; set; }

        public ToggleButton(Editor editor, int xPosition, int yPosition, int size, int borderWidth, string text, bool toggled)
        {
            Editor = editor;
            XPosition = xPosition;
            YPosition = yPosition;
            Size = size;
            BorderWidth = borderWidth;
            Text = text;
            Color = Editor.BaseColor;
            BorderColor = Editor.BorderColor;
            HoverColor = Editor.HoverColor;
            IsToggled = toggled;
        }

        public void Update()
        {
            IsHover = Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), new Rectangle(XPosition, YPosition, Size, Size));
            if (IsHover is false) return;
            if (Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                IsToggled = !IsToggled;
            }
        }
        public void Render()
        {
            Update();
            if (IsHover)
            {
                Raylib.DrawRectangle(XPosition, YPosition, Size, Size, HoverColor);
            }
            else
            {
                Raylib.DrawRectangle(XPosition, YPosition, Size, Size, Color);
            }
            Raylib.DrawRectangleLinesEx(new Rectangle(XPosition, YPosition, Size, Size), BorderWidth, BorderColor);
            Raylib.DrawText(Text, XPosition + Size, YPosition, 20, Raylib_cs.Color.White);
            if (IsToggled)
            {
                Raylib.DrawRectangle(XPosition + Size - 20, YPosition, 20, 20, Raylib_cs.Color.Red);
            }
        }
    }
}
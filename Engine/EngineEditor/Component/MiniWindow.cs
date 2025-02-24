using Raylib_cs;
using TemplateGame.Component;
using VisualNovelEngine.Engine.EngineEditor.Interface;

namespace VisualNovelEngine.Engine.EngineEditor.Component
{
    /// <summary>
    /// A mini window is a window that is only shown if the player presses the right click.
    /// </summary>
    public class MiniWindow : IWindow
    {
        private Editor Editor { get; set; }
        internal int XPosition { get; set; }
        internal int YPosition { get; set; }
        internal int Width { get; set; }
        internal int Height { get; set; }
        internal int BorderWidth { get; set; }
        internal Color Color { get; set; }
        internal Color BorderColor { get; set; }
        internal bool IsHover { get; set; } = false;
        internal List<Button> ComponentList { get; set; } = [];
        internal Scrollbar Scrollbar { get; set; }

        public MiniWindow(Editor editor, int xPosition, int yPosition, int width, int height, int borderWidth, Color color, Color borderColor, Button[] buttons)
        {
            Editor = editor;
            XPosition = xPosition;
            YPosition = yPosition;
            Width = width;
            Height = height;
            BorderWidth = borderWidth;
            Color = color;
            BorderColor = borderColor;
            ComponentList.AddRange(buttons);
            UpdateComponentPosition();
            Scrollbar = new Scrollbar(Editor, XPosition + Width, YPosition, Height, Editor.SmallButtonWidth, Scrollbar.ScrollbarType.Vertical, false, [.. ComponentList]);
        }

        private void UpdateComponentPosition()
        {
            if (ComponentList.Count < 1) return;
            for (int i = 0; i < ComponentList.Count; i++)
            {
                ComponentList[i].XPosition = XPosition;
                ComponentList[i].YPosition = YPosition + (i * Editor.ComponentHeight);
            }
        }

        public void Show()
        {
            IsHover = Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), new Rectangle(XPosition, YPosition, Width, Height));
            Raylib.DrawRectangle(XPosition, YPosition, Width, Height, Color);
            Raylib.DrawRectangleLines(XPosition, YPosition, Width, Height, BorderColor);
            if (ComponentList == null) return;
            if (ComponentList.Count * Editor.ComponentHeight > Height) Scrollbar.Render();
            for (int i = 0; i < ComponentList.Count; i++)
            {
                ComponentList[i].Render();
            }
        }
    }
}
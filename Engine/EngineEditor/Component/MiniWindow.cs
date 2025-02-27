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
        public enum miniWindowType
        {
            Vertical,
            Horizontal
        }
        internal miniWindowType Type { get; set; }
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

        public MiniWindow(Editor editor, int xPosition, int yPosition, int width, int height, int borderWidth, Color color, Color borderColor, miniWindowType miniWindowType, Button[] buttons)
        {
            Editor = editor;
            Type = miniWindowType;
            XPosition = xPosition;
            YPosition = yPosition;
            Width = width;
            Height = height;
            BorderWidth = borderWidth;
            Color = color;
            BorderColor = borderColor;
            ComponentList.AddRange(buttons);
            UpdateComponentPosition();
            switch (Type)
            {
                case miniWindowType.Vertical:
                    Scrollbar = new Scrollbar(Editor, XPosition + Width, YPosition, Editor.SmallButtonHeight, Editor.SmallButtonWidth, Scrollbar.ScrollbarType.Vertical, false, [.. ComponentList]);
                    break;
                case miniWindowType.Horizontal:
                    Scrollbar = new Scrollbar(Editor, XPosition + Width, YPosition, Editor.SmallButtonHeight, Editor.SmallButtonWidth, Scrollbar.ScrollbarType.Vertical, false, [.. ComponentList]);
                    break;
            }
        }

        private void UpdateComponentPosition()
        {
            if (ComponentList.Count < 1) return;
            switch (Type)
            {
                case miniWindowType.Vertical:
                    for (int i = 0; i < ComponentList.Count; i++)
                    {
                        ComponentList[i].XPosition = XPosition;
                        ComponentList[i].YPosition = YPosition + (i * Editor.SmallButtonHeight);
                    }
                    break;
                case miniWindowType.Horizontal:
                    for (int i = 0; i < ComponentList.Count; i++)
                    {
                        ComponentList[i].XPosition = XPosition + (i * Editor.ButtonWidth);
                        ComponentList[i].YPosition = YPosition;
                    }
                    break;
            }

        }

        public void Show()
        {
            IsHover = Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), new Rectangle(XPosition, YPosition, Width, Height));
            Raylib.DrawRectangle(XPosition, YPosition, Width, Height, Color);
            Raylib.DrawRectangleLines(XPosition, YPosition, Width, Height, BorderColor);
            if (ComponentList == null) return;
            if (ComponentList.Count * Editor.SmallButtonHeight > Raylib.GetScreenHeight()) Scrollbar.Render();
            for (int i = 0; i < ComponentList.Count; i++)
            {
                ComponentList[i].Render();
            }
        }
    }
}
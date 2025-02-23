using System.Runtime.CompilerServices;
using Raylib_cs;
using VisualNovelEngine.Engine.EngineEditor.Interface;

namespace VisualNovelEngine.Engine.EngineEditor.Component
{
    public class Scrollbar : IComponent
    {
        public enum ScrollbarType
        {
            Vertical,
            Horizontal
        }
        Editor Editor { get; set; }
        ScrollbarType Type { get; set; }
        public int XPosition { get; set; }
        public int YPosition { get; set; }
        internal int Width { get; set; }
        internal int Height { get; set; }
        internal int BorderWidth { get; set; }
        internal int ScrollWidth { get; set; }
        internal int ScrollHeight { get; set; }
        internal int ScrollXPosition { get; set; }
        internal int ScrollYPosition { get; set; }
        internal bool IsHover { get; set; }
        internal bool IsStatic { get; set; }
        internal bool IsSelected { get; set; } = false;
        internal bool IsScrolling { get; set; } = false;
        internal Color Color { get; set; }
        internal Color BorderColor { get; set; }
        internal Color ScrollColor { get; set; }
        internal Color ScrollHoverColor { get; set; }
        internal Color ScrollBorderColor { get; set; }
        List<IComponent> Components { get; set; } = [];
        public Scrollbar(Editor editor, int xPosition, int yPosition, int height, int width, ScrollbarType type, bool isStatic, IComponent[] components)
        {
            Editor = editor;
            XPosition = xPosition;
            YPosition = yPosition;
            Width = width;
            Height = height;
            Width = width;
            ScrollWidth = Width / 2;
            ScrollHeight = Height;
            Type = type;
            IsStatic = isStatic;
            BorderWidth = Editor.ComponentBorderWidth;
            Color = Editor.BaseColor;
            BorderColor = Editor.BorderColor;
            ScrollColor = Editor.BaseColor;
            ScrollHoverColor = Editor.HoverColor;
            ScrollBorderColor = Editor.BorderColor;
            ScrollXPosition = XPosition;
            ScrollYPosition = YPosition;
            Components.AddRange(components);
        }

        public void Render()
        {
            Update();
            Raylib.DrawRectangle(XPosition, YPosition, Width, Height, Color);
            Raylib.DrawRectangleLines(XPosition, YPosition, Width, Height, BorderColor);
            Raylib.DrawRectangle(ScrollXPosition, ScrollYPosition, ScrollWidth, ScrollHeight, ScrollColor);
            Raylib.DrawRectangleLines(ScrollXPosition, ScrollYPosition, ScrollWidth, ScrollHeight, ScrollBorderColor);
        }

        public void Update()
        {
            if (IsStatic) return;
            IsHover = Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), new Rectangle(ScrollXPosition, ScrollYPosition, ScrollWidth, ScrollHeight));
            IsScrolling = IsSelected && IsHover;
            if (IsHover && Raylib.IsMouseButtonDown(MouseButton.Left))
            {
                IsSelected = true;
            }
            else if (IsHover is false && Raylib.IsMouseButtonUp(MouseButton.Left)) IsSelected = false;
            if (IsSelected)
            {
                switch (Type)
                {
                    case ScrollbarType.Vertical:
                        ScrollYPosition = (int)Raylib.GetMousePosition().Y; //Ez egy fos?
                        if (ScrollYPosition < YPosition) ScrollYPosition = YPosition;
                        if (ScrollYPosition + ScrollHeight > YPosition + Height) ScrollYPosition = YPosition + Height - ScrollHeight;
                        for (int i = 0; i < Components.Count; i++)
                        {
                            Components[i].YPosition -= ScrollYPosition;
                        }
                        break;
                    case ScrollbarType.Horizontal:
                        ScrollXPosition = (int)Raylib.GetMousePosition().X;
                        if (ScrollXPosition < XPosition) ScrollXPosition = XPosition;
                        if ((ScrollXPosition + ScrollWidth) > (XPosition + Width)) ScrollXPosition = XPosition + Width - ScrollWidth;
                        for (int i = 0; i < Components.Count; i++)
                        {
                            Components[i].XPosition -= ScrollXPosition;
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        public void AddComponent(IComponent component)
        {
            Components.Add(component);
            ScrollWidth = Raylib.GetScreenWidth() / Components.Count;
        }
    }
}
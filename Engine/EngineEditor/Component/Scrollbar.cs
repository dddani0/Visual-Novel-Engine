using System.Runtime.CompilerServices;
using Raylib_cs;
using VisualNovelEngine.Engine.EngineEditor.Component.Command;
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
        internal bool IsStatic { get; set; }
        internal Color Color { get; set; }
        internal Color BorderColor { get; set; }
        internal Color ScrollColor { get; set; }
        internal Color ScrollHoverColor { get; set; }
        internal Color ScrollBorderColor { get; set; }
        List<IComponent> Components { get; set; } = [];
        private Button NextButton { get; set; }
        private Button PreviousButton { get; set; }
        public Scrollbar(Editor editor, int xPosition, int yPosition, int height, int width, ScrollbarType type, bool isStatic, IComponent[] components)
        {
            Editor = editor;
            XPosition = xPosition;
            YPosition = yPosition;
            Width = width;
            Height = height;
            Width = width;
            Type = type;
            IsStatic = isStatic;
            BorderWidth = Editor.ComponentBorderWidth;
            Color = Editor.BaseColor;
            BorderColor = Editor.BorderColor;
            ScrollColor = Editor.BaseColor;
            ScrollHoverColor = Editor.HoverColor;
            ScrollBorderColor = Editor.BorderColor;
            Components.AddRange(components);
            switch (Type)
            {
                case ScrollbarType.Vertical:
                    NextButton = new Button(Editor, XPosition, YPosition + Height - Editor.SmallButtonHeight, "+",true, Editor.SmallButtonWidth, Editor.SmallButtonHeight, BorderWidth, Color, BorderColor, Editor.HoverColor, new ScrollForwardCommand(this), Button.ButtonType.Trigger);
                    PreviousButton = new Button(Editor, XPosition, YPosition, "-",true, Editor.SmallButtonWidth, Editor.SmallButtonHeight, BorderWidth, Color, BorderColor, Editor.HoverColor, new ScrollBackwardsCommand(this), Button.ButtonType.Trigger);
                    break;
                case ScrollbarType.Horizontal:
                    NextButton = new Button(Editor, XPosition + Width - Editor.SmallButtonWidth, YPosition, "+",true, Editor.SmallButtonWidth, Editor.SmallButtonHeight, BorderWidth, Color, BorderColor, Editor.HoverColor, new ScrollForwardCommand(this), Button.ButtonType.Trigger);
                    PreviousButton = new Button(Editor, XPosition, YPosition, "-",true, Editor.SmallButtonWidth, Editor.SmallButtonHeight, BorderWidth, Color, BorderColor, Editor.HoverColor, new ScrollBackwardsCommand(this), Button.ButtonType.Trigger);
                    break;
            }
        }

        public void Render()
        {
            Raylib.DrawRectangle(XPosition, YPosition, Width, Height, Color);
            Raylib.DrawRectangleLines(XPosition, YPosition, Width, Height, BorderColor);
            Update();
        }

        public void Update()
        {
            if (IsStatic) return;
            NextButton.Render();
            PreviousButton.Render();
        }
        internal void ScrollForward()
        {
            switch (Type)
            {
                case ScrollbarType.Vertical:
                    //if the last element is on the screen, don't scroll
                    if (Components[^1].YPosition + Editor.SmallButtonHeight <= YPosition + Height) return;
                    for (int i = 0; i < Components.Count; i++)
                    {
                        Components[i].YPosition -= Editor.SmallButtonHeight;
                    }
                    break;
                case ScrollbarType.Horizontal:
                    if (Components[^1].XPosition + Editor.ButtonWidth <= XPosition + Width) return;
                    for (int i = 0; i < Components.Count; i++)
                    {
                        Components[i].XPosition -= Editor.ButtonWidth;
                    }
                    break;
            }
        }
        internal void ScrollBackward()
        {
            switch (Type)
            {
                case ScrollbarType.Vertical:
                    if (Components[0].YPosition >= YPosition) return;
                    for (int i = 0; i < Components.Count; i++)
                    {
                        Components[i].YPosition += Editor.SmallButtonHeight;
                    }
                    break;
                case ScrollbarType.Horizontal:
                    if (Components[0].XPosition >= XPosition) return;
                    for (int i = 0; i < Components.Count; i++)
                    {
                        Components[i].XPosition += Editor.ButtonWidth;
                    }
                    break;
            }

        }
        public void AddComponent(IComponent component) => Components.Add(component);
        public void AddComponent(IComponent component, int indexOffset) => Components.Insert(Components.Count - (indexOffset - 1), component);
        public void AddComponents(IComponent[] components) => Components.AddRange(components);
        public void DropComponents() => Components.Clear();
    }
}
using System.Runtime.CompilerServices;
using Raylib_cs;
using VisualNovelEngine.Engine.Editor.Component.Command;
using VisualNovelEngine.Engine.Editor.Interface;

namespace VisualNovelEngine.Engine.Editor.Component
{
    /// <summary>
    /// A scrollbar component that can be used to scroll through a list of components
    /// </summary>
    public class Scrollbar : IComponent
    {
        /// <summary>
        /// The type of scrollbar
        /// </summary>
        public enum ScrollbarType
        {
            Vertical,
            Horizontal
        }
        /// <summary>
        /// The editor that the scrollbar is in
        /// </summary>
        Editor Editor { get; set; }
        /// <summary>
        /// The type of scrollbar
        /// </summary>
        ScrollbarType Type { get; set; }
        /// <summary>
        /// The x position of the scrollbar
        /// </summary>
        public int XPosition { get; set; }
        /// <summary>
        /// The y position of the scrollbar
        /// </summary>
        public int YPosition { get; set; }
        /// <summary>
        /// The width and height of the scrollbar
        /// </summary>
        internal int Width { get; set; }
        /// <summary>
        /// The height of the scrollbar
        /// </summary>
        internal int Height { get; set; }
        /// <summary>
        /// The border width of the scrollbar
        /// </summary>
        internal int BorderWidth { get; set; }
        /// <summary>
        /// If the scrollbar is static
        /// </summary>
        internal bool IsStatic { get; set; }
        /// <summary>
        /// The color of the scrollbar
        /// </summary>
        internal Color Color { get; set; }
        /// <summary>
        /// The border color of the scrollbar
        /// </summary>
        internal Color BorderColor { get; set; }
        /// <summary>
        /// The color of the scrollbar scroll
        /// </summary>
        internal Color ScrollColor { get; set; }
        /// <summary>
        /// The hover color of the scrollbar scroll
        /// </summary>
        internal Color ScrollHoverColor { get; set; }
        /// <summary>
        /// The border color of the scrollbar scroll
        /// </summary>
        internal Color ScrollBorderColor { get; set; }
        /// <summary>
        /// The components that the scrollbar is scrolling through
        /// </summary>
        internal List<IComponent> Components { get; set; } = [];
        /// <summary>
        /// The next button of the scrollbar
        /// </summary>
        private Button NextButton { get; set; }
        /// <summary>
        /// The previous button of the scrollbar
        /// </summary>
        private Button PreviousButton { get; set; }
        /// <summary>
        /// Creates a new scrollbar
        /// </summary>
        /// <param name="editor"></param>
        /// <param name="xPosition"></param>
        /// <param name="yPosition"></param>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="type"></param>
        /// <param name="isStatic"></param>
        /// <param name="components"></param>
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
                    NextButton = new Button(Editor, XPosition, YPosition + Height - Editor.SmallButtonHeight, "+",  Editor.SmallButtonWidth, Editor.SmallButtonHeight, BorderWidth, Color, BorderColor, Editor.HoverColor, new ScrollForwardCommand(this), Button.ButtonType.Trigger);
                    PreviousButton = new Button(Editor, XPosition, YPosition, "-",  Editor.SmallButtonWidth, Editor.SmallButtonHeight, BorderWidth, Color, BorderColor, Editor.HoverColor, new ScrollBackwardsCommand(this), Button.ButtonType.Trigger);
                    break;
                case ScrollbarType.Horizontal:
                    NextButton = new Button(Editor, XPosition + Width - Editor.SmallButtonWidth, YPosition, "+", Editor.SmallButtonWidth, Editor.SmallButtonHeight, BorderWidth, Color, BorderColor, Editor.HoverColor, new ScrollForwardCommand(this), Button.ButtonType.Trigger);
                    PreviousButton = new Button(Editor, XPosition, YPosition, "-", Editor.SmallButtonWidth, Editor.SmallButtonHeight, BorderWidth, Color, BorderColor, Editor.HoverColor, new ScrollBackwardsCommand(this), Button.ButtonType.Trigger);
                    break;
            }
        }
        /// <summary>
        /// Renders the scrollbar
        /// </summary>
        public void Render()
        {
            Raylib.DrawRectangle(XPosition, YPosition, Width, Height, Color);
            Raylib.DrawRectangleLines(XPosition, YPosition, Width, Height, BorderColor);
            Update();
        }
        /// <summary>
        /// Updates the scrollbar
        /// </summary>
        public void Update()
        {
            if (IsStatic) return;
            NextButton.Render();
            PreviousButton.Render();
        }
        /// <summary>
        /// Scrolls the scrollbar forward
        /// </summary>
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
                    if (Components[^1].XPosition + Editor.ButtonWidth <= Width) return;
                    for (int i = 0; i < Components.Count; i++)
                    {
                        Components[i].XPosition -= Editor.ButtonWidth;
                    }
                    break;
            }
        }
        /// <summary>
        /// Scrolls the scrollbar backward
        /// </summary>
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
        /// <summary>
        /// Adds a component to the scrollbar
        /// </summary>
        /// <param name="component"></param>
        public void AddComponent(IComponent component) => Components.Add(component);
        /// <summary>
        /// Adds a component to the scrollbar at a specific index
        /// </summary>
        /// <param name="component"></param>
        /// <param name="indexOffset"></param>
        public void AddComponent(IComponent component, int indexOffset) => Components.Insert(Components.Count - (indexOffset - 1), component);
        /// <summary>
        /// Adds multiple components to the scrollbar
        /// </summary>
        /// <param name="components"></param>
        public void AddComponents(IComponent[] components) => Components.AddRange(components);
        /// <summary>
        /// Removes a component from the scrollbar
        /// </summary>
        public void DropComponents() => Components.Clear();
    }
}
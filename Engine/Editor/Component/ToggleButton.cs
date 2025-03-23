using Raylib_cs;
using VisualNovelEngine.Engine.Editor.Component;
using VisualNovelEngine.Engine.Editor.Interface;

namespace VisualNovelEngine.Engine.Editor.Component
{
    /// <summary>
    /// A toggle button component.
    /// </summary>
    public class ToggleButton : IComponent
    {
        /// <summary>
        /// The editor instance.
        /// </summary>
        private Editor Editor { get; set; }
        /// <summary>
        /// The x position of the button.
        /// </summary>
        public int XPosition { get; set; }
        /// <summary>
        /// The y position of the button.
        /// </summary>
        public int YPosition { get; set; }
        /// <summary>
        /// The size of the button.
        /// </summary>
        internal int Size { get; set; }
        /// <summary>
        /// The border width of the button.
        /// </summary>
        internal int BorderWidth { get; set; }
        /// <summary>
        /// The text of the button.
        /// </summary>
        internal string? Text { get; set; }
        /// <summary>
        /// The hover state of the button.
        /// </summary>
        internal bool IsHover { get; set; }
        /// <summary>
        /// The toggle state of the button.
        /// </summary>
        internal bool IsToggled { get; set; }
        /// <summary>
        /// The color of the button.
        /// </summary>
        private Color Color { get; set; }
        /// <summary>
        /// The border color of the button.
        /// </summary>
        private Color BorderColor { get; set; }
        /// <summary>
        /// The hover color of the button.
        /// </summary>
        private Color HoverColor { get; set; }
        /// <summary>
        /// The constructor of the toggle button.
        /// </summary>
        /// <param name="editor"></param>
        /// <param name="xPosition"></param>
        /// <param name="yPosition"></param>
        /// <param name="size"></param>
        /// <param name="borderWidth"></param>
        /// <param name="text"></param>
        /// <param name="toggled"></param>
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
        /// <summary>
        /// The update method of the toggle button.
        /// </summary>
        public void Update()
        {
            IsHover = Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), new Rectangle(XPosition, YPosition, Size, Size));
            if (IsHover is false) return;
            if (Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                IsToggled = !IsToggled;
            }
        }
        /// <summary>
        /// The render method of the toggle button.
        /// </summary>
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
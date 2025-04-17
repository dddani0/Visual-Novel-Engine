using System.Text.RegularExpressions;
using Raylib_cs;
using VisualNovelEngine.Engine.Game.Component;
using VisualNovelEngine.Engine.Editor.Interface;

namespace VisualNovelEngine.Engine.Editor.Component
{
    /// <summary>
    /// Represents a text field that can be rendered on the screen.
    /// A text field can display a single instance of text.
    /// No limit to the row count.
    /// </summary>
    public class TextField : IComponent
    {
        /// <summary>
        /// The editor that the text field is associated with.
        /// </summary>
        private Editor Editor { get; set; }
        /// <summary>
        /// The x position of the text field.
        /// </summary>
        public int XPosition { get; set; }
        /// <summary>
        /// The y position of the text field.
        /// </summary>
        public int YPosition { get; set; }
        /// <summary>
        /// The width and height of the text field.
        /// </summary>
        internal int Width { get; set; }
        /// <summary>
        /// The height of the text field.
        /// </summary>
        internal int Height { get; set; }
        /// <summary>
        /// The border width of the text field.
        /// </summary>
        internal int BorderWidth { get; set; }
        /// <summary>
        /// The text of the text field.
        /// </summary>
        internal string? Text { get; set; }
        /// <summary>
        /// Whether the text field is being edited.
        /// </summary>
        internal bool Editing { get; set; }
        /// <summary>
        /// Whether the text field is being hovered over.
        /// </summary>
        internal bool IsHover { get; set; }
        /// <summary>
        /// Whether the text field is static.
        /// </summary>
        internal bool IsStatic { get; set; }
        /// <summary>
        /// The font of the text field.
        /// </summary>
        internal Font Font { get; set; }
        /// <summary>
        /// The color of the text field.
        /// </summary>
        private Color Color { get; set; }
        /// <summary>
        /// The border color of the text field.
        /// </summary>
        private Color BorderColor { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="TextField"/> class.
        /// </summary>
        /// <param name="editor"></param>
        /// <param name="xPosition"></param>
        /// <param name="yPosition"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="borderWidth"></param>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <param name="isStatic"></param>
        public TextField(Editor editor, int xPosition, int yPosition, int width, int height, int borderWidth, string text, Font font, bool isStatic)
        {
            Editor = editor;
            XPosition = xPosition;
            YPosition = yPosition;
            Width = width;
            Height = height;
            BorderWidth = borderWidth;
            Text = text;
            Font = font;
            Color = Editor.BaseColor;
            BorderColor = Editor.BorderColor;
            IsStatic = isStatic;
        }
        /// <summary>
        /// Renders the text field on the screen.
        /// </summary>
        public void Render()
        {
            Update();
            Raylib.DrawRectangle(XPosition, YPosition, Width, Height, Color);
            Raylib.DrawRectangleLines(XPosition, YPosition, Width, Height, BorderColor);
            Raylib.DrawText(Text, XPosition + 5, YPosition + 5, 12, Color.Black);
        }
        /// <summary>
        /// Updates the text field.
        /// </summary>
        public void Update()
        {
            if (IsStatic) return;
            IsHover = Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), new Rectangle(XPosition, YPosition, Width, Height));
            if (IsHover && Raylib.IsMouseButtonPressed(MouseButton.Left)) Editing = true;
            else if (IsHover is false && Raylib.IsMouseButtonPressed(MouseButton.Left)) Editing = false;
            if (Editing)
            {
                if (VisualNovelEngine.Engine.Game.Component.Game.IsKeyPressed(KeyboardKey.Backspace))
                {
                    if (Text.Length > 0)
                    {
                        Text = Text.Remove(Text.Length - 1);
                    }
                }
                else if (VisualNovelEngine.Engine.Game.Component.Game.IsKeyPressed(KeyboardKey.Enter)) Text += '\n';
                else if (Raylib.GetKeyPressed() > 0)
                {
                    Text = Regex.Unescape(Text + ((char)Raylib.GetCharPressed()).ToString()).Replace('\0', ' ');
                }
            }
        }
    }
}
using Raylib_cs;
using VisualNovelEngine.Engine.Editor.Interface;

namespace VisualNovelEngine.Engine.Editor.Component
{
    /// <summary>
    /// Represents a label, which is a simple text component.
    /// </summary>
    public class Label : IComponent
    {
        /// <summary>
        /// The x position of the label.
        /// </summary>
        public int XPosition { get; set; }
        /// <summary>
        /// The y position of the label.
        /// </summary>
        public int YPosition { get; set; }
        /// <summary>
        /// The text of the label.
        /// </summary>
        internal string Text { get; set; }
        /// <summary>
        /// The width of the label.
        /// </summary>
        internal int Width { get; set; }
        /// <summary>
        /// The height of the label.
        /// </summary>
        internal int Height { get; set; }
        /// <summary>
        /// Whether the label is active.
        /// </summary>
        internal bool Active { get; set; } = true;
        /// <summary>
        /// Creates a new label.
        /// </summary>
        /// <param name="xPosition"></param>
        /// <param name="yPosition"></param>
        /// <param name="text"></param>
        public Label(int xPosition, int yPosition, string text)
        {
            XPosition = xPosition;
            YPosition = yPosition;
            Text = text;
        }
        /// <summary>
        /// Renders the label.
        /// </summary>
        public void Render()
        {
            if (Active is false) return;
            Raylib.DrawText(Text, XPosition, YPosition, 20, Color.White);
        }
        /// <summary>
        /// Updates the label.
        /// </summary>
        public void Update()
        {
            //null
            Width = Text.Length * Raylib.GetFontDefault().BaseSize / 2;
            Height = Raylib.GetFontDefault().BaseSize;
        }
    }
}
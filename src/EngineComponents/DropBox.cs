using EngineComponents.Interfaces;
using Raylib_cs;

namespace EngineComponents
{
    public class DropBox : IPermanentRenderingObject
    {
        internal int XPosition { get; set; }
        internal int YPosition { get; set; }
        internal int Width { get; set; }
        internal int Height { get; set; }
        internal string Text { get; set; }
        internal bool IsSelected { get; set; }
        internal bool IsVisible { get; set; }
        internal List<Button> Options { get; set; }
        internal Button SelectedOption { get; set; }
        private Color DropBoxColor { get; set; }
        private Color DropBoxBorderColor { get; set; }
        private Color DropBoxHoverColor { get; set; }
        public DropBox(Block block, int xPosition, int yPosition, int width, int height, string text, Button[] options, Color dropBoxColor, Color dropBoxBorderColor, Color dropBoxHoverColor)
        {
            XPosition = block.XPosition + xPosition;
            YPosition = block.YPosition + yPosition;
            Width = width;
            Height = height;
            Text = text;
            DropBoxColor = dropBoxColor;
            DropBoxBorderColor = dropBoxBorderColor;
            DropBoxHoverColor = dropBoxHoverColor;
            IsSelected = false;
            IsVisible = true;
            // Position options below each other
            Options = [];
            for (int i = 0; i < options.Length; i++)
            {
                options[i].XPosition = XPosition - Width / 2;
                options[i].YPosition = (YPosition - Height / 2) - i * options[i].Height;
                Options.Add(options[i]);
            }
        }

        /// <summary>
        /// Enables the DropBox if it's visible.
        /// </summary>
        /// <returns></returns>
        public bool Enabled() => IsVisible;
        /// <summary>
        /// Add an option to the DropBox.
        /// </summary>
        /// <param name="option"></param>
        internal void AddOption(Button option) => Options.Add(option);
        /// <summary>
        /// Add multiple options to the DropBox.
        /// </summary>
        /// <param name="options"></param>
        internal void AddOptions(Button[] options) => Options.AddRange(options);
        /// <summary>
        /// Update the DropBox.
        /// </summary>
        private void UpdateDropBox()
        {
            if (IsSelected)
            {
                foreach (Button option in Options)
                {
                    option.Render();
                    if (option.isPressed)
                    {
                        SelectedOption = option;
                        IsSelected = false;
                    }
                }
            }
        }
        /// <summary>
        /// Render the DropBox.
        /// </summary>
        public void Render()
        {
            if (Enabled() is false) return;
            UpdateDropBox();
            Raylib.DrawRectangle(XPosition - Width / 2, YPosition - Height / 2, Width, Height, DropBoxColor);
            Raylib.DrawRectangleLines(XPosition - Width / 2, YPosition - Height / 2, Width, Height, DropBoxBorderColor);
            Raylib.DrawText(Text, XPosition - Width / 2, YPosition - Height / 2, 20, Color.Black);
        }
    }
}
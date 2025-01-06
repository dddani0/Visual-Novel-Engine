using EngineComponents.Interfaces;
using Raylib_cs;

namespace EngineComponents
{
    /// <summary>
    /// Represents a DropBox.
    /// Gives selectable option from a list of options.
    /// An option is a button.
    /// </summary>
    public class DropBox : IPermanentRenderingObject
    {
        /// <summary>
        /// The position of the DropBox on the X axis.
        /// </summary>
        internal int XPosition { get; set; }
        /// <summary>
        /// The position of the DropBox on the Y axis.
        /// </summary>
        internal int YPosition { get; set; }
        /// <summary>
        /// The width of the DropBox.
        /// </summary>
        internal int Width { get; set; }
        /// <summary>
        /// The height of the DropBox.
        /// </summary>
        internal int Height { get; set; }
        /// <summary>
        /// The text of the DropBox.
        /// </summary>
        internal string Text { get; set; }
        /// <summary>
        /// Is the DropBox selected.
        /// </summary>
        internal bool IsSelected { get; set; } = false;
        /// <summary>
        /// Is the DropBox visible.
        /// </summary>
        internal bool IsVisible { get; set; } = true;
        /// <summary>
        /// Is the DropBox locked.
        /// </summary>
        internal bool IsLocked { get; set; } = false;
        /// <summary>
        /// The list of options in the DropBox.
        /// </summary>
        internal List<Button> Options { get; set; }
        /// <summary>
        /// The selected option in the DropBox.
        /// </summary>
        internal Button SelectedOption { get; set; }
        /// <summary>
        /// The color of the DropBox.
        /// </summary>
        private Color DropBoxColor { get; set; }
        /// <summary>
        /// The border color of the DropBox.
        /// </summary>
        private Color DropBoxBorderColor { get; set; }
        /// <summary>
        /// The hover color of the DropBox.
        /// </summary>
        private Color DropBoxHoverColor { get; set; }
        public DropBox(Block block, int xPosition, int yPosition, int width, int height, Button[] options, Color dropBoxColor, Color dropBoxBorderColor, Color dropBoxHoverColor)
        {
            XPosition = block.XPosition + xPosition;
            YPosition = block.YPosition + yPosition;
            Width = width;
            Height = height;
            Text = options[0].Text;
            DropBoxColor = dropBoxColor;
            DropBoxBorderColor = dropBoxBorderColor;
            DropBoxHoverColor = dropBoxHoverColor;
            IsSelected = false;
            // Position options below each other
            Options = [];
            for (int i = 0; i < options.Length; i++)
            {
                options[i].XPosition = XPosition;
                options[i].YPosition = YPosition + ((i + 1) * Height);
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
            if (IsLocked) return;
            if (Game.IsLeftMouseButtonPressed() && Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), new Rectangle(XPosition - Width / 2, YPosition - Height / 2, Width, Height)))
            {
                IsSelected = !IsSelected;
            }
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
        /// <summary>
        /// Get a DropBox option.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="block"></param>
        /// <param name="font"></param>
        /// <param name="xPosition"></param>
        /// <param name="yPosition"></param>
        /// <param name="borderWidth"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="text"></param>
        /// <param name="buttonEvent"></param>
        /// <param name="textColor"></param>
        /// <param name="buttonColor"></param>
        /// <param name="borderColor"></param>
        /// <param name="hoverColor"></param>
        /// <returns></returns>
        public static Button GetDropBoxOption(Game game, Block block, Font font, int xPosition, int yPosition, int borderWidth, int width, int height, string text, Color textColor, Color buttonColor, Color borderColor, Color hoverColor, ISettingsEvent buttonEvent)
            => new(game, block, font, xPosition, yPosition, borderWidth, width, height, text, textColor, buttonColor, borderColor, hoverColor, (IButtonEvent)buttonEvent);

    }
}
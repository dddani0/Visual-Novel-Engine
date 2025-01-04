using EngineComponents.Interfaces;
using Raylib_cs;

namespace EngineComponents
{
    /// <summary>
    /// Represents an input field.
    /// Input data from the player.
    /// </summary>
    class InputField : IPermanentRenderingObject
    {
        internal int XPosition { get; set; }
        internal int YPosition { get; set; }
        internal int Width { get; set; }
        internal int Height { get; set; }
        internal string Text { get; set; }
        internal string Placeholder { get; set; }
        internal bool IsVisible { get; set; }
        internal bool IsSelected { get; set; }
        internal Button Button { get; set; }
        private int BorderWidth { get; set; }
        private Color InputFieldColor { get; set; }
        private Color InputFieldBorderColor { get; set; }
        private Color HoverColor { get; set; }
        public InputField(Game Game, Block block, int ButtonYOffset, int width, int height, string placeholder, string buttonText, IButtonEvent buttonEvent)
        {
            XPosition = block.XPosition;
            YPosition = block.YPosition;
            Width = width;
            Height = height;
            Placeholder = placeholder;
            IsVisible = true;
            IsSelected = false;
            Button = new Button(Game, block, 0, 0 - ButtonYOffset, Width, Height, buttonText, InputFieldColor, InputFieldBorderColor, HoverColor, buttonEvent);
        }
        public bool Enabled() => IsVisible;

        /// <summary>
        /// Update the input field.
        /// </summary>
        private void UpdateInputField()
        {
            if (Enabled() is false) return;
            if (Game.IsLeftMouseButtonPressed())
            {
                if (Raylib.GetMouseX() >= XPosition && Raylib.GetMouseX() <= XPosition + Width && Raylib.GetMouseY() >= YPosition && Raylib.GetMouseY() <= YPosition + Height)
                {
                    IsSelected = true;
                }
                else
                {
                    IsSelected = false;
                }
            }
            if (IsSelected)
            {
                if (Raylib.IsKeyPressed(KeyboardKey.Backspace))
                {
                    if (Text.Length > 0)
                    {
                        Text = Text.Remove(Text.Length - 1);
                    }
                }
                else if (Raylib.IsKeyPressed(KeyboardKey.Enter))
                {
                    IsSelected = false;
                }
                else
                {
                    Text += Raylib.GetCharPressed();
                }
            }
        }
        /// <summary>
        /// Render the input field.
        /// </summary>
        public void Render()
        {
            if (Enabled() is false) return;
            // Update the input field.
            UpdateInputField();
            // Render the Button component
            Button.Render();
            // draw the input field.
            Raylib.DrawRectangle(XPosition - Width / 2, YPosition - Height / 2, Width, Height, InputFieldColor);
            Raylib.DrawRectangleLines(XPosition - Width / 2, YPosition - Height / 2, Width, Height, InputFieldBorderColor);
            switch (IsSelected)
            {
                case true:
                    Raylib.DrawRectangle(XPosition - Width / 2, YPosition - Height / 2, Width, Height, HoverColor);
                    break;
                case false:
                    Raylib.DrawRectangle(XPosition - Width / 2, YPosition - Height / 2, Width, Height, InputFieldColor);
                    if (string.IsNullOrEmpty(Text))
                    {
                        Raylib.DrawText(Placeholder, XPosition + 5, YPosition + 5, 20, Color.Black);
                    }
                    else
                    {
                        Raylib.DrawText(Text, XPosition + 5, YPosition + 5, 20, Color.Black);
                    }
                    break;
            }
        }
    }
}
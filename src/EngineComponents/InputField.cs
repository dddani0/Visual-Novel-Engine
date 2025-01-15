using System.Numerics;
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
        /// <summary>
        /// X position of the input field.
        /// </summary>
        internal int XPosition { get; set; }
        /// <summary>
        /// Y position of the input field.
        /// </summary>
        internal int YPosition { get; set; }
        /// <summary>
        /// Width of the input field.
        /// </summary>
        internal int Width { get; set; }
        /// <summary>
        /// Height of the input field.
        /// </summary>
        internal int Height { get; set; }
        /// <summary>
        /// Text of the input field.
        /// </summary>
        internal string Text { get; set; }
        /// <summary>
        /// Placeholder of the input field.
        /// </summary>
        internal string Placeholder { get; set; }
        /// <summary>
        /// Is the input field visible.
        /// </summary>
        internal bool IsVisible { get; set; }
        /// <summary>
        /// Is the input field hovered.
        /// </summary>
        internal bool IsHover { get; set; }
        /// <summary>
        /// Is the input field selected.
        /// </summary>
        /// 
        internal bool IsSelected { get; set; } = false;
        /// <summary>
        /// Is the input field interactable.
        /// </summary>
        internal bool IsLocked { get; set; } = false;
        /// <summary>
        /// The button component of the input field.
        /// </summary>
        internal Button Button { get; set; }
        /// <summary>
        /// The border width of the input field.
        /// </summary>
        private int BorderWidth { get; set; }
        /// <summary>
        /// The color of the input field.
        /// </summary>
        private Color InputFieldColor { get; set; }
        /// <summary>
        /// The border color of the input field.
        /// </summary>
        private Color InputFieldBorderColor { get; set; }
        /// <summary>
        /// The hover color of the input field.
        /// </summary>
        private Color InputFieldHoverColor { get; set; }
        /// <summary>
        /// Create a new input field with a button attached.
        /// </summary>
        /// <param name="Game"></param>
        /// <param name="block"></param>
        /// <param name="xPosition"></param>
        /// <param name="yPosition"></param>
        /// <param name="ButtonYOffset"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="placeholder"></param>
        /// <param name="buttonText"></param>
        /// <param name="inputFieldColor"></param>
        /// <param name="inputFieldBorderColor"></param>
        /// <param name="inputFieldHoverColor"></param>
        /// <param name="buttonEvent"></param>
        public InputField(Game Game, Block block, int xPosition, int yPosition, int ButtonYOffset, int width, int height, string placeholder, string buttonText, Color inputFieldColor, Color inputFieldBorderColor, Color inputFieldHoverColor, IButtonEvent buttonEvent)
        {
            XPosition = block.XPosition + xPosition;
            YPosition = block.YPosition + yPosition;
            Width = width;
            Height = height;
            Placeholder = placeholder;
            IsVisible = true;
            IsSelected = false;
            InputFieldColor = inputFieldColor;
            InputFieldBorderColor = inputFieldBorderColor;
            InputFieldHoverColor = inputFieldHoverColor;
            Button = new Button(Game, block, new Font() { BaseSize = 30 }, 0, ButtonYOffset, 0, Width, Height, buttonText, Color.Black, InputFieldColor, InputFieldBorderColor, InputFieldHoverColor, buttonEvent);
        }
        /// <summary>
        /// Create a new static input field, with a static button attached.
        /// </summary>
        /// <param name="Game"></param>
        /// <param name="block"></param>
        /// <param name="xPosition"></param>
        /// <param name="yPosition"></param>
        /// <param name="ButtonYOffset"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="placeholder"></param>
        /// <param name="buttonText"></param>
        /// <param name="inputFieldColor"></param>
        /// <param name="inputFieldBorderColor"></param>
        /// <param name="inputFieldHoverColor"></param>
        /// <param name="buttonEvent"></param>
        public InputField(Game Game, Block block, int xPosition, int yPosition, int ButtonYOffset, int width, int height, string placeholder, string buttonText, Color inputFieldColor, Color inputFieldBorderColor, Color inputFieldHoverColor, ISettingsEvent buttonEvent)
        {
            XPosition = block.XPosition + xPosition;
            YPosition = block.YPosition + yPosition;
            Width = width;
            Height = height;
            Placeholder = placeholder;
            IsVisible = true;
            IsSelected = false;
            InputFieldColor = inputFieldColor;
            InputFieldBorderColor = inputFieldBorderColor;
            InputFieldHoverColor = inputFieldHoverColor;
            Button = new Button(Game, block, new Font() { BaseSize = 30 }, 0, ButtonYOffset, 0, Width, Height, buttonText, Color.Black, InputFieldColor, InputFieldBorderColor, InputFieldHoverColor, buttonEvent);
        }
        /// <summary>
        /// Returns if the input field is visible.
        /// </summary>
        /// <returns></returns>
        public bool Enabled() => IsVisible;

        /// <summary>
        /// Update the input field.
        /// </summary>
        private void UpdateInputField()
        {
            if (IsLocked) return;
            if (IsSelected)
            {
                if (Game.IsKeyPressed(KeyboardKey.Backspace))
                {
                    if (Text.Length > 0)
                    {
                        Text = Text.Remove(Text.Length - 1);
                    }
                }
                else if (Game.IsKeyPressed(KeyboardKey.Enter))
                {
                    IsSelected = false;
                }
                else if (Raylib.GetKeyPressed() > 0)
                {
                    Text += (char)Raylib.GetCharPressed();
                }
            }
            if (Game.IsLeftMouseButtonPressed())
            {
                if (Game.GetMouseXPosition() >= XPosition && Game.GetMouseXPosition() <= XPosition + Width && Game.GetMouseYPosition() >= YPosition && Game.GetMouseYPosition() <= YPosition + Height)
                {
                    IsSelected = true;
                }
                else
                {
                    IsSelected = false;
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
            //IsHover = Raylib.CheckCollisionPointRec(new Vector2(Game.GetMouseXPosition(), Game.GetMouseYPosition()), new Rectangle(XPosition - Width / 2, YPosition + Height / 2, Width, Height));
            Raylib.DrawRectangle(XPosition - Width / 2, YPosition + Height / 2, Width, Height, IsHover ? InputFieldHoverColor : InputFieldColor);
            Raylib.DrawRectangleLines(XPosition - Width / 2, YPosition + Height / 2, Width, Height, InputFieldBorderColor);
            switch (IsSelected)
            {
                case true:
                    Raylib.DrawRectangle(XPosition - Width / 2, YPosition + Height / 2, Width, Height, InputFieldHoverColor);
                    break;
                case false:
                    Raylib.DrawRectangle(XPosition - Width / 2, YPosition + Height / 2, Width, Height, InputFieldColor);
                    if (string.IsNullOrEmpty(Text))
                    {
                        Raylib.DrawText(Placeholder, XPosition - Width / 2, YPosition + Height / 2, 20, Color.Black);
                    }
                    else
                    {
                        Raylib.DrawText(Text, XPosition - Width / 2, YPosition + Height, 20, Color.Black);
                    }
                    break;
            }
        }
    }
}
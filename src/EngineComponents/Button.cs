using System.Numerics;
using EngineComponents.Interfaces;
using Raylib_cs;

namespace EngineComponents
{
    /// <summary>
    /// Represent a Button.
    /// Allows to perform an action when clicked.
    /// </summary>
    public class Button : IPermanentRenderingObject
    {
        /// <summary>
        /// The absolute position on the X axis.
        /// </summary>
        internal int XPosition { get; set; }
        /// <summary>
        /// The absolute position on the Y axis.
        /// </summary>
        internal int YPosition { get; set; }
        /// <summary>
        /// The width of the button.
        /// </summary>
        internal int Width { get; set; }
        /// <summary>
        /// The height of the button.
        /// </summary>
        internal int Height { get; set; }
        /// <summary>
        /// The text on the button.
        /// </summary>
        internal string Text { get; set; }
        /// <summary>
        /// Is the cursor hovering above the button?
        /// </summary>
        internal bool isHover;
        /// <summary>
        /// Is the button pressed.
        /// </summary>
        internal bool isPressed;

        /// <summary>
        /// The border width of the button.
        /// </summary>
        int BorderWidth { get; set; }
        /// <summary>
        /// The color of the text.
        /// </summary>
        private Color TextColor { get; set; }
        /// <summary>
        /// The color of the button.
        /// </summary>
        private Color Color { get; }
        /// <summary>
        /// The color of the button when hovered.
        /// </summary>
        private Color HoverColor { get; }
        /// <summary>
        /// The border color of the button.
        /// </summary>
        private Color BorderColor { get; }
        /// <summary>
        /// The event which is attached to the button.
        /// </summary>
        private IButtonEvent Event { get; }
        /// <summary>
        /// The parent block of the button.
        /// </summary>
        private Block ParentBlock { get; }
        /// <summary>
        /// Width of a character within the confines of the Font
        /// </summary>
        private int CharacterWidth { get; set; }
        /// <summary>
        /// Height of a character within the confines of the Font
        /// </summary>
        private int CharacterHeight { get; set; }
        /// <summary>
        /// Width of the text
        /// </summary>
        private int TextWidth { get; set; }
        /// <summary>
        /// The font of the button.
        /// </summary>
        private Font Font { get; set; }
        Game Game { get; }
        /// <summary>
        /// Creates a button.
        /// </summary>
        /// <param name="game">Active Game</param>
        /// <param name="parentMenu">Parent menu of the button</param>
        /// <param name="xPos">Relative position on the X axis.</param>
        /// <param name="yPos">Relative position on the Y axis</param>
        /// <param name="width">Width of the Button</param>
        /// <param name="height">Heigth of the Button</param>
        /// <param name="text">The text on the button</param>
        /// <param name="buttonColor">The neutral color of the Button</param>
        /// <param name="borderColor">The Border color of the Button</param>
        /// <param name="hoverColor">The color of the button in hovering state.</param>
        /// <param name="buttonEvent">The event which is attached to the button</param>
        public Button(Game game, Block block, Font font, int xPos, int yPos, int borderWidth, int width, int height, string text, Color textColor, Color buttonColor, Color borderColor, Color hoverColor, IButtonEvent buttonEvent)
        {
            ParentBlock = block;
            XPosition = block.XPosition + xPos;
            YPosition = block.YPosition + yPos;
            Width = width;
            Height = height;
            BorderWidth = borderWidth;
            Text = text;
            Color = buttonColor;
            BorderColor = borderColor;
            HoverColor = hoverColor;
            TextColor = textColor;
            Event = buttonEvent;
            Game = game;
            Font = font;
            CharacterWidth = (Font.BaseSize / 2) + Font.GlyphPadding;
            CharacterHeight = Font.BaseSize;
            TextWidth = Text.Length * CharacterWidth;
        }

        /// <summary>
        /// Executes the event when the button is pressed.
        /// </summary>
        public void PressButton()
        {
            if (!isHover || !Game.IsLeftMouseButtonPressed()) return;
            isPressed = true;
            Event.PerformEvent();
        }
        /// <summary>
        /// Renders the button on the screen.
        /// </summary>
        public void Render()
        {
            if (Enabled() is false) return;
            isHover = Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), new Rectangle(XPosition - Width / 2, YPosition - Height / 2, Width, Height));
            PressButton();
            Raylib.DrawRectangle(XPosition - Width / 2, YPosition - Height / 2, Width, Height, isHover ? HoverColor : Color);
            Raylib.DrawRectangleLinesEx(new Rectangle(XPosition - Width / 2, YPosition - Height / 2, Width, Height), BorderWidth, BorderColor);
            Raylib.DrawTextEx(Font, Text, new Vector2(XPosition - TextWidth / 2, YPosition - CharacterHeight / 2), Font.BaseSize, 5, TextColor);
        }
        /// <summary>
        /// Checks if the button is enabled.
        /// </summary>
        /// <returns></returns>
        public bool Enabled() => isPressed is false;
    }
}
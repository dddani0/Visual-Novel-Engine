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
        internal bool isHover;
        internal bool isPressed;

        /// <summary>
        /// The border width of the button.
        /// </summary>
        int BorderWidth { get; set; }
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
        private Block ParentMenu { get; }
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
        /// <param name="color">The neutral color of the Button</param>
        /// <param name="borderColor">The Border color of the Button</param>
        /// <param name="hoverColor">The color of the button in hovering state.</param>
        /// <param name="buttonEvent">The event which is attached to the button</param>
        public Button(Game game, Block block, int xPos, int yPos, int width, int height, string text, Color color, Color borderColor, Color hoverColor, IButtonEvent buttonEvent)
        {
            ParentMenu = block;
            XPosition = block.XPosition + xPos;
            YPosition = block.YPosition + yPos;
            Width = width;
            Height = height;
            Text = text;
            Color = color;
            BorderColor = borderColor;
            HoverColor = hoverColor;
            Event = buttonEvent;
            Game = game;
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
            Raylib.DrawTextEx(new Font() { BaseSize = 30, GlyphPadding = 5 }, Text, new Vector2(XPosition, YPosition), 30, 5, Color.White);
        }
        /// <summary>
        /// Checks if the button is enabled.
        /// </summary>
        /// <returns></returns>
        public bool Enabled() => isPressed is false;
    }
}
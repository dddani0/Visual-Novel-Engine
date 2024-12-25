using EngineComponents.Interfaces;
using Raylib_cs;

namespace EngineComponents
{
    /// <summary>
    /// Button component
    /// </summary>
    class Button : IPermanentRenderingObject
    {
        internal int XPos { get; set; }
        internal int YPos { get; set; }
        private int Width { get; set; }
        private int Height { get; set; }
        internal string Text { get; set; }
        internal bool isHover;
        internal bool isPressed;
        int BorderWidth { get; set; }
        private Color Color { get; }
        private Color HoverColor { get; }
        private Color BorderColor { get; }
        private IButtonEvent Event { get; }
        private Menu ParentMenu { get; }
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
        public Button(Game game, Menu parentMenu, int xPos, int yPos, int width, int height, string text, Color color, Color borderColor, Color hoverColor, IButtonEvent buttonEvent)
        {
            ParentMenu = parentMenu;
            XPos = parentMenu.XPosition + xPos;
            YPos = parentMenu.YPosition + yPos;
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
            Game.ActiveScene.Timeline.NextStep();
        }
        /// <summary>
        /// Renders the button on the screen.
        /// </summary>
        public void Render()
        {
            if (Enabled() is false) return;
            isHover = Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), new Rectangle(XPos, YPos, Width, Height));
            Raylib.DrawRectangle(XPos - Width / 2, YPos - Height / 2, Width, Height, isHover ? HoverColor : Color);
        }
        /// <summary>
        /// Checks if the button is enabled.
        /// </summary>
        /// <returns></returns>
        public bool Enabled() => isPressed is false;
    }
}
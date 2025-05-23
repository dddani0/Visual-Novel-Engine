using System.Numerics;
using VisualNovelEngine.Engine.Game.Interface;
using Raylib_cs;

namespace VisualNovelEngine.Engine.Game.Component
{
    /// <summary>
    /// Represent a Button.
    /// Allows to perform an action when clicked.
    /// </summary>
    public class Button : IRenderingObject
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
        internal int BorderWidth { get; set; }
        /// <summary>
        /// The color of the text.
        /// </summary>
        internal Color TextColor { get; set; }
        /// <summary>
        /// The color of the button.
        /// </summary>
        internal Color Color { get; set; }
        /// <summary>
        /// The color of the button when hovered.
        /// </summary>
        internal Color HoverColor { get; set; }
        /// <summary>
        /// The border color of the button.
        /// </summary>
        internal Color BorderColor { get; set; }
        /// <summary>
        /// The event which is attached to the button.
        /// </summary>
        internal IAction Action { get; set; }
        /// <summary>
        /// The parent block of the button.
        /// </summary>
        private Block ParentBlock { get; set; }
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
        /// <param name="game"></param>
        /// <param name="block"></param>
        /// <param name="font"></param>
        /// <param name="xPos"></param>
        /// <param name="yPos"></param>
        /// <param name="borderWidth"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="text"></param>
        /// <param name="textColor"></param>
        /// <param name="buttonColor"></param>
        /// <param name="borderColor"></param>
        /// <param name="hoverColor"></param>
        /// <param name="buttonEvent"></param>
        public Button(Game game, Block block, Font font, int xPos, int yPos, int borderWidth, int width, int height, string text, Color textColor, Color buttonColor, Color borderColor, Color hoverColor, IButtonAction buttonEvent)
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
            Action = (IAction)buttonEvent;
            Game = game;
            Font = font;
            CharacterWidth = (Font.BaseSize / 2) + Font.GlyphPadding;
            CharacterHeight = Font.BaseSize;
            TextWidth = Text.Length * CharacterWidth;
        }
        /// <summary>
        /// Creates a static button.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="block"></param>
        /// <param name="font"></param>
        /// <param name="xPos"></param>
        /// <param name="yPos"></param>
        /// <param name="borderWidth"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="text"></param>
        /// <param name="textColor"></param>
        /// <param name="buttonColor"></param>
        /// <param name="borderColor"></param>
        /// <param name="hoverColor"></param>
        /// <param name="buttonEvent"></param>
        public Button(Game game, Block block, Font font, int xPos, int yPos, int borderWidth, int width, int height, string text, Color textColor, Color buttonColor, Color borderColor, Color hoverColor, ISettingsAction buttonEvent)
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
            Action = (IAction)buttonEvent;
            Game = game;
            Font = font;
            CharacterWidth = (Font.BaseSize / 2) + Font.GlyphPadding;
            CharacterHeight = Font.BaseSize;
            TextWidth = Text.Length * CharacterWidth;
        }

        /// <summary>
        /// Executes the event when the button is pressed.
        /// </summary>
        public void ButtonPress()
        {
            if (!isHover || !Game.IsLeftMouseButtonPressed()) return;
            isPressed = true;
            IAction eventToPerform = (IAction)Action;
            eventToPerform.PerformAction();
        }
        /// <summary>
        /// Renders the button on the screen.
        /// </summary>
        public void Render()
        {
            if (Enabled() is false) return;
            isHover = Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), new Rectangle(XPosition - Width / 2, YPosition - Height / 2, Width, Height));
            ButtonPress();
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
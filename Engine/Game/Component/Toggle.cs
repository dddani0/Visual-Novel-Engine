using VisualNovelEngine.Engine.Game.Component;
using Raylib_cs;
using VisualNovelEngine.Engine.Game.Interface;

namespace VisualNovelEngine.Engine.Game.Component
{
    /// <summary>
    /// A toggle is a button that can be toggled on and off.
    /// </summary>
    public class Toggle : IRenderingObject
    {
        /// <summary>
        /// The x position of the toggle.
        /// </summary>
        internal int XPosition { get; set; }
        /// <summary>
        /// The y position of the toggle.
        /// </summary>
        internal int YPosition { get; set; }
        /// <summary>
        /// The border width of the toggle.
        /// </summary>
        internal int BorderWidth { get; set; }
        /// <summary>
        /// The width of the toggle.
        /// </summary>
        internal int BoxSize { get; set; }
        /// <summary>
        /// The height of the toggle.
        /// </summary>
        internal int TextXOffset { get; set; }
        /// <summary>
        /// The text of the toggle.
        /// </summary>
        internal string Text { get; set; }
        /// <summary>
        /// The parent block of the toggle.
        /// </summary>
        internal bool IsVisible { get; set; }
        /// <summary>
        /// Is the toggle enabled.
        /// </summary>
        internal bool IsToggled { get; set; }
        /// <summary>
        /// Is the toggle locked.
        /// </summary>
        internal bool IsLocked { get; set; }
        /// <summary>
        /// The font of the toggle.
        /// </summary>
        internal Font Font { get; set; }
        /// <summary>
        /// The color of the toggle.
        /// </summary>
        internal Color Color { get; set; }
        /// <summary>
        /// The border color of the toggle.
        /// </summary>
        internal Color BorderColor { get; set; }
        /// <summary>
        /// The color of the toggle when toggled.
        /// </summary>
        internal Color ToggledColor { get; set; }
        /// <summary>
        /// The event which is attached to the toggle.
        /// </summary>
        internal IAction SettingsAction { get; set; }
        /// <summary>
        /// Creates a toggle component.
        /// </summary>
        /// <param name="block"></param>
        /// <param name="xPosition"></param>
        /// <param name="yPosition"></param>
        /// <param name="boxSize"></param>
        /// <param name="textXOffset"></param>
        /// <param name="text"></param>
        /// <param name="isLocked"></param>
        /// <param name="color"></param>
        /// <param name="borderColor"></param>
        /// <param name="toggledColor"></param>
        /// <param name="settingsEvent"></param>
        public Toggle(Block block, int xPosition, int yPosition, int boxSize, int textXOffset, string text, bool isLocked, Color color, Color borderColor, Color toggledColor, ISettingsAction settingsEvent)
        {
            XPosition = block.XPosition + xPosition;
            YPosition = block.YPosition + yPosition;
            BoxSize = boxSize;
            TextXOffset = textXOffset;
            Text = text;
            IsLocked = isLocked;
            Color = color;
            BorderColor = borderColor;
            ToggledColor = toggledColor;
            SettingsAction = (IAction)settingsEvent;
        }
        /// <summary>
        /// Updates the toggle component.
        /// </summary>
        private void Update()
        {
            if (IsLocked) return;
            if (Game.IsLeftMouseButtonPressed() && Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), new Rectangle(XPosition, YPosition, 20, 20)))
            {
                IsToggled = !IsToggled;
                if (IsToggled) SettingsAction.PerformAction();
            }
        }
        /// <summary>
        /// Renders the toggle component.
        /// </summary>
        public void Render()
        {
            if (Enabled() is false) return;
            Update();
            Raylib.DrawRectangle(XPosition - BoxSize / 2, YPosition - BoxSize / 2, BoxSize, BoxSize, Color);
            Raylib.DrawRectangleLines(XPosition - BoxSize / 2, YPosition - BoxSize / 2, BorderWidth, BorderWidth, BorderColor);
            Raylib.DrawText(Text, XPosition + TextXOffset, YPosition, 20, Color);
            if (!IsToggled) return;
            Raylib.DrawRectangle(XPosition - BoxSize / 2, YPosition - BoxSize / 2, BoxSize, BoxSize, Color.Black);
        }
        /// <summary>
        /// Checks if the toggle is enabled.
        /// </summary>
        /// <returns></returns>
        public bool Enabled() => IsVisible;
    }
}
using EngineComponents.Interfaces;
using Raylib_cs;

namespace EngineComponents
{
    /// <summary>
    /// A toggle is a button that can be toggled on and off.
    /// </summary>
    public class Toggle : IPermanentRenderingObject
    {
        internal int XPosition { get; set; }
        internal int YPosition { get; set; }
        internal int BorderWidth { get; set; }
        internal int BoxSize { get; set; }
        internal int TextXOffset { get; set; }
        internal string Text { get; set; }
        internal bool IsVisible { get; set; }
        internal bool IsToggled { get; set; }
        internal bool IsLocked { get; set; }
        internal Font Font { get; set; }
        private Color Color { get; set; }
        private Color BorderColor { get; set; }
        private Color ToggledColor { get; set; }
        public bool Enabled() => IsVisible;
        internal IEvent SettingsEvent { get; set; }

        /// <summary>
        /// Creates a new toggle component.
        /// </summary>
        /// <param name="block">Parent block</param>
        /// <param name="xPosition">Relative position to the block in the X axis.</param>
        /// <param name="yPosition">Relative position to the block in the Y axis.</param>
        /// <param name="text">Text next to the toggle</param>
        /// <param name="isVisible">Should be enabled</param>
        /// <param name="isActive">Is toggled on or off</param>
        /// <param name="isLocked">Is interactable</param>
        /// <param name="color">Color of the toggle</param>
        /// <param name="borderColor">Border color of the toggle</param>
        public Toggle(Block block, int xPosition, int yPosition, int boxSize, int textXOffset, string text, bool isLocked, Color color, Color borderColor, Color toggledColor, ISettingsEvent settingsEvent)
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
            SettingsEvent = (IEvent)settingsEvent;
        }
        /// <summary>
        /// Updates the toggle component.
        /// </summary>
        private void UpdateToggle()
        {
            if (IsLocked) return;
            if (Game.IsLeftMouseButtonPressed() && Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), new Rectangle(XPosition, YPosition, 20, 20)))
            {
                IsToggled = !IsToggled;
                if (IsToggled) SettingsEvent.PerformEvent();
            }
        }
        /// <summary>
        /// Renders the toggle component.
        /// </summary>
        public void Render()
        {
            if (Enabled() is false) return;
            UpdateToggle();
            Raylib.DrawRectangle(XPosition - BoxSize / 2, YPosition - BoxSize / 2, BoxSize, BoxSize, Color);
            Raylib.DrawRectangleLines(XPosition - BoxSize / 2, YPosition - BoxSize / 2, BorderWidth, BorderWidth, BorderColor);
            Raylib.DrawText(Text, XPosition + TextXOffset, YPosition, 20, Color);
            if (!IsToggled) return;
            Raylib.DrawRectangle(XPosition - BoxSize / 2, YPosition - BoxSize / 2, BoxSize, BoxSize, Color.Black);
        }
    }
}
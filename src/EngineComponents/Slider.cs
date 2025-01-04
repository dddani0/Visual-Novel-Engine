using System.Numerics;
using EngineComponents.Interfaces;
using Raylib_cs;

namespace EngineComponents
{
    /// <summary>
    /// Represents a Slider.
    /// </summary>
    class Slider : IPermanentRenderingObject
    {
        internal int XPosition { get; set; }
        internal int YPosition { get; set; }
        internal int Width { get; set; }
        internal int Height { get; set; }
        internal int SliderHeight { get; set; }
        internal int SliderDragRadius { get; set; }
        internal bool IsVisible { get; set; }
        internal bool IsSelected { get; set; }
        internal int UnitValue { get; set; }
        internal int MaximumValue { get; set; }
        //internal IOptionEvent OptionEvent { get; set; }
        private Color SliderColor { get; set; }
        private Color SliderBorderColor { get; set; }
        public bool Enabled() => IsVisible;
        /// <summary>
        ///Updates slider according to mouse input.
        /// </summary>
        private void UpdateSlider()
        {
            if (Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                //execute event here.
                if (Raylib.GetMousePosition().X >= XPosition && Raylib.GetMousePosition().X <= XPosition + Width &&
                    Raylib.GetMousePosition().Y >= YPosition && Raylib.GetMousePosition().Y <= YPosition + Height)
                {
                    UnitValue = (int)Raylib.GetMousePosition().X - XPosition;
                }
            }
            if (UnitValue < 0) UnitValue = 0;
            if (UnitValue > Width) UnitValue = Width;
        }
        /// <summary>
        /// Render the Slider.
        /// </summary>
        public void Render()
        {
            if (Enabled() is false) return;
            UpdateSlider();
            Raylib.DrawLineEx(new Vector2(XPosition, YPosition), new Vector2(XPosition + Width, YPosition), SliderHeight, SliderBorderColor);
            Raylib.DrawCircle(XPosition + UnitValue, YPosition, SliderDragRadius, SliderColor);
        }
    }
}
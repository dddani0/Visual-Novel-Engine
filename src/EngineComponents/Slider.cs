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
        /// <summary>
        /// The position of the Slider on the X axis.
        /// </summary>
        internal int XPosition { get; set; }
        /// <summary>
        /// The position of the Slider on the Y axis.
        /// </summary>
        internal int YPosition { get; set; }
        /// <summary>
        /// The width of the Slider.
        /// </summary>
        internal int Width { get; set; }
        /// <summary>
        /// The height of the Slider.
        /// </summary>
        internal int Height { get; set; }
        /// <summary>
        /// The radius of the Slider's draggable part.
        /// </summary>
        internal int SliderDragRadius { get; set; }
        /// <summary>
        /// Is the Slider enabled.
        /// </summary>
        internal bool IsVisible { get; set; } = true;
        /// <summary>
        /// Is the Slider selected.
        /// </summary>
        internal bool IsSelected { get; set; } = false;
        /// <summary>
        /// Is the Slider interactable.
        /// </summary>
        internal bool IsLocked { get; set; } = false;
        /// <summary>
        /// The normalized value of the Slider.
        /// Value between 0 and 1.
        /// </summary>
        internal int UnitValue { get; set; }
        //internal ISettingsEvent SettingsEvent { get; set; }
        /// <summary>
        /// The color of the Slider.
        /// </summary>
        private Color SliderColor { get; set; }
        /// <summary>
        /// The border color of the Slider.
        /// </summary>
        private Color SliderBorderColor { get; set; }
        public Slider(Block block, int xPosition, int yPosition, int width, int height, int sliderDragRadius, Color sliderColor, Color sliderBorderColor)
        {
            XPosition = block.XPosition + xPosition;
            YPosition = block.YPosition + yPosition;
            Width = width;
            Height = height;
            SliderDragRadius = sliderDragRadius;
            SliderColor = sliderColor;
            SliderBorderColor = sliderBorderColor;
        }
        public bool Enabled() => IsVisible;
        /// <summary>
        ///Updates slider according to mouse input.
        /// </summary>
        private void UpdateSlider()
        {
            if (IsLocked) return;
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
            Raylib.DrawLineEx(new Vector2(XPosition, YPosition), new Vector2(XPosition + Width, YPosition), Height, SliderBorderColor);
            Raylib.DrawCircle(XPosition + UnitValue, YPosition, SliderDragRadius, SliderColor);
        }
    }
}
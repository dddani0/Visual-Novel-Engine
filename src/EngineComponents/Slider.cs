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
        /// The border width of the Slider.
        /// </summary>
        internal int BorderWidth { get; set; }
        /// <summary>
        /// The radius of the Slider's draggable part.
        /// </summary>
        internal int SliderDragRadius { get; set; }
        /// <summary>
        /// Is the Slider enabled.
        /// </summary>
        internal bool IsVisible { get; set; } = true;
        /// <summary>
        /// Is the Slider interactable.
        /// </summary>
        internal bool IsLocked { get; set; } = false;
        /// <summary>
        /// The normalized value of the Slider.
        /// Value between 0 and 1.
        /// </summary>
        internal int UnitValue { get; set; }
        /// <summary>
        /// The color of the Slider's draggable part.
        /// </summary>
        private Color SliderDragColor { get; set; }
        /// <summary>
        /// The color of the Slider.
        /// </summary>
        private Color SliderColor { get; set; }
        /// <summary>
        /// The border color of the Slider.
        /// </summary>
        private Color SliderBorderColor { get; set; }
        private IEvent Event { get; set; }
        /// <summary>
        /// Creates a new Slider.
        /// </summary>
        /// <param name="block">Parent block.</param>
        /// <param name="xPosition">X position offset of the Slider</param>
        /// <param name="yPosition">Y position offset of the slider</param>
        /// <param name="width">Width of the slider</param>
        /// <param name="height">Heigth of the slider</param>
        /// <param name="borderWidth">The width of the border of the slider</param>
        /// <param name="sliderDragRadius">The size of the slider drag component's radius</param>
        /// <param name="sliderDragColor">The color of the slider drag component</param>
        /// <param name="sliderColor">The color of the slider's drag component</param>
        /// <param name="sliderBorderColor">The color of the border of the slider.</param>
        /// <param name="sliderEvent">The event that is triggered when the slider is interacted with.</param>
        public Slider(Block block, int xPosition, int yPosition, int width, int height, int borderWidth, int sliderDragRadius, Color sliderDragColor, Color sliderColor, Color sliderBorderColor, ISettingsEvent sliderEvent)
        {
            XPosition = block.XPosition + xPosition;
            YPosition = block.YPosition + yPosition;
            Width = width;
            Height = height;
            BorderWidth = borderWidth;
            SliderDragRadius = sliderDragRadius;
            SliderDragColor = sliderDragColor;
            SliderColor = sliderColor;
            SliderBorderColor = sliderBorderColor;
            Event = (IEvent)sliderEvent;
        }
        public bool Enabled() => IsVisible;
        /// <summary>
        ///Updates slider according to mouse input.
        /// </summary>
        private void UpdateSlider()
        {
            if (IsLocked) return;
            if (Raylib.IsMouseButtonDown(MouseButton.Left) || Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                //execute event here.
                if (Raylib.GetMousePosition().X >= XPosition - Width && Raylib.GetMousePosition().X <= XPosition + Width &&
                    Raylib.GetMousePosition().Y >= YPosition - Height && Raylib.GetMousePosition().Y <= YPosition + Height)
                {
                    UnitValue = (int)Raylib.GetMousePosition().X - XPosition;
                }
                Event.PerformEvent();
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
            Raylib.DrawRectangle(XPosition, YPosition, Width, Height, SliderColor);
            Raylib.DrawRectangleLinesEx(new Rectangle(XPosition, YPosition, Width, Height), BorderWidth, SliderBorderColor);
            Raylib.DrawCircle(XPosition + UnitValue, YPosition + Height / 2, SliderDragRadius, SliderDragColor);
        }
        /// <summary>
        /// Fetch the value of the Slider.
        /// </summary>
        /// <returns></returns>
        public int FetchUnitValue() => UnitValue;
    }
}
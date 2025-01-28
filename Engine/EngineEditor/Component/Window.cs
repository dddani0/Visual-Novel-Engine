using System.Drawing;
using VisualNovelEngine.Engine.EngineEditor.Interface;

namespace VisualNovelEngine.Engine.EngineEditor.Component
{
    /// <summary>
    /// Represents a window.
    /// </summary>
    public class Window : IWindow
    {
        internal int XPosition { get; set; }
        internal int YPosition { get; set; }
        internal int Width { get; set; }
        internal int Height { get; set; }
        internal int BorderWidth { get; set; }
        internal Color Color { get; set; }
        internal Color BorderColor { get; set; }
        internal Button CloseButton { get; set; }
        internal Button InspectorButton { get; set; }
        //GUI component with option button (?)
        public void Hide()
        {

        }

        public void Show()
        {

        }
    }
}
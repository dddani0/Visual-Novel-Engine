using Raylib_cs;
using VisualNovelEngine.Engine.EngineEditor.Interface;

namespace VisualNovelEngine.Engine.EngineEditor.Component
{
    public class Label : IComponent
    {
        public int XPosition { get; set; }
        public int YPosition { get; set; }
        internal string Text { get; set; }
        internal int Width { get; set; }
        internal int Height { get; set; }
        internal bool Active { get; set; } = true;
        public Label(int xPosition, int yPosition, string text)
        {
            XPosition = xPosition;
            YPosition = yPosition;
            Text = text;
        }

        public void Render()
        {
            if (Active is false) return;
            Raylib.DrawText(Text, XPosition, YPosition, 20, Color.Black);
        }

        public void Update()
        {
            //null
            Width = Text.Length * Raylib.GetFontDefault().BaseSize / 2;
            Height = Raylib.GetFontDefault().BaseSize;
        }
    }
}
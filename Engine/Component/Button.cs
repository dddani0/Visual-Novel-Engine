using Raylib_cs;
using VisualNovelEngine.Engine.Editor.Interface;
using VisualNovelEngine.Engine.Game.Component;

namespace VisualNovelEngine.Engine.Component
{
    public class Button : IButton
    {
        public string Text { get; set; }
        public int Y { get; set; }
        public int X { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        internal bool Hover { get; set; }
        internal bool Visible { get; set; } = true;
        internal bool Active { get; set; } = true;
        public Color Color { get; set; }
        public Color HoverColor { get; set; }
        public ICommand Command { get; set; }
        private Game.Component.Timer Timer { get; set; }
        
        public Button(string text, int x, int y, int width, int height, Color color, Color hoverColor, ICommand command)
        {
            Text = text;
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Color = color;
            HoverColor = hoverColor;
            Command = command;
            Timer = new(1f);
        }

        internal void Render()
        {
            if (Visible is false) return;
            Update();
            Raylib.DrawRectangle(X, Y, Width, Height, Hover ? HoverColor : Color);
            Raylib.DrawText(Text, X + 10, Y, 20, Color.Black);
        }

        internal void Update()
        {
            if (Active is false) return;
            if (Timer.OnCooldown())
            {
                Timer.DecreaseTimer();
                return;
            }
            Hover = Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), new Rectangle(X, Y, Width, Height));
            if (Hover && Raylib.IsMouseButtonPressed(MouseButton.Left)) Click();
        }

        public void Click()
        {
            Command.Execute();
            Timer.Reset();
        }
    }
}
using Raylib_cs;
using VisualNovelEngine.Engine.Editor.Interface;
using VisualNovelEngine.Engine.Game.Component;

namespace VisualNovelEngine.Engine.Component
{
    public class Button : IButton
    {
        internal string Text { get; set; }
        internal int X { get; set; }
        internal int Y { get; set; }
        internal int Width { get; set; }
        internal int Height { get; set; }
        internal bool Hover { get; set; }
        internal bool Visible { get; set; } = true;
        internal bool Active { get; set; } = true;
        internal Color Color { get; set; }
        internal Color HoverColor { get; set; }
        internal ICommand Command { get; set; }
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
            Timer = new(0.1f);
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
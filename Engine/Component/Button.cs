using Raylib_cs;
using VisualNovelEngine.Engine.Editor.Interface;
using VisualNovelEngine.Engine.Game.Component;

namespace VisualNovelEngine.Engine.Component
{
    public class Button : IButton
    {
        /// <summary>
        /// The x position of the button.
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// The y position of the button.
        /// </summary>
        public int Y { get; set; }
        /// <summary>
        /// The width of the button.
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// The height of the button.
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// The height of the button.
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// Whether the button is being hovered over.
        /// </summary>
        internal bool Hover { get; set; }
        /// <summary>
        /// Whether the button is visible.
        /// </summary>
        internal bool Visible { get; set; } = true;
        /// <summary>
        /// Whether the button is active.
        /// </summary>
        internal bool Active { get; set; } = true;
        /// <summary>
        /// The color of the button.
        /// </summary>
        public Color Color { get; set; }
        /// <summary>
        /// The color of the button when hovered.
        /// </summary>
        public Color HoverColor { get; set; }
        /// <summary>
        /// The color of the button when clicked.
        /// </summary>
        public ICommand Command { get; set; }
        /// <summary>
        /// The command to execute when the button is clicked.
        /// </summary>
        private Game.Component.Timer Timer { get; set; }
        /// <summary>
        /// Constructor for the button.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="color"></param>
        /// <param name="hoverColor"></param>
        /// <param name="command"></param>
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
            Timer = new(0.5f);
        }
        /// <summary>
        /// Renders the button.
        /// </summary>
        internal void Render()
        {
            if (Visible is false) return;
            Update();
            Raylib.DrawRectangle(X, Y, Width, Height, Hover ? HoverColor : Color);
            Raylib.DrawText(Text, X + 10, Y, 20, Color.Black);
        }
        /// <summary>
        /// Updates the button.
        /// </summary>
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
        /// <summary>
        /// Executes the command associated with the button.
        /// </summary>
        public void Click()
        {
            Command.Execute();
            Timer.Reset();
        }
    }
}
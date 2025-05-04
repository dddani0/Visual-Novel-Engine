using System.Text.RegularExpressions;
using Raylib_cs;
using VisualNovelEngine.Engine.Editor.Component.Command;
using VisualNovelEngine.Engine.Editor.Interface;

namespace VisualNovelEngine.Engine.Component
{
    /// <summary>
    /// Represents a command to open the path window.
    /// </summary>
    public class InputField : IComponent
    {
        /// <summary>
        /// The editor that the input field is associated with.
        /// </summary>
        public int XPosition { get; set; }
        /// <summary>
        /// The y position of the input field.
        /// </summary>
        public int YPosition { get; set; }
        /// <summary>
        /// The width of the input field.
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// The height of the input field.
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// The button component of the input field.
        /// </summary>
        public Button? Button { get; set; }
        /// <summary>
        /// The text of the input field.
        /// </summary>
        public string Text { get; internal set; }
        /// <summary>
        /// The placeholder of the input field.
        /// </summary>
        public Color Color { get; set; }
        /// <summary>
        /// The color of the input field.
        /// </summary>
        public Color TextColor { get; set; }
        /// <summary>
        /// The color of the text in the input field.
        /// </summary>
        public Color HoverColor { get; set; }
        /// <summary>
        /// The color of the input field when hovered.
        /// </summary>
        internal bool Hover { get; set; }
        /// <summary>
        /// Whether the input field is being edited.
        /// </summary>
        internal bool Active { get; set; }
        /// <summary>
        /// constructor for the input field.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="text"></param>
        /// <param name="buttonText"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="color"></param>
        /// <param name="textColor"></param>
        /// <param name="hoverColor"></param>
        /// <param name="command"></param>
        public InputField(int x, int y, string text, string buttonText, int width, int height, Color color, Color textColor, Color hoverColor, ICommand command)
        {
            XPosition = x;
            YPosition = y;
            Text = text;
            Width = width;
            Height = height;
            Color = color;
            TextColor = textColor;
            HoverColor = hoverColor;
            Button = new Button(buttonText, XPosition, YPosition + 60, 200, 50, Color.White, Color.Gray, command);
        }
        /// <summary>
        /// Creates a new input field.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="text"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="color"></param>
        /// <param name="textColor"></param>
        /// <param name="hoverColor"></param>
        public InputField(int x, int y, string text, int width, int height, Color color, Color textColor, Color hoverColor)
        {
            XPosition = x;
            YPosition = y;
            Text = text;
            Width = width;
            Height = height;
            Color = color;
            TextColor = textColor;
            HoverColor = hoverColor;
        }
        /// <summary>
        /// Renders the input field.
        /// </summary>
        public void Render()
        {
            Update();
            Raylib.DrawRectangle(XPosition, YPosition, Width, Height, Hover ? HoverColor : Color);
            Raylib.DrawText(Text, XPosition + 5, YPosition + 5, 20, TextColor);
            Button?.Render();
        }
        /// <summary>
        /// Updates the input field.
        /// </summary>
        public void Update()
        {
            if (Active)
            {
                if (Raylib.IsKeyPressed(KeyboardKey.Backspace))
                {
                    if (Text.Length > 0)
                    {
                        Text = Text.Remove(Text.Length - 1);
                    }
                }
                else if (Raylib.IsKeyPressed(KeyboardKey.Enter)) Active = false;
                else if (Raylib.GetKeyPressed() > 0 && !Raylib.IsKeyPressed(KeyboardKey.LeftShift) && !Raylib.IsKeyPressed(KeyboardKey.RightShift))
                {
                    Text = Regex.Unescape(Text + ((char)Raylib.GetCharPressed()).ToString()).Replace('\0', ' ');
                }
                if (Raylib.IsMouseButtonPressed(MouseButton.Left)) Active = false;
            }
            else
            {
                Hover = Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), new Rectangle(XPosition, YPosition, Width, Height));
                if (Hover && Raylib.IsMouseButtonPressed(MouseButton.Left))
                {
                    Active = true;
                    Hover = true;
                }
            }
        }
    }
}
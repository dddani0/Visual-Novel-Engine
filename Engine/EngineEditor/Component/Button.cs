using VisualNovelEngine.Engine.EngineEditor.Interface;
using Raylib_cs;
using TemplateGame.Component;
using Timer = TemplateGame.Component.Timer;
using System.Numerics;

namespace VisualNovelEngine.Engine.EngineEditor.Component
{
    /// <summary>
    /// Represents a button inside the editor.
    /// </summary>
    public class Button : IButton, IComponent, ITool
    {
        /// <summary>
        /// Represents the type of button.
        /// </summary>
        public enum ButtonType
        {
            Trigger,
            Hold,
            Toggle
        }
        /// <summary>
        /// Represents the editor.
        /// </summary>
        internal Editor Editor { get; set; }
        /// <summary>
        /// The x position of the button.
        /// </summary>
        public int XPosition { get; set; }
        /// <summary>
        /// The y position of the button.
        /// </summary>
        public int YPosition { get; set; }
        /// <summary>
        /// The text of the button.
        /// </summary>
        internal string Text { get; set; }
        /// <summary>
        /// The width of the button.
        /// </summary>
        internal int Width { get; set; }
        /// <summary>
        /// The height of the button.
        /// </summary>
        internal int Height { get; set; }
        /// <summary>
        /// The border width of the button.
        /// </summary>
        internal int BorderWidth { get; set; }
        /// <summary>
        /// The color of the button.
        /// </summary>
        internal Color Color { get; set; }
        /// <summary>
        /// The border color of the button.
        /// </summary>
        internal Color BorderColor { get; set; }
        /// <summary>
        /// The hover color of the button.
        /// </summary>
        internal Color HoverColor { get; set; }
        /// <summary>
        /// Represents if the button is active.
        /// </summary>
        internal bool Active { get; set; } = true;
        internal bool IsLocked { get; set; } = false;
        /// <summary>
        /// Represents if the button is selected.
        /// </summary>
        internal bool Selected { get; set; }
        /// <summary>
        /// Represents if the button is executed
        /// </summary>
        private bool IsExecuted { get; set; }
        /// <summary>
        /// Represents if the button is hovered.
        /// </summary>
        private bool IsHover { get; set; }
        /// <summary>
        /// Represents the command of the button.
        /// </summary>
        internal ICommand Command { get; set; }
        /// <summary>
        /// Represents the component wihtin the DropDown button
        /// </summary>
        internal IComponent? Component { get; set; }
        /// <summary>
        /// Represents the type of the button.
        /// </summary>
        internal ButtonType Type { get; set; }
        /// <summary>
        /// Represents the timer of the button.
        /// </summary>
        private Timer Timer { get; set; }
        internal TextBox.PositionType PositionType { get; set; }
        internal TemplateGame.Component.Scene.BackgroundOption SceneBackgroundOption { get; set; }
        internal VariableType VariableType { get; set; }
        /// <summary>
        /// Represents a button inside the editor.
        /// </summary>
        /// <param name="editor"></param>
        /// <param name="xPosition"></param>
        /// <param name="yPosition"></param>
        /// <param name="text"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="borderWidth"></param>
        /// <param name="color"></param>
        /// <param name="borderColor"></param>
        /// <param name="hoverColor"></param>
        /// <param name="command"></param>
        /// <param name="type"></param>
        public Button(Editor editor, int xPosition, int yPosition, string text, bool crampText, int width, int height, int borderWidth, Color color, Color borderColor, Color hoverColor, ICommand command, ButtonType type)
        {
            Editor = editor;
            XPosition = xPosition;
            YPosition = yPosition;
            //If length is greater than 5, set the text to the first 4 and add three dots to the end.
            Text = crampText ? text.Length > Editor.ComponentEnabledCharacterCount ? $"{text[..Editor.ComponentEnabledCharacterCount]}..." : text : text;
            Width = width;
            Height = height;
            BorderWidth = borderWidth;
            Color = color;
            BorderColor = borderColor;
            HoverColor = hoverColor;
            Command = command;
            Type = type;
            Timer = new Timer(0.1f);
            Editor.ButtonList.Add(this);
        }

        public Button(Editor editor, DropDown dropDown, string text, Color color, Color borderColor, Color hoverColor, IComponent component)
        {
            Editor = editor;
            XPosition = 0;
            YPosition = 0;
            Text = text.Length > Editor.ComponentEnabledCharacterCount ? $"{text[..Editor.ComponentEnabledCharacterCount]}..." : text;
            Width = 0;
            Height = 0;
            BorderWidth = 0;
            Color = color;
            BorderColor = borderColor;
            HoverColor = hoverColor;
            Component = component;
            Type = ButtonType.Trigger;
            Timer = new Timer(0.1f);
            Active = true;
            Command = new SelectDropDownButtonCommand(dropDown, this);
            Editor.ButtonList.Add(this);
        }

        public void Render()
        {
            if (Active is false) return;
            Update();
            Raylib.DrawRectangle(XPosition, YPosition, Width, Height, IsHover ? HoverColor : Color);
            Raylib.DrawRectangleLines(XPosition, YPosition, Width, Height, BorderColor);
            Raylib.DrawText(Text, XPosition, YPosition, 20, Color.Black);
        }

        public void Update()
        {
            if (IsLocked is true) return;
            IsHover = Raylib.CheckCollisionPointRec(new Vector2(Raylib.GetMouseX(), Raylib.GetMouseY()), new Rectangle(XPosition, YPosition, Width, Height)) && Editor.ButtonList.Any(button => ((Button)button).IsHover is false);
            Click();
            Timer.DecreaseTimer();
        }

        internal void AddCommand(ICommand command)
        {
            Command = command;
        }
        public void Click()
        {
            if (IsHover && Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                switch (Type)
                {
                    case ButtonType.Trigger:
                        if (Timer.OnCooldown()) return;
                        Selected = true;
                        Command.Execute();
                        Selected = false;
                        Timer.ResetTimer();
                        break;
                    case ButtonType.Hold:
                        Selected = !Selected;
                        Command.Execute();
                        break;
                    case ButtonType.Toggle:
                        if (Selected)
                        {
                            Selected = false;
                        }
                        else
                        {
                            Command.Execute();
                            Selected = true;
                        }
                        break;
                }
            }
            else if (IsHover is false && Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                if (Type is ButtonType.Toggle) return;
                Selected = false;
                IsExecuted = false;
            }
        }
    }
}
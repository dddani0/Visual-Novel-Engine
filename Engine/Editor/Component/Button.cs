using VisualNovelEngine.Engine.Editor.Interface;
using Raylib_cs;
using VisualNovelEngine.Engine.Game.Component;
using Timer = VisualNovelEngine.Engine.Game.Component.Timer;
using System.Numerics;
using VisualNovelEngine.Engine.Game.Interface;

namespace VisualNovelEngine.Engine.Editor.Component
{
    /// <summary>
    /// Represents a button in the editor.
    /// </summary>
    public class Button : IButton, IComponent, ITool
    {
        /// <summary>
        /// Type of button
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
        /// <summary>
        /// Is the button non-interactable
        /// </summary>
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
        /// Represents an ingame action which the button can carry.
        /// </summary>
        internal IAction? Action { get; set; }
        /// <summary>
        /// Represents the type of the button.
        /// </summary>
        internal ButtonType Type { get; set; }
        /// <summary>
        /// Represents the timer of the button.
        /// </summary>
        private Timer Timer { get; set; }
        /// <summary>
        /// Represents the position type of a textbox which the button can carry.
        /// </summary>
        internal TextBox.PositionType PositionType { get; set; }
        /// <summary>
        /// Represents the scene background option of a button which the button can carry.
        /// </summary>
        internal VisualNovelEngine.Engine.Game.Component.Scene.BackgroundOption SceneBackgroundOption { get; set; }
        /// <summary>
        /// Represents the variable type of a button which the button can carry.
        /// </summary>
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
        public Button(Editor editor, int xPosition, int yPosition, string text, int width, int height, int borderWidth, Color color, Color borderColor, Color hoverColor, ICommand command, ButtonType type)
        {
            Editor = editor;
            XPosition = xPosition;
            YPosition = yPosition;
            Text = text;
            Width = width;
            Height = height;
            BorderWidth = borderWidth;
            Color = color;
            BorderColor = borderColor;
            HoverColor = hoverColor;
            AddCommand(command);
            Type = type;
            Timer = new Timer(0.1f);
            Editor.ButtonList.Add(this);
        }
        /// <summary>
        /// Represents a button of a dropdown.
        /// </summary>
        /// <param name="editor"></param>
        /// <param name="dropDown"></param>
        /// <param name="text"></param>
        /// <param name="color"></param>
        /// <param name="borderColor"></param>
        /// <param name="hoverColor"></param>
        /// <param name="component"></param>
        public Button(Editor editor, Dropdown dropDown, string text, Color color, Color borderColor, Color hoverColor, IComponent component)
        {
            Editor = editor;
            XPosition = 0;
            YPosition = 0;
            Text = text;
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
            AddCommand(new SelectDropDownButtonCommand(Editor, dropDown, this));
            Editor.ButtonList.Add(this);
        }
        /// <summary>
        /// Display button on the screen.
        /// </summary>
        public void Render()
        {
            if (Active is false) return;
            Update();
            Raylib.DrawRectangle(XPosition, YPosition, Width, Height, IsHover ? HoverColor : Color);
            Raylib.DrawRectangleLines(XPosition, YPosition, Width, Height, BorderColor);
            Raylib.DrawText(Text, XPosition, YPosition, 20, Editor.TextColor);
        }
        /// <summary>
        /// Update button.
        /// </summary>
        public void Update()
        {
            if (IsLocked is true) return;
            IsHover = Raylib.CheckCollisionPointRec(new Vector2(Raylib.GetMouseX(), Raylib.GetMouseY()), new Rectangle(XPosition, YPosition, Width, Height)) && Editor.ButtonList.Any(button => ((Button)button).IsHover is false);
            if (Editor.ButtonList.Any(button => ((Button)button).IsHover) && IsHover is false) return;
            Click();
            Timer.DecreaseTimer();
        }
        /// <summary>
        /// Attribute a command to the button.
        /// </summary>
        /// <param name="command"></param>
        internal void AddCommand(ICommand command)
        {
            Command = command;
        }
        /// <summary>
        /// Execute the button's command.
        /// </summary>
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
                        Timer.Reset();
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
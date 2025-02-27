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
        public enum ButtonType
        {
            Trigger,
            Hold,
            Toggle
        }
        public int XPosition { get; set; }
        public int YPosition { get; set; }
        internal string Text { get; set; }
        internal int Width { get; set; }
        internal int Height { get; set; }
        internal int BorderWidth { get; set; }
        internal Color Color { get; set; }
        internal Color BorderColor { get; set; }
        internal Color HoverColor { get; set; }
        internal bool Active { get; set; } = true;
        internal bool Selected { get; set; }
        private bool IsExecuted { get; set; }
        private bool IsHover { get; set; }
        internal Editor Editor { get; set; }
        internal ICommand Command { get; set; }
        internal IComponent? Component { get; set; }
        internal ButtonType Type { get; set; }
        private Timer Timer { get; set; }
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
            //If length is greater than 5, set the text to the first 4 and add three dots to the end.
            Text = text.Length > Editor.ComponentEnabledCharacterCount ? $"{text[..Editor.ComponentEnabledCharacterCount]}..." : text;
            Width = width;
            Height = height;
            BorderWidth = borderWidth;
            Color = color;
            BorderColor = borderColor;
            HoverColor = hoverColor;
            Command = command;
            Type = type;
            Timer = new Timer(0.1f);
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
            IsHover = Raylib.CheckCollisionPointRec(new Vector2(Raylib.GetMouseX(), Raylib.GetMouseY()), new Rectangle(XPosition, YPosition, Width, Height));
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
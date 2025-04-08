using Raylib_cs;
using VisualNovelEngine.Engine.Component.Command;
using VisualNovelEngine.Engine.Editor.Component.Command;
using VisualNovelEngine.Engine.Editor.Interface;

namespace VisualNovelEngine.Engine.Component
{
    enum WindowType
    {
        NewProject,
        ImportProject,
        PlayProject
    }
    /// <summary>
    /// Represents a command to open the path window.
    /// </summary>
    class Window : IWindow
    {
        Engine Engine { get; set; }
        internal WindowType Type { get; set; }
        internal int XPosition { get; set; }
        internal int YPosition { get; set; }
        internal int Width { get; set; }
        internal int Height { get; set; }
        internal Button CloseButton { get; set; }
        internal InputField ProjectPathInputField { get; set; }
        internal InputField? NameInputField { get; set; }
        internal InputField? VariableInputField { get; set; }
        public Window(Engine engine, int x, int y, int width, int height, WindowType type)
        {
            Engine = engine;
            Type = type;
            XPosition = x;
            YPosition = y;
            Width = width;
            Height = height;
            switch (Type)
            {
                case WindowType.NewProject:
                    NameInputField = new(XPosition + (Width / 2 - 300 / 2), YPosition + (Height / 2 - 2 * 50), "Projekt név", 300, 50, Color.White, Color.Black, Color.Gray);
                    CloseButton = new Button("X", XPosition + Width - 20, YPosition, 20, 20, Color.Red, Color.DarkBrown, new CloseWindowCommand(Engine));
                    ProjectPathInputField = new InputField(XPosition + (Width / 2 - 300 / 2), YPosition + (Height / 2 - 50 / 2), "D:/", "Kész", 300, 50, Color.White, Color.Black, Color.Gray, new CreateNewProjectCommand(Engine));
                    break;
                case WindowType.ImportProject:
                    CloseButton = new Button("X", XPosition + Width - 20, YPosition, 20, 20, Color.Red, Color.DarkBrown, new CloseWindowCommand(Engine));
                    ProjectPathInputField = new InputField(XPosition + (Width / 2 - 300 / 2), YPosition + (Height / 2 - 50 / 2), "D:/", "Betöltés", 300, 50, Color.White, Color.Black, Color.Gray, new ImportNewProjectCommand(Engine));
                    break;
                case WindowType.PlayProject:
                    CloseButton = new Button("X", XPosition + Width - 20, YPosition, 20, 20, Color.Red, Color.DarkBrown, new CloseWindowCommand(Engine));
                    VariableInputField = new InputField(XPosition + (Width / 2 - 300 / 2), YPosition + (Height / 2 - 2 * 50), "Változó név", 300, 50, Color.White, Color.Black, Color.Gray);
                    ProjectPathInputField = new InputField(XPosition + (Width / 2 - 300 / 2), YPosition + (Height / 2 - 50 / 2), "D:/", "Betöltés", 300, 50, Color.White, Color.Black, Color.Gray, new PlayGameCommand(Engine));
                    break;
            }
        }

        public void Show()
        {
            Raylib.DrawRectangle(XPosition, YPosition, Width, Height, Color.Black);
            //Draw white border
            Raylib.DrawRectangleLines(XPosition, YPosition, Width, Height, Color.White);
            CloseButton.Render();
            NameInputField?.Render();
            VariableInputField?.Render();
            ProjectPathInputField.Render();
        }
    }
}
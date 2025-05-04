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
        /// <summary>
        /// The editor that the window is associated with.
        /// </summary>
        Engine Engine { get; set; }
        /// <summary>
        /// The x position of the window.
        /// </summary>
        internal WindowType Type { get; set; }
        /// <summary>
        /// The y position of the window.
        /// </summary>
        internal int XPosition { get; set; }
        /// <summary>
        /// The y position of the window.
        /// </summary>
        internal int YPosition { get; set; }
        /// <summary>
        /// The width of the window.
        /// </summary>
        internal int Width { get; set; }
        /// <summary>
        /// The height of the window.
        /// </summary>
        internal int Height { get; set; }
        /// <summary>
        /// The button component of the window.
        /// </summary>
        internal Button CloseButton { get; set; }
        /// <summary>
        /// The text of the window.
        /// </summary>
        internal InputField ProjectPathInputField { get; set; }
        /// <summary>
        /// The text of the window.
        /// </summary>
        internal InputField? NameInputField { get; set; }
        /// <summary>
        /// The placeholder of the window.
        /// </summary>
        /// <param name="engine"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="type"></param>
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
                    NameInputField = new(XPosition + (Width / 2 - 300 / 2), YPosition + (Height / 2 - 2 * 50), "Name", 300, 50, Color.White, Color.Black, Color.Gray);
                    CloseButton = new Button("X", XPosition + Width - 20, YPosition, 20, 20, Color.Red, Color.DarkBrown, new CloseWindowCommand(Engine));
                    ProjectPathInputField = new InputField(XPosition + (Width / 2 - 300 / 2), YPosition + (Height / 2 - 50 / 2), "D:/", "Create", 300, 50, Color.White, Color.Black, Color.Gray, new CreateNewProjectCommand(Engine));
                    break;
                case WindowType.ImportProject:
                    CloseButton = new Button("X", XPosition + Width - 20, YPosition, 20, 20, Color.Red, Color.DarkBrown, new CloseWindowCommand(Engine));
                    ProjectPathInputField = new InputField(XPosition + (Width / 2 - 300 / 2), YPosition + (Height / 2 - 50 / 2), "D:/", "Load", 300, 50, Color.White, Color.Black, Color.Gray, new ImportNewProjectCommand(Engine));
                    break;
                case WindowType.PlayProject:
                    CloseButton = new Button("X", XPosition + Width - 20, YPosition, 20, 20, Color.Red, Color.DarkBrown, new CloseWindowCommand(Engine));
                    ProjectPathInputField = new InputField(XPosition + (Width / 2 - 300 / 2), YPosition + (Height / 2 - 50 / 2), "D:/", "Load", 300, 50, Color.White, Color.Black, Color.Gray, new PlayGameCommand(Engine));
                    break;
            }
        }
        /// <summary>
        /// Shows the window.
        /// </summary>
        public void Show()
        {
            Raylib.DrawRectangle(XPosition, YPosition, Width, Height, Color.Black);
            //Draw white border
            Raylib.DrawRectangleLines(XPosition, YPosition, Width, Height, Color.White);
            CloseButton.Render();
            NameInputField?.Render();
            ProjectPathInputField.Render();
        }
    }
}
using VisualNovelEngine.Engine.Editor.Interface;

namespace VisualNovelEngine.Engine.Component.Command
{
    /// <summary>
    /// Represents a command to open the path window.
    /// </summary>
    class OpenWindowCommand : ICommand
    {
        Engine Engine { get; set; }
        Window Window { get; set; }
        public OpenWindowCommand(Engine engine, WindowType type)
        {
            Engine = engine;
            Window = new Window(Engine, Engine.Width / 2 - 200, Engine.Height / 2 - 100, 400, 200, type);
        }

        public void Execute()
        {
            Engine.Window = Window;
            //Disable buttons
            Engine.NewProjectButton.Active = false;
            Engine.ImportProjectButton.Active = false;
            Engine.OpenGameProject.Active = false;
        }
    }
}
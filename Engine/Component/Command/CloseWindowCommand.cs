using VisualNovelEngine.Engine.Editor.Interface;

namespace VisualNovelEngine.Engine.Component.Command
{
    /// <summary>
    /// Represents a command to open the path window.
    /// </summary>
    class CloseWindowCommand : ICommand
    {
        Engine Engine { get; set; }
        public CloseWindowCommand(Engine engine)
        {
            Engine = engine;
        }

        public void Execute()
        {
            Engine.Window = null;
            //Disable buttons
            Engine.NewProject.Active = true;
            Engine.ImportProject.Active = true;
            Engine.ImportBuild.Active = true;
        }
    }
}
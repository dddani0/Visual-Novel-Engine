using VisualNovelEngine.Engine.Editor.Interface;
using VisualNovelEngine.Engine.Editor.Component;

namespace VisualNovelEngine.Engine.Component.Command
{
    /// <summary>
    /// /// Represents a command to import a new project.
    /// </summary>
    public class ImportNewProjectCommand : ICommand
    {
        Engine Engine { get; set; }

        public ImportNewProjectCommand(Engine engine)
        {
            Engine = engine;
        }

        public void Execute() => Engine.Editor = Engine.CreateEditor(Engine.Window.ProjectPathInputField.Text);
    }
}
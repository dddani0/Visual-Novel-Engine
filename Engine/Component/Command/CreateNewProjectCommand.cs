using VisualNovelEngine.Engine.Editor.Interface;

namespace VisualNovelEngine.Engine.Component.Command
{
    /// <summary>
    /// /// Represents a command to create a new project.
    /// </summary>
    class CreateNewProjectCommand : ICommand
    {
        Engine Engine { get; set; }
        public CreateNewProjectCommand(Engine engine)
        {
            Engine = engine;
        }
        public void Execute() => Engine.Editor = Engine.CreateEditor(Engine.Window.ProjectPathInputField.Text, Engine.Window.NameInputField.Text);
    }
}
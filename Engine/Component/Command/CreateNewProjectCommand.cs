using VisualNovelEngine.Engine.Editor.Interface;

namespace VisualNovelEngine.Engine.Component.Command
{
    class CreateNewProjectCommand : ICommand
    {
        Engine Engine { get; set; }
        public CreateNewProjectCommand(Engine engine)
        {
            Engine = engine;
        }
        public void Execute() => Engine.Editor = Engine.CreateEditor(Engine.Window.NameInputField.Text, Engine.Window.ProjectPathInputField.Text);

    }
}
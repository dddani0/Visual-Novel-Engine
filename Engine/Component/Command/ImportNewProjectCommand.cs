using VisualNovelEngine.Engine.Editor.Interface;
using VisualNovelEngine.Engine.Editor.Component;

namespace VisualNovelEngine.Engine.Component.Command
{
    public class ImportNewProjectCommand : ICommand
    {
        Engine Engine { get; set; }

        public ImportNewProjectCommand(Engine engine)
        {
            Engine = engine;
        }

        public void Execute()
        {
            Engine.Editor = new(Engine, Engine.Window.ProjectPathInputField.Text);
            Engine.ChangeState(EngineState.Editor);
        }
    }
}
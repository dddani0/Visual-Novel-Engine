using EngineEditor.Interface;
using TemplateGame.Component;
using TemplateGame.Interface;

namespace EngineEditor.Component.Command
{
    /// <summary>
    /// Represents an empty command.
    /// </summary>
    public class CreateComponentCommand : ICommand
    {
        private readonly Editor Editor;
        private readonly Component Component;
        private string componentName;

        public CreateComponentCommand(Editor editor, IPermanentRenderingObject component)
        {
            Editor = editor;
            componentName = component switch
            {
                InputField => $"New InputField({Editor.IDGenerator.GenerateID()})",
                Sprite => $"New Sprite({Editor.IDGenerator.GenerateID()})"
            };
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        public void Execute()
        {
            Editor.ComponentList.Add(Component);
        }
    }
}
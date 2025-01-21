using EngineEditor.Interface;

namespace EngineEditor.Component.Command
{
    /// <summary>
    /// Represents an empty command.
    /// </summary>
    public class CreateComponentCommand : ICommand
    {
        private readonly Editor Editor;
        private readonly Component Component;

        public CreateComponentCommand(Editor editor, Component component)
        {
            Editor = editor;
            Component = component;
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        public void Execute()
        {
            //Editor.ComponentGroupList.Add(Component);
        }
    }
}
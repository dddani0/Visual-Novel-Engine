using TemplateGame.Component;
using VisualNovelEngine.Engine.Editor.Component;
using VisualNovelEngine.Engine.Editor.Interface;

namespace Engine.EngineEditor.Component.Command
{
    /// <summary>
    /// Command to create a new variable.
    /// </summary>
    class CreateVariableCommand : ICommand
    {
        Editor Editor { get; set; }
        internal string Name;
        internal string Value;
        internal VariableType Type;
        public CreateVariableCommand(Editor editor, string value, VariableType type)
        {
            Editor = editor;
            Value = value;
            Type = type;
        }
        public void Execute()
        {
            Name = $"New Variable({Editor.GameVariables.Count})";
            Editor.GameVariables.Add(new Variable(Name, Value, Type));
            foreach (MiniWindow item in Editor.MiniWindow)
            {
                if (item.HasVariableComponent is true) item.FetchVariables();
            }
        }

    }
}
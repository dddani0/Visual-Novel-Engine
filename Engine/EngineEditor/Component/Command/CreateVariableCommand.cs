using TemplateGame.Component;
using VisualNovelEngine.Engine.EngineEditor.Component;
using VisualNovelEngine.Engine.EngineEditor.Interface;

namespace Engine.EngineEditor.Component.Command
{
    /// <summary>
    /// Command to create a new variable.
    /// </summary>
    class CreateVariableCommand : ICommand
    {
        Editor Editor { get; set; }
        readonly string Name;
        readonly string Value;
        readonly VariableType Type;
        public CreateVariableCommand(Editor editor, string name, string value, VariableType type)
        {
            Editor = editor;
            Name = name;
            Value = value;
            Type = type;
        }
        public void Execute()
        {
            if (Editor.GameVariables.Any(v => v.Name == Name))
            {
                throw new System.Exception("Variable with the same name already exists!");
            }
            Editor.GameVariables.Add(new Variable(Name, Value, Type));
        }

    }
}
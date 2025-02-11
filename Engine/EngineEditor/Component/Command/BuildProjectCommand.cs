using VisualNovelEngine.Engine.EngineEditor.Interface;

namespace VisualNovelEngine.Engine.EngineEditor.Component.Command
{
    /// <summary>
    /// 
    /// </summary>
    public class BuildProjectCommand : ICommand
    {
        Editor Editor { get; set; }
        public BuildProjectCommand(Editor editor)
        {
            Editor = editor;
        }
        public void Execute()
        {
            Editor.Build();
        }
    }
}
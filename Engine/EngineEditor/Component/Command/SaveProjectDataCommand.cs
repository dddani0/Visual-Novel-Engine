using System.Text.Json;
using System.Text.Json.Nodes;
using TemplateGame.Component;
using VisualNovelEngine.Engine.EngineEditor.Interface;

namespace VisualNovelEngine.Engine.EngineEditor.Component.Command
{
    /// <summary>
    /// 
    /// </summary>
    public class SaveProjectDataCommand : ICommand
    {
        Editor Editor { get; set; }
        public SaveProjectDataCommand(Editor editor)
        {
            Editor = editor;
        }
        public void Execute()
        {
            Editor.Save();
        }
    }
}
using System.Text.Json;
using System.Text.Json.Nodes;
using TemplateGame.Component;
using VisualNovelEngine.Engine.Editor.Interface;

namespace VisualNovelEngine.Engine.Editor.Component.Command
{
    /// <summary>
    /// Save the project data
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
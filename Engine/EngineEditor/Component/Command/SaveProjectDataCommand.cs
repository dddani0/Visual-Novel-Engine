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
        EditorImporter EditorImporter { get; set; }
        public SaveProjectDataCommand(Editor editor)
        {
            Editor = editor;
            EditorImporter = Editor.EditorImporter;
        }
        public void Execute()
        {
            var editorJson = JsonSerializer.Serialize(EditorImporter.ExportEditorData(Editor), new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(Editor.SaveFilePath, editorJson);
        }
    }
}
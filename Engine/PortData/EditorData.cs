using System.ComponentModel;
using System.Text.Json.Serialization;

namespace VisualNovelEngine.Engine.PortData
{
    public class EditorEXIM
    {
        [JsonPropertyName("ID")]
        public int ID { get; set; }
        [JsonPropertyName("ProjectName")]
        public required string ProjectName { get; set; }
        [JsonPropertyName("ToolBar")]
        public required GroupEXIM ToolBar { get; set; }
        [JsonPropertyName("Scenes")]
        public SceneEXIM[] Scenes { get; set; }
    }
    public class GroupEXIM
    {
        [JsonPropertyName("XPosition")]
        public int XPosition { get; set; }
        [JsonPropertyName("YPosition")]
        public int YPosition { get; set; }
        [JsonPropertyName("Width")]
        public int Width { get; set; }
        [JsonPropertyName("Height")]
        public int Height { get; set; }
        [JsonPropertyName("BorderWidth")]
        public int BorderWidth { get; set; }
        [JsonPropertyName("Buttons")]
        public ButtonEXIM[]? Buttons { get; set; }
        [JsonPropertyName("Components")]
        public ComponentEXIM[]? Components { get; set; }
    }
    public class ButtonEXIM
    {
        [JsonPropertyName("XPosition")]
        public int XPosition { get; set; }
        [JsonPropertyName("YPosition")]
        public int YPosition { get; set; }
        [JsonPropertyName("Text")]
        public required string Text { get; set; }
        [JsonPropertyName("Command")]
        public required CommandImport Command { get; set; }
        [JsonPropertyName("Type")]
        public int Type { get; set; }
    }
    public class ComponentEXIM
    {
        [JsonPropertyName("ID")]
        public int ID { get; set; }
        [JsonPropertyName("Name")]
        public required string Name { get; set; }
        [JsonPropertyName("XPosition")]
        public int XPosition { get; set; }
        [JsonPropertyName("YPosition")]
        public int YPosition { get; set; }
        [JsonPropertyName("RenderingObject")]
        public required RenderingObjectEXIM RenderingObject { get; set; }
    }
    public class RenderingObjectEXIM
    {
        [JsonPropertyName("Type")]
        public required string Type { get; set; }
        [JsonPropertyName("Path")]
        public required string Path { get; set; }
    }
    public class CommandImport
    {
        [JsonPropertyName("Type")]
        public required string Type { get; set; }
        [JsonPropertyName("RenderingObjectType")]
        public int RenderingObjectType { get; set; }
        [JsonPropertyName("EnabledRowComponentCount")]
        public int EnabledRowComponentCount { get; set; }
        [JsonPropertyName("XPosition")]
        public int XPosition { get; set; }
        [JsonPropertyName("YPosition")]
        public int YPosition { get; set; }
        [JsonPropertyName("ParentButtonName")]
        public string? DependentButton { get; set; }
        [JsonPropertyName("Buttons")]
        public ButtonEXIM[]? Buttons { get; set; }
        [JsonPropertyName("ButtonDependency")]
        public ButtonEXIM? ButtonDependency { get; set; }
    }
    public class SceneEXIM
    {
        [JsonPropertyName("Name")]
        public required string Name { get; set; }
        [JsonPropertyName("Components")]
        public ComponentEXIM[]? Components { get; set; }
        [JsonPropertyName("GroupList")]
        public GroupEXIM[]? GroupList { get; set; }
    }
}
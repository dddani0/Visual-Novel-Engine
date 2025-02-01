using System.Text.Json.Serialization;

namespace VisualNovelEngine.Engine.PortData
{
    public class EditorImport
    {
        [JsonPropertyName("ProjectName")]
        public required string ProjectName { get; set; }
        [JsonPropertyName("ToolBar")]
        public required GroupImport ToolBar { get; set; }
        [JsonPropertyName("Scenes")]
        public SceneImport[] Scenes { get; set; }
    }
    public class GroupImport
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
        public ButtonImport[]? Buttons { get; set; }
        [JsonPropertyName("Components")]
        public ButtonImport[]? Components { get; set; }
    }
    public class ButtonImport
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
    public class ComponentImport
    {

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
        public string? ParentButtonName { get; set; }
        [JsonPropertyName("Buttons")]
        public ButtonImport[]? Buttons { get; set; }
        [JsonPropertyName("Components")]
        public ComponentImport[]? Components { get; set; }
        [JsonPropertyName("ButtonDependency")]
        public ButtonImport? ButtonDependency { get; set; }
    }
    public class SceneImport
    {
        [JsonPropertyName("Name")]
        public required string Name { get; set; }
        [JsonPropertyName("Components")]
        public ComponentImport[]? Components { get; set; }
        [JsonPropertyName("GroupList")]
        public GroupImport[]? GroupList { get; set; }
    }
}
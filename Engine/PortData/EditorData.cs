using System.Text.Json.Serialization;

namespace VisualNovelEngine.Engine.PortData
{
    public class EditorImport
    {
        [JsonPropertyName("ToolBar")]
        public required GroupImport ToolBar { get; set; }
    }
    public class EditorConfigurationImport
    {
        [JsonPropertyName("ComponentWidth")]
        public required int ComponentWidth { get; set; }
        [JsonPropertyName("ComponentHeight")]
        public required int ComponentHeight { get; set; }
        [JsonPropertyName("ComponentBorderWidth")]
        public required int ComponentBorderWidth { get; set; }
        [JsonPropertyName("ComponentEnabledCharacterCount")]
        public required int ComponentEnabledCharacterCount { get; set; }
        [JsonPropertyName("BaseColor")]
        public required int[] BaseColor { get; set; }
        [JsonPropertyName("BorderColor")]
        public required int[] BorderColor { get; set; }
        [JsonPropertyName("TextColor")]
        public required int[] TextColor { get; set; }
        [JsonPropertyName("HoverColor")]
        public required int[] HoverColor { get; set; }
        [JsonPropertyName("InspectorButtonBaseColor")]
        public required int[] InspectorButtonBaseColor { get; set; }
        [JsonPropertyName("InspectorButtonBorderColor")]
        public required int[] InspectorButtonBorderColor { get; set; }
        [JsonPropertyName("InspectorButtonHoverColor")]
        public required int[] InspectorButtonHoverColor { get; set; }
        [JsonPropertyName("CloseButtonBaseColor")]
        public required int[] CloseButtonBaseColor { get; set; }
        [JsonPropertyName("CloseButtonBorderColor")]
        public required int[] CloseButtonBorderColor { get; set; }
        [JsonPropertyName("CloseButtonHoverColor")]
        public required int[] CloseButtonHoverColor { get; set; }
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
        [JsonPropertyName("ITool")]
        public ButtonImport[] ITool { get; set; }
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
    public class CommandImport
    {
        [JsonPropertyName("Type")]
        public required string Type { get; set; }
        [JsonPropertyName("RenderingObjectType")]
        public int RenderingObjectType { get; set; }
    }
}
using System.Text.Json.Serialization;

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
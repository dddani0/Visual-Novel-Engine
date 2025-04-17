using System.Text.Json.Serialization;

public class EditorPreferencesImport
{
    [JsonPropertyName("ComponentWidth")]
    public required int ComponentWidth { get; set; }
    [JsonPropertyName("ComponentHeight")]
    public required int ComponentHeight { get; set; }
    [JsonPropertyName("ComponentBorderWidth")]
    public required int ComponentBorderWidth { get; set; }
    [JsonPropertyName("ButtonWidth")]
    public required int ButtonWidth { get; set; }
    [JsonPropertyName("ButtonHeight")]
    public required int ButtonHeight { get; set; }
    [JsonPropertyName("ButtonBorderWidth")]
    public required int ButtonBorderWidth { get; set; }
    [JsonPropertyName("SmallButtonWidth")]
    public required int SmallButtonWidth { get; set; }
    [JsonPropertyName("SmallButtonHeight")]
    public required int SmallButtonHeight { get; set; }
    [JsonPropertyName("SmallButtonBorderWidth")]
    public required int SmallButtonBorderWidth { get; set; }
    [JsonPropertyName("SideButtonWidth")]
    public required int SideButtonWidth { get; set; }
    [JsonPropertyName("SideButtonHeight")]
    public required int SideButtonHeight { get; set; }
    [JsonPropertyName("SideButtonBorderWidth")]
    public required int SideButtonBorderWidth { get; set; }
    [JsonPropertyName("ComponentEnabledCharacterCount")]
    public required int ComponentEnabledCharacterCount { get; set; }
    [JsonPropertyName("InspectorWindowWidth")]
    public required int InspectorWidth { get; set; }
    [JsonPropertyName("InspectorWindowHeight")]
    public required int InspectorHeight { get; set; }
    [JsonPropertyName("InspectorWindowBorderWidth")]
    public required int InspectorBorderWidth { get; set; }
    [JsonPropertyName("MiniWindowWidth")]
    public required int MiniWindowWidth { get; set; }
    [JsonPropertyName("MiniWindowHeight")]
    public required int MiniWindowHeight { get; set; }
    [JsonPropertyName("MiniWindowBorderWidth")]
    public required int MiniWindowBorderWidth { get; set; }
    [JsonPropertyName("BaseColor")]
    public required int[] BaseColor { get; set; }
    [JsonPropertyName("BorderColor")]
    public required int[] BorderColor { get; set; }
    [JsonPropertyName("TextColor")]
    public required int[] TextColor { get; set; }
    [JsonPropertyName("HoverColor")]
    public required int[] HoverColor { get; set; }
    [JsonPropertyName("EditorColor")]
    public required int[] EditorColor { get; set; }
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
using System.Text.Json.Serialization;

namespace VisualNovelEngine.Engine.PortData
{
    /// <summary>
    /// The "GameExim" class is a helper class to import/export the game settings to or from a JSON file.
    /// </summary>
    public class GameExim
    {
        [JsonPropertyName("Title")]
        public required string Title { get; set; }
        [JsonPropertyName("WindowWidth")]
        public int WindowWidth { get; set; }
        [JsonPropertyName("WindowHeight")]
        public int WindowHeigth { get; set; }
    }
    /// <summary>
    /// The "SceneExim" class is a helper class to import/export the list of scenes to or from a JSON file.
    /// </summary>
    internal class SceneExim
    {
        [JsonPropertyName("Name")]
        public required string Name { get; set; }
        [JsonPropertyName("ID")]
        public required long ID { get; set; } //remove setter!
        [JsonPropertyName("Background")]
        public required string Background { get; set; }
        [JsonPropertyName("SolidColor")]
        public int[]? SolidColor { get; set; }
        [JsonPropertyName("GradientColor")]
        public int[]? GradientColor { get; set; }
        [JsonPropertyName("ImageTexture")]
        public string? ImageTexture { get; set; }
        [JsonPropertyName("ActionList")]
        public ActionExim[]? ActionList { get; set; }
    }
    /// <summary>
    /// The "ActionExim" class is a helper class to import/export the list of actions to or from a JSON file.
    /// </summary>
    public class ActionExim
    {
        [JsonPropertyName("Type")]
        public required string Type { get; set; }
        [JsonPropertyName("Sprite")]
        public SpriteExim? Sprite { get; set; }
        [JsonPropertyName("TextBox")]
        public TextBoxExIm? TextBox { get; set; }
        [JsonPropertyName("TintColor")]
        public int[]? TintColor { get; set; }
        [JsonPropertyName("SceneID")]
        public long? SceneID { get; set; }
        [JsonPropertyName("VariableName")]
        public string? VariableName { get; set; }
        [JsonPropertyName("TriggerVariableName")]
        public string? TriggerVariableName { get; set; }
        [JsonPropertyName("VariableValue")]
        public string? VariableValue { get; set; }
        [JsonPropertyName("VariableType")]
        public int? VariableType { get; set; }
        [JsonPropertyName("ImpendingVariable")]
        public string? ImpendingVariableName { get; set; }
        [JsonPropertyName("Menu")]
        public MenuExim? Menu { get; set; }
        [JsonPropertyName("StaticMenu")]
        public MenuExim? StaticMenu { get; set; }
        [JsonPropertyName("BlockComponentID")]
        public long? BlockComponentID { get; set; }
        [JsonPropertyName("DisablingMenuID")]
        public long? DisablingMenuID { get; set; }
        [JsonPropertyName("EnablingMenuID")]
        public long? EnablingMenuID { get; set; }
    }
    /// <summary>
    /// The "VariableExim" class is a helper class to import/export the list of variables to or from a JSON file.
    /// </summary>
    public class VariableExim
    {
        [JsonPropertyName("Name")]
        public required string Name { get; set; }
        [JsonPropertyName("Value")]
        public required string Value { get; set; }
        [JsonPropertyName("Type")]
        public required int Type { get; set; }
    }
    /// <summary>
    /// The "MenuExim" class is a helper class to import/export the list of menus to or from a JSON file.
    /// </summary>
    public class MenuExim
    {
        [JsonPropertyName("ID")]
        public long ID { get; set; }
        [JsonPropertyName("XPosition")]
        public int? XPosition { get; set; }
        [JsonPropertyName("YPosition")]
        public int? YPosition { get; set; }
        [JsonPropertyName("Width")]
        public int? Width { get; set; }
        [JsonPropertyName("Height")]
        public int? Height { get; set; }
        [JsonPropertyName("FullScreen")]
        public string? FullScreen { get; set; }
        [JsonPropertyName("Static")]
        public string? Static { get; set; }
        [JsonPropertyName("BlockList")]
        public BlockExim[]? BlockList { get; set; }
        [JsonPropertyName("Color")]
        public int[]? Color { get; set; }
        [JsonPropertyName("BorderColor")]
        public int[]? BorderColor { get; set; }
    }
    /// <summary>
    /// The "BlockExim" class is a helper class to import/export the list of blocks to or from a JSON file.
    /// </summary>
    public class BlockExim
    {
        [JsonPropertyName("ID")]
        public required int ID { get; set; }
        [JsonPropertyName("XPosition")]
        public int XPosition { get; set; }
        [JsonPropertyName("YPosition")]
        public int YPosition { get; set; }
        [JsonPropertyName("Button")]
        public ButtonComponentExIm? Button { get; set; }
        [JsonPropertyName("StaticButton")]
        public ButtonComponentExIm? StaticButton { get; set; }
        [JsonPropertyName("InputField")]
        public InputFieldExim? InputField { get; set; }
        [JsonPropertyName("StaticInputField")]
        public InputFieldExim? StaticInputField { get; set; }
        [JsonPropertyName("DropBox")]
        public DropBoxExim? DropBox { get; set; }
        [JsonPropertyName("Slider")]
        public SliderExim? Slider { get; set; }
        [JsonPropertyName("Toggle")]
        public ToggleExim? Toggle { get; set; }
        [JsonPropertyName("TextField")]
        public TextFieldExim? TextField { get; set; }
        [JsonPropertyName("Sprite")]
        public SpriteExim? Sprite { get; set; }
    }
    public class TextBoxExIm
    {
        [JsonPropertyName("CharactersPerSecond")]
        public double? CharactersPerSecond { get; set; }
        [JsonPropertyName("Font")] //Need font importer class
        public string Font { get; set; }
        [JsonPropertyName("Color")]
        public int[]? Color { get; set; }
        [JsonPropertyName("BorderColor")]
        public int[]? BorderColor { get; set; }
        [JsonPropertyName("PositionType")]
        public int? PositionType { get; set; }
        [JsonPropertyName("HorizontalTextMargin")]
        public int? HorizontalTextMargin { get; set; }
        [JsonPropertyName("VerticalTextMargin")]
        public int? VerticalTextMargin { get; set; }
        [JsonPropertyName("WordWrap")]
        public bool? WordWrap { get; set; }
        [JsonPropertyName("Title")]
        public string? Title { get; set; }
        [JsonPropertyName("Content")]
        public string[]? Content { get; set; }
    }

    /// <summary>
    /// The "ButtonComponentExim" class is a helper class to import/export the Button component's data to or from a JSON file.
    /// </summary>
    public class ButtonComponentExIm
    {
        [JsonPropertyName("XPosition")]
        public required int XPosition { get; set; }
        [JsonPropertyName("YPosition")]
        public required int YPosition { get; set; }
        [JsonPropertyName("Width")]
        public required int Width { get; set; }
        [JsonPropertyName("Height")]
        public required int Height { get; set; }
        [JsonPropertyName("Text")]
        public required string Text { get; set; }
        [JsonPropertyName("BorderWidth")]
        public int BorderWidth { get; set; }
        [JsonPropertyName("TextColor")]
        public required int[] TextColor { get; set; }
        [JsonPropertyName("Color")]
        public required int[] Color { get; set; }
        [JsonPropertyName("BorderColor")]
        public required int[] BorderColor { get; set; }
        [JsonPropertyName("HoverColor")]
        public required int[] HoverColor { get; set; }
        [JsonPropertyName("Action")]
        public required ActionExim Action { get; set; }
    }
    /// <summary>
    /// The "InputFieldExim" class is a helper class to import/export the TextBox component's data to or from a JSON file.
    /// </summary>
    public class InputFieldExim
    {
        [JsonPropertyName("XPosition")]
        public int XPosition { get; set; }
        [JsonPropertyName("YPosition")]
        public int YPosition { get; set; }
        [JsonPropertyName("ButtonYOffset")]
        public int ButtonYOffset { get; set; }
        [JsonPropertyName("Width")]
        public int Width { get; set; }
        [JsonPropertyName("Height")]
        public int Height { get; set; }
        [JsonPropertyName("PlaceholderText")]
        public required string PlaceholderText { get; set; }
        [JsonPropertyName("ButtonText")]
        public required string ButtonText { get; set; }
        [JsonPropertyName("ButtonAction")]
        public required ActionExim ButtonAction { get; set; }
        [JsonPropertyName("BorderWidth")]
        public int BorderWidth { get; set; }
        [JsonPropertyName("Color")]
        public required int[] Color { get; set; }
        [JsonPropertyName("BorderColor")]
        public required int[] BorderColor { get; set; }
        [JsonPropertyName("HoverColor")]
        public required int[] HoverColor { get; set; }
        [JsonPropertyName("SelectedColor")]
        public required int[] SelectedColor { get; set; }
    }
    /// <summary>
    /// The "DropBoxExim" class is a helper class to import/export the DropBox component's data to or from a JSON file.
    /// </summary>
    public class DropBoxExim
    {
        [JsonPropertyName("XPosition")]
        public int XPosition { get; set; }
        [JsonPropertyName("YPosition")]
        public int YPosition { get; set; }
        [JsonPropertyName("Width")]
        public int Width { get; set; }
        [JsonPropertyName("Height")]
        public int Height { get; set; }
        [JsonPropertyName("Options")]
        public required DropBoxOptionExim[] Options { get; set; }
        [JsonPropertyName("TextColor")]
        public required int[] TextColor { get; set; }
        [JsonPropertyName("Color")]
        public required int[] Color { get; set; }
        [JsonPropertyName("BorderColor")]
        public required int[] BorderColor { get; set; }
        [JsonPropertyName("HoverColor")]
        public required int[] HoverColor { get; set; }
    }
    /// <summary>
    /// The "DropBoxOptionExim" class is a helper class to import/export the DropBoxOption component's data to or from a JSON file.
    /// </summary>
    public class DropBoxOptionExim
    {
        [JsonPropertyName("Text")]
        public required string Text { get; set; }
        [JsonPropertyName("Action")]
        public required ActionExim Action { get; set; }
    }
    /// <summary>
    /// The "SliderExim" class is a helper class to import/export the Slider component's data to or from a JSON file.
    /// </summary>
    public class SliderExim
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
        [JsonPropertyName("DragRadius")]
        public int DragRadius { get; set; }
        [JsonPropertyName("Value")]
        public int value { get; set; }
        [JsonPropertyName("DragColor")]
        public required int[] DragColor { get; set; }
        [JsonPropertyName("Color")]
        public required int[] Color { get; set; }
        [JsonPropertyName("BorderColor")]
        public required int[] BorderColor { get; set; }
        [JsonPropertyName("Action")]
        public required ActionExim Action { get; set; }
    }
    /// <summary>
    /// The "ToggleExim" class is a helper class to import/export the Toggle component's data to or from a JSON file.
    /// </summary>
    public class ToggleExim
    {
        [JsonPropertyName("XPosition")]
        public int XPosition { get; set; }
        [JsonPropertyName("YPosition")]
        public int YPosition { get; set; }
        [JsonPropertyName("BoxSize")]
        public int BoxSize { get; set; }
        [JsonPropertyName("TextXOffset")]
        public int TextXOffset { get; set; }
        [JsonPropertyName("Text")]
        public required string Text { get; set; }
        [JsonPropertyName("Toggled")]
        public required string Toggled { get; set; }
        [JsonPropertyName("Color")]
        public required int[] Color { get; set; }
        [JsonPropertyName("BorderColor")]
        public required int[] BorderColor { get; set; }
        [JsonPropertyName("ActivatedColor")]
        public required int[] ActivatedColor { get; set; }
        [JsonPropertyName("Action")]
        public required ActionExim Action { get; set; }
    }
    /// <summary>
    /// The "TextFieldExim" class is a helper class to import/export the TextField component's data to or from a JSON file.
    /// </summary>
    public class TextFieldExim
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
        [JsonPropertyName("HorizontalTextMargin")]
        public int HorizontalTextMargin { get; set; }
        [JsonPropertyName("VerticalTextMargin")]
        public int VerticalTextMargin { get; set; }
        [JsonPropertyName("Text")]
        public string? Text { get; set; }
        [JsonPropertyName("IsVisible")]
        public required string IsVisible { get; set; }
        [JsonPropertyName("WordWrap")]
        public required string WordWrap { get; set; }
        [JsonPropertyName("Font")]
        public string Font { get; set; }
        [JsonPropertyName("Color")]
        public required int[] Color { get; set; }
        [JsonPropertyName("BorderColor")]
        public required int[] BorderColor { get; set; }
    }
    /// <summary>
    /// The "SpriteExim" class is a helper class to import/export the Sprite component's data to or from a JSON file.
    /// </summary>
    public class SpriteExim
    {
        [JsonPropertyName("Path")]
        public required string Path { get; set; }
        [JsonPropertyName("Block")]
        public BlockExim? Block { get; set; }
        [JsonPropertyName("XPosition")]
        public int? XPosition { get; set; }
        [JsonPropertyName("YPosition")]
        public int? YPosition { get; set; }
    }
}
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace VisualNovelEngine.Engine.PortData
{
    /// <summary>
    /// Helper class for importing and exporting editor data.
    /// </summary>
    public class EditorExIm
    {
        [JsonPropertyName("ID")]
        public int ID { get; set; }
        [JsonPropertyName("ProjectName")]
        public required string ProjectName { get; set; }
        [JsonPropertyName("ProjectPath")]
        public required string ProjectPath { get; set; }
        [JsonPropertyName("WindowWidth")]
        public int WindowWidth { get; set; }
        [JsonPropertyName("WindowHeight")]
        public int WindowHeight { get; set; }
        [JsonPropertyName("ToolBar")]
        public required GroupExIm ToolBar { get; set; }
        [JsonPropertyName("Scenes")]
        public SceneExIm[] Scenes { get; set; }
    }
    public class GroupExIm
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
        public ButtonExIm[]? Buttons { get; set; }
        [JsonPropertyName("Components")]
        public ComponentExIm[]? Components { get; set; }
    }
    public class ButtonExIm
    {
        [JsonPropertyName("XPosition")]
        public int XPosition { get; set; }
        [JsonPropertyName("YPosition")]
        public int YPosition { get; set; }
        [JsonPropertyName("Text")]
        public required string Text { get; set; }
        [JsonPropertyName("Command")]
        public required CommandExIm Command { get; set; }
        [JsonPropertyName("Type")]
        public int Type { get; set; }
    }
    public class ComponentExIm
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
        public required RenderingObjectExIm RenderingObject { get; set; }
    }
    public class RenderingObjectExIm
    {
        [JsonPropertyName("Static")]
        public string? Static { get; set; }
        [JsonPropertyName("Sprite")]
        public SpriteExim? Sprite { get; set; }
        [JsonPropertyName("TextField")]
        public TextFieldExim? TextField { get; set; }
        [JsonPropertyName("Textbox")]
        public TextBoxExIm? Textbox { get; set; }
        [JsonPropertyName("Button")]
        public ButtonComponentExIm? Button { get; set; }
        [JsonPropertyName("InputField")]
        public InputFieldExim? InputField { get; set; }
        [JsonPropertyName("StaticInputField")]
        public InputFieldExim? StaticInputField { get; set; }
        [JsonPropertyName("DropBox")]
        public DropBoxExim? DropBox { get; set; }
        [JsonPropertyName("Menu")]
        public MenuExim? Menu { get; set; }
        [JsonPropertyName("Block")]
        public BlockExim? Block { get; set; }
        [JsonPropertyName("StaticBlock")]
        public BlockExim? StaticBlock { get; set; }
        [JsonPropertyName("Slider")]
        public SliderExim? Slider { get; set; }
        [JsonPropertyName("Toggle")]
        public ToggleExim? Toggle { get; set; }
    }
    public class CommandExIm
    {
        [JsonPropertyName("Type")]
        public required string Type { get; set; }
        [JsonPropertyName("ErrorType")]
        public int? ErrorType { get; set; }
        [JsonPropertyName("ErrorMessage")]
        public string? ErrorMessage { get; set; }
        [JsonPropertyName("WarningButtons")]
        public ButtonExIm[]? WarningButtons { get; set; }
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
        public ButtonExIm[]? Buttons { get; set; }
        [JsonPropertyName("ButtonDependency")]
        public ButtonExIm? ButtonDependency { get; set; }
    }
    public class SceneExIm
    {
        [JsonPropertyName("ID")]
        public int ID { get; set; }
        [JsonPropertyName("Name")]
        public required string Name { get; set; }
        [JsonPropertyName("Components")]
        public ComponentExIm[]? Components { get; set; }
        [JsonPropertyName("GroupList")]
        public GroupExIm[]? GroupList { get; set; }
        [JsonPropertyName("Timeline")]
        public TimelineExIm Timeline { get; set; }
    }
    public class TimelineExIm
    {
        [JsonPropertyName("Actions")]
        public ActionExim[]? Actions { get; set; }
    }
}
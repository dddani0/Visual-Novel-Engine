using System.Text.Json;
using System.Text.Json.Serialization;
using EngineComponents.Actions.TimelineIndependent;
using EngineComponents.Actions.TimelineDependent;
using EngineComponents.Interfaces;
using Raylib_cs;
using EngineComponents.Actions;

namespace EngineComponents
{
    /// <summary>
    /// The GameImport class is a helper class to import the game settings from a json file.
    /// Intended to be used by the Game class.
    /// </summary>
    internal class GameImport
    {
        [JsonPropertyName("Title")]
        public required string Title { get; set; }
        [JsonPropertyName("WindowWidth")]
        public int WindowWidth { get; set; }
        [JsonPropertyName("WindowHeight")]
        public int WindowHeigth { get; set; }
    }
    /// <summary>
    /// The SceneImport class is a helper class to import the scene settings from a json file.
    /// Intended to be used by the Game class.
    /// </summary>
    internal class SceneImport
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
        public ActionImport[]? ActionList { get; set; }
    }
    /// <summary>
    /// The ActionImport class is a helper class to import the list of actions from a json file.
    /// </summary>
    internal class ActionImport
    {
        [JsonPropertyName("Type")]
        public required string Type { get; set; }
        [JsonPropertyName("Sprite")]
        public SpriteImport? Sprite { get; set; }
        [JsonPropertyName("CharactersPerSecond")]
        public double? CharactersPerSecond { get; set; }
        [JsonPropertyName("Font")] //Need font importer class
        public string Font { get; set; }
        [JsonPropertyName("TextBoxColor")]
        public int[]? TextBoxColor { get; set; }
        [JsonPropertyName("TextBoxBorder")]
        public int[]? TextBoxBorder { get; set; }
        [JsonPropertyName("PositionType")]
        public int? PositionType { get; set; }
        [JsonPropertyName("HorizontalTextMargin")]
        public int? HorizontalTextMargin { get; set; }
        [JsonPropertyName("VerticalTextMargin")]
        public int? VerticalTextMargin { get; set; }
        [JsonPropertyName("WordWrap")]
        public bool? WordWrap { get; set; }
        [JsonPropertyName("TextBoxTitle")]
        public string? TextBoxTitle { get; set; }
        [JsonPropertyName("TextBoxContent")]
        public string[]? TextBoxContent { get; set; }
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
        public string ImpendingVariableName { get; set; }
        [JsonPropertyName("MenuID")]
        public long? MenuID { get; set; }
        [JsonPropertyName("MenuXPosition")]
        public int? MenuXPosition { get; set; }
        [JsonPropertyName("MenuYPosition")]
        public int? MenuYPosition { get; set; }
        [JsonPropertyName("MenuWidth")]
        public int? MenuWidth { get; set; }
        [JsonPropertyName("MenuHeight")]
        public int? MenuHeight { get; set; }
        [JsonPropertyName("MenuStatic")]
        public string? MenuStatic { get; set; }
        [JsonPropertyName("MenuFullScreen")]
        public string? MenuFullScreen { get; set; }
        [JsonPropertyName("MenuBlockList")]
        public BlockImport[]? MenuBlockList { get; set; }
        [JsonPropertyName("MenuColor")]
        public int[]? MenuColor { get; set; }
        [JsonPropertyName("MenuBorderColor")]
        public int[]? MenuBorderColor { get; set; }
        [JsonPropertyName("BlockComponentID")]
        public long BlockComponentID { get; set; }
        [JsonPropertyName("DisablingMenuID")]
        public long? DisablingMenuID { get; set; }
        [JsonPropertyName("EnablingMenuID")]
        public long? EnablingMenuID { get; set; }
    }
    /// <summary>
    /// The VariableImport class is a helper class to import the list of saved variables from a json file.
    /// </summary>
    internal class VariableImport
    {
        [JsonPropertyName("Name")]
        public required string Name { get; set; }
        [JsonPropertyName("Value")]
        public required string Value { get; set; }
        [JsonPropertyName("Type")]
        public required int Type { get; set; }
    }
    /// <summary>
    /// The BlockImport class is a helper class to import the list of blocks from a json file.
    /// </summary>
    internal class BlockImport
    {
        [JsonPropertyName("BlockID")]
        public required long BlockID { get; set; }
        [JsonPropertyName("BlockXPosition")]
        public int BlockXPosition { get; set; }
        [JsonPropertyName("BlockYPosition")]
        public int BlockYPosition { get; set; }
        [JsonPropertyName("Button")]
        public ButtonComponentImport? Button { get; set; }
        [JsonPropertyName("StaticButton")]
        public ButtonComponentImport? StaticButton { get; set; }
        [JsonPropertyName("InputField")]
        public InputFieldImport? InputField { get; set; }
        [JsonPropertyName("StaticInputField")]
        public InputFieldImport? StaticInputField { get; set; }
        [JsonPropertyName("DropBox")]
        public DropBoxImport? DropBox { get; set; }
        [JsonPropertyName("Slider")]
        public SliderImport? Slider { get; set; }
        [JsonPropertyName("Toggle")]
        public ToggleImport? Toggle { get; set; }
        [JsonPropertyName("TextField")]
        public TextFieldImport? TextField { get; set; }
        [JsonPropertyName("Sprite")]
        public SpriteImport? Sprite { get; set; }
    }
    /// <summary>
    /// The ButtonComponentImport class is a helper class to import the list of buttons from a json file.
    /// </summary>
    internal class ButtonComponentImport
    {
        [JsonPropertyName("ButtonXPosition")]
        public required int ButtonXPosition { get; set; }
        [JsonPropertyName("ButtonYPosition")]
        public required int ButtonYPosition { get; set; }
        [JsonPropertyName("ButtonWidth")]
        public required int ButtonWidth { get; set; }
        [JsonPropertyName("ButtonHeight")]
        public required int ButtonHeight { get; set; }
        [JsonPropertyName("ButtonText")]
        public required string ButtonText { get; set; }
        [JsonPropertyName("ButtonBorderWidth")]
        public int ButtonBorderWidth { get; set; }
        [JsonPropertyName("TextColor")]
        public required int[] TextColor { get; set; }
        [JsonPropertyName("ButtonColor")]
        public required int[] ButtonColor { get; set; }
        [JsonPropertyName("ButtonBorderColor")]
        public required int[] ButtonBorderColor { get; set; }
        [JsonPropertyName("ButtonHoverColor")]
        public required int[] ButtonHoverColor { get; set; }
        [JsonPropertyName("Event")]
        public required ActionImport Event { get; set; }
    }
    /// <summary>
    /// The TextBoxCreateAction class is a helper class to import the TextField component.
    /// </summary>
    internal class InputFieldImport
    {
        [JsonPropertyName("InputFieldXPosition")]
        public int XPosition { get; set; }
        [JsonPropertyName("InputFieldYPosition")]
        public int YPosition { get; set; }
        [JsonPropertyName("ButtonYOffset")]
        public int ButtonYOffset { get; set; }
        [JsonPropertyName("InputFieldWidth")]
        public int Width { get; set; }
        [JsonPropertyName("InputFieldHeight")]
        public int Height { get; set; }
        [JsonPropertyName("InputFieldPlaceholder")]
        public required string InputFieldPlaceholder { get; set; }
        [JsonPropertyName("InputFieldButtonText")]
        public required string InputFieldButtonText { get; set; }
        [JsonPropertyName("InputFieldButtonEvent")]
        public required ActionImport InputFieldButtonEvent { get; set; }
        [JsonPropertyName("BorderWidth")]
        public int InputFieldBorderWidth { get; set; }
        [JsonPropertyName("InputFieldColor")]
        public required int[] InputFieldColor { get; set; }
        [JsonPropertyName("InputFieldBorderColor")]
        public required int[] InputFieldBorderColor { get; set; }
        [JsonPropertyName("InputFieldHoverColor")]
        public required int[] InputFieldHoverColor { get; set; }
    }
    /// <summary>
    /// The DropBoxImport class is a helper class to import the DropBox component.
    /// </summary>
    internal class DropBoxImport
    {
        [JsonPropertyName("DropBoxXPosition")]
        public int DropBoxXPosition { get; set; }
        [JsonPropertyName("DropBoxYPosition")]
        public int DropBoxYPosition { get; set; }
        [JsonPropertyName("DropBoxWidth")]
        public int DropBoxWidth { get; set; }
        [JsonPropertyName("DropBoxHeight")]
        public int DropBoxHeight { get; set; }
        [JsonPropertyName("DropBoxOptions")]
        public DropBoxOptionImport[] DropBoxOptions { get; set; }
        [JsonPropertyName("DropBoxTextColor")]
        public int[] DropBoxTextColor { get; set; }
        [JsonPropertyName("DropBoxColor")]
        public int[] DropBoxColor { get; set; }
        [JsonPropertyName("DropBoxBorderColor")]
        public int[] DropBoxBorderColor { get; set; }
        [JsonPropertyName("DropBoxHoverColor")]
        public int[] DropBoxHoverColor { get; set; }
    }
    /// <summary>
    /// The DropBoxOptionImport class is a helper class to import the DropBoxOption component.
    /// </summary>
    internal class DropBoxOptionImport
    {
        [JsonPropertyName("OptionText")]
        public string ButtonText { get; set; }
        [JsonPropertyName("OptionEvent")]
        public ActionImport OptionEvent { get; set; }
    }
    /// <summary>
    /// The SliderImport class is a helper class to import the Slider component.
    /// </summary>
    internal class SliderImport
    {
        [JsonPropertyName("SliderXPosition")]
        public int SliderXPosition { get; set; }
        [JsonPropertyName("SliderYPosition")]
        public int SliderYPosition { get; set; }
        [JsonPropertyName("SliderWidth")]
        public int SliderWidth { get; set; }
        [JsonPropertyName("SliderHeight")]
        public int SliderHeight { get; set; }
        [JsonPropertyName("SliderBorderWidth")]
        public int SliderBorderWidth { get; set; }
        [JsonPropertyName("SliderDragRadius")]
        public int SliderDragRadius { get; set; }
        [JsonPropertyName("SliderDragColor")]
        public int[] SliderDragColor { get; set; }
        [JsonPropertyName("SliderColor")]
        public int[] SliderColor { get; set; }
        [JsonPropertyName("SliderBorderColor")]
        public int[] SliderBorderColor { get; set; }
        [JsonPropertyName("SliderEvent")]
        public ActionImport SliderEvent { get; set; }
    }
    /// <summary>
    /// The ToggleImport class is a helper class to import the Toggle component.
    /// </summary>
    internal class ToggleImport
    {
        [JsonPropertyName("ToggleXPosition")]
        public int ToggleXPosition { get; set; }
        [JsonPropertyName("ToggleYPosition")]
        public int ToggleYPosition { get; set; }
        [JsonPropertyName("ToggleBoxSize")]
        public int ToggleBoxSize { get; set; }
        [JsonPropertyName("ToggleTextXOffset")]
        public int ToggleTextXOffset { get; set; }
        [JsonPropertyName("ToggleText")]
        public required string ToggleText { get; set; }
        [JsonPropertyName("ToggleColor")]
        public int[] ToggleColor { get; set; }
        [JsonPropertyName("ToggleBorderColor")]
        public int[] BorderColor { get; set; }
        [JsonPropertyName("ToggleToggledColor")]
        public int[] ToggledColor { get; set; }
        [JsonPropertyName("ToggleEvent")]
        public ActionImport ToggleEvent { get; set; }
    }

    internal class TextFieldImport
    {
        [JsonPropertyName("TextFieldXPosition")]
        public int XPosition { get; set; }
        [JsonPropertyName("TextFieldYPosition")]
        public int YPosition { get; set; }
        [JsonPropertyName("TextFieldWidth")]
        public int Width { get; set; }
        [JsonPropertyName("TextFieldHeight")]
        public int Height { get; set; }
        [JsonPropertyName("HorizontalTextMargin")]
        public int HorizontalTextMargin { get; set; }
        [JsonPropertyName("VerticalTextMargin")]
        public int VerticalTextMargin { get; set; }
        [JsonPropertyName("Text")]
        public string? Text { get; set; }
        [JsonPropertyName("IsVisible")]
        public string IsVisible { get; set; }
        [JsonPropertyName("WordWrap")]
        public string WordWrap { get; set; }
        [JsonPropertyName("Font")]
        public string Font { get; set; }
        [JsonPropertyName("TextFieldColor")]
        public int[] TextFieldColor { get; set; }
    }
    /// <summary>
    /// The SpriteImport class is a helper class to import the Sprite component.
    /// </summary>
    internal class SpriteImport
    {
        [JsonPropertyName("SpritePath")]
        public required string SpritePath { get; set; }
        [JsonPropertyName("SpriteXPosition")]
        public int? SpriteXPosition { get; set; }
        [JsonPropertyName("SpriteYPosition")]
        public int? SpriteYPosition { get; set; }
    }
    /// <summary>
    /// The GameLoader class is the class that turns raw data into exact objects, which the game can use.
    /// Create Events from the scene.json file.
    /// </summary>
    public class GameLoader(Game game)
    {
        /// <summary>
        /// The Game object.
        /// </summary>
        Game Game { get; set; } = game;
        /// <summary>
        /// The cache of created blocks.
        /// </summary>
        internal List<Block> BlockListCache { get; set; } = [];
        internal List<Menu> MenuListCache { get; set; } = [];
        /// <summary>
        /// Creates a sprite from the importer class
        /// </summary>
        /// <param name="rawAction"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        Sprite FetchSpriteFromImport(SpriteImport rawAction)
        {
            if (rawAction.SpritePath == null)
            {
                throw new InvalidOperationException("Failed to load scene settings, because the sprite path is null.");
            }
            if (File.Exists(Game.currentFolderPath + rawAction.SpritePath) is false)
            {
                throw new InvalidOperationException("Failed to load scene settings, because the sprite path is invalid.");
            }
            return new Sprite(Game.currentFolderPath + rawAction.SpritePath);
        }
        /// <summary>
        /// Creates a variable from the importer class
        /// </summary>
        /// <param name="rawAction"></param>
        /// <param name="block"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        Sprite FetchSpriteFromImport(SpriteImport rawAction, Block block)
        {
            if (rawAction.SpritePath == null)
            {
                throw new InvalidOperationException("Failed to load scene settings, because the sprite path is null.");
            }
            if (rawAction.SpriteXPosition == null)
            {
                throw new InvalidOperationException("Failed to load scene settings, because the sprite x position is null.");
            }
            if (rawAction.SpriteYPosition == null)
            {
                throw new InvalidOperationException("Failed to load scene settings, because the sprite y position is null.");
            }
            if (File.Exists(Game.currentFolderPath + rawAction.SpritePath) is false)
            {
                throw new InvalidOperationException("Failed to load scene settings, because the sprite path is invalid.");
            }
            return new Sprite(Game.currentFolderPath + rawAction.SpritePath, block, rawAction.SpriteXPosition.Value, rawAction.SpriteYPosition.Value);
        }
        /// <summary>
        /// Creates a variable from the importer class
        /// </summary>
        /// <param name="rawAction"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        Variable FetchVariableFromImport(ActionImport rawAction)
        {
            if (rawAction.VariableName == null)
            {
                throw new InvalidOperationException("Failed to load scene settings, because the variable name is null.");
            }
            if (rawAction.VariableValue == null)
            {
                throw new InvalidOperationException("Failed to load scene settings, because the variable value is null.");
            }
            if (rawAction.VariableType == null)
            {
                throw new InvalidOperationException("Failed to load scene settings, because the variable type is null.");
            }
            return new Variable(rawAction.VariableName, rawAction.VariableValue, (VariableType)rawAction.VariableType);
        }
        /// <summary>
        /// Creates a button with a Timeline dependent or a general event attached to it from the importer class
        /// </summary>
        /// <param name="buttonComponentImport"></param>
        /// <param name="block"></param>
        /// <returns></returns>
        Button FetchButtonFromImport(ButtonComponentImport buttonComponentImport, Block block)
        {
            return new Button(
                Game,
                block,
                new Font() { BaseSize = 30, GlyphPadding = 5 },
                buttonComponentImport.ButtonXPosition,
                buttonComponentImport.ButtonYPosition,
                buttonComponentImport.ButtonBorderWidth,
                buttonComponentImport.ButtonWidth,
                buttonComponentImport.ButtonHeight,
                buttonComponentImport.ButtonText,
                new Color()
                {
                    R = (byte)buttonComponentImport.TextColor[0],
                    G = (byte)buttonComponentImport.TextColor[1],
                    B = (byte)buttonComponentImport.TextColor[2],
                    A = (byte)buttonComponentImport.TextColor[3]
                },
                new Color()
                {
                    R = (byte)buttonComponentImport.ButtonColor[0],
                    G = (byte)buttonComponentImport.ButtonColor[1],
                    B = (byte)buttonComponentImport.ButtonColor[2],
                    A = (byte)buttonComponentImport.ButtonColor[3]
                },
                new Color()
                {
                    R = (byte)buttonComponentImport.ButtonBorderColor[0],
                    G = (byte)buttonComponentImport.ButtonBorderColor[1],
                    B = (byte)buttonComponentImport.ButtonBorderColor[2],
                    A = (byte)buttonComponentImport.ButtonBorderColor[3]
                },
                new Color()
                {
                    R = (byte)buttonComponentImport.ButtonHoverColor[0],
                    G = (byte)buttonComponentImport.ButtonHoverColor[1],
                    B = (byte)buttonComponentImport.ButtonHoverColor[2],
                    A = (byte)buttonComponentImport.ButtonHoverColor[3]
                },
                (IButtonEvent)FetchTimelineDependentEventFromImport(buttonComponentImport.Event)
            );
        }
        /// <summary>
        /// Creates a button with a Timeline independent or a general event attached to it from the importer class
        /// </summary>
        /// <param name="buttonComponentImport"></param>
        /// <param name="block"></param>
        /// <returns></returns>
        Button FetchStaticButtonFromImport(ButtonComponentImport buttonComponentImport, Block block)
        {
            return new Button(
                            Game,
                            block,
                            new Font() { BaseSize = 30, GlyphPadding = 5 },
                            buttonComponentImport.ButtonXPosition,
                            buttonComponentImport.ButtonYPosition,
                            buttonComponentImport.ButtonBorderWidth,
                            buttonComponentImport.ButtonWidth,
                            buttonComponentImport.ButtonHeight,
                            buttonComponentImport.ButtonText,
                            new Color()
                            {
                                R = (byte)buttonComponentImport.TextColor[0],
                                G = (byte)buttonComponentImport.TextColor[1],
                                B = (byte)buttonComponentImport.TextColor[2],
                                A = (byte)buttonComponentImport.TextColor[3]
                            },
                            new Color()
                            {
                                R = (byte)buttonComponentImport.ButtonColor[0],
                                G = (byte)buttonComponentImport.ButtonColor[1],
                                B = (byte)buttonComponentImport.ButtonColor[2],
                                A = (byte)buttonComponentImport.ButtonColor[3]
                            },
                            new Color()
                            {
                                R = (byte)buttonComponentImport.ButtonBorderColor[0],
                                G = (byte)buttonComponentImport.ButtonBorderColor[1],
                                B = (byte)buttonComponentImport.ButtonBorderColor[2],
                                A = (byte)buttonComponentImport.ButtonBorderColor[3]
                            },
                            new Color()
                            {
                                R = (byte)buttonComponentImport.ButtonHoverColor[0],
                                G = (byte)buttonComponentImport.ButtonHoverColor[1],
                                B = (byte)buttonComponentImport.ButtonHoverColor[2],
                                A = (byte)buttonComponentImport.ButtonHoverColor[3]
                            },
                            FetchTimelineIndependentEventFromImport(buttonComponentImport.Event)
                        );
        }
        /// <summary>
        /// Creates an option from the importer class
        /// </summary>
        /// <param name="rawDropBoxOption"></param>
        /// <param name="block"></param>
        /// <returns></returns>
        Button FetchDropBoxOptionFromImport(DropBoxImport rawDropBox, DropBoxOptionImport rawDropBoxOption, Block block)
        {
            //Each dropbox option's Y position is calculated by the height of the dropbox and the index of the option in the DropBox.cs class.
            return DropBox.GetDropBoxOption(
                Game,
                block,
                new Font() { BaseSize = 10, GlyphPadding = 2 },
                0,
                0,
                0,
                rawDropBox.DropBoxWidth,
                rawDropBox.DropBoxHeight,
                rawDropBoxOption.ButtonText,
                new Color()
                {
                    R = (byte)rawDropBox.DropBoxTextColor[0],
                    G = (byte)rawDropBox.DropBoxTextColor[1],
                    B = (byte)rawDropBox.DropBoxTextColor[2],
                    A = (byte)rawDropBox.DropBoxTextColor[3]
                },
                new Color()
                {
                    R = (byte)rawDropBox.DropBoxColor[0],
                    G = (byte)rawDropBox.DropBoxColor[1],
                    B = (byte)rawDropBox.DropBoxColor[2],
                    A = (byte)rawDropBox.DropBoxColor[3]
                },
                new Color()
                {
                    R = (byte)rawDropBox.DropBoxBorderColor[0],
                    G = (byte)rawDropBox.DropBoxBorderColor[1],
                    B = (byte)rawDropBox.DropBoxBorderColor[2],
                    A = (byte)rawDropBox.DropBoxBorderColor[3]
                },
                new Color()
                {
                    R = (byte)rawDropBox.DropBoxHoverColor[0],
                    G = (byte)rawDropBox.DropBoxHoverColor[1],
                    B = (byte)rawDropBox.DropBoxHoverColor[2],
                    A = (byte)rawDropBox.DropBoxHoverColor[3]
                },
                FetchTimelineIndependentEventFromImport(rawDropBoxOption.OptionEvent)
            );
        }
        /// <summary>
        /// Creates a dropbox from the importer class
        /// </summary>
        /// <param name="rawDropBox"></param>
        /// <param name="block"></param>
        /// <returns></returns>
        DropBox FetchDropBoxFromImport(DropBoxImport rawDropBox, Block block)
        {
            return new DropBox(
                                    block,
                                    rawDropBox.DropBoxXPosition,
                                    rawDropBox.DropBoxYPosition,
                                    rawDropBox.DropBoxWidth,
                                    rawDropBox.DropBoxHeight,
                                    rawDropBox.DropBoxOptions.Select(option => FetchDropBoxOptionFromImport(rawDropBox, option, block)).ToArray(),
                                    new Color()
                                    {
                                        R = (byte)rawDropBox.DropBoxColor[0],
                                        G = (byte)rawDropBox.DropBoxColor[1],
                                        B = (byte)rawDropBox.DropBoxColor[2],
                                        A = (byte)rawDropBox.DropBoxColor[3]
                                    },
                                    new Color()
                                    {
                                        R = (byte)rawDropBox.DropBoxBorderColor[0],
                                        G = (byte)rawDropBox.DropBoxBorderColor[1],
                                        B = (byte)rawDropBox.DropBoxBorderColor[2],
                                        A = (byte)rawDropBox.DropBoxBorderColor[3]
                                    },
                                    new Color()
                                    {
                                        R = (byte)rawDropBox.DropBoxHoverColor[0],
                                        G = (byte)rawDropBox.DropBoxHoverColor[1],
                                        B = (byte)rawDropBox.DropBoxHoverColor[2],
                                        A = (byte)rawDropBox.DropBoxHoverColor[3]
                                    });
        }
        /// <summary>
        /// Creates an input field from the importer class
        /// </summary>
        /// <param name="inputFieldImport"></param>
        /// <param name="block"></param>
        /// <returns></returns>
        InputField FetchInputFieldFromImport(InputFieldImport inputFieldImport, Block block)
        {
            return new InputField(
                Game,
                block,
                inputFieldImport.XPosition,
                inputFieldImport.YPosition,
                inputFieldImport.ButtonYOffset,
                inputFieldImport.Width,
                inputFieldImport.Height,
                inputFieldImport.InputFieldPlaceholder,
                inputFieldImport.InputFieldButtonText,
                new Color()
                {
                    R = (byte)inputFieldImport.InputFieldColor[0],
                    G = (byte)inputFieldImport.InputFieldColor[1],
                    B = (byte)inputFieldImport.InputFieldColor[2],
                    A = (byte)inputFieldImport.InputFieldColor[3]
                },
                new Color()
                {
                    R = (byte)inputFieldImport.InputFieldBorderColor[0],
                    G = (byte)inputFieldImport.InputFieldBorderColor[1],
                    B = (byte)inputFieldImport.InputFieldBorderColor[2],
                    A = (byte)inputFieldImport.InputFieldBorderColor[3]
                },
                new Color()
                {
                    R = (byte)inputFieldImport.InputFieldHoverColor[0],
                    G = (byte)inputFieldImport.InputFieldHoverColor[1],
                    B = (byte)inputFieldImport.InputFieldHoverColor[2],
                    A = (byte)inputFieldImport.InputFieldHoverColor[3]
                },
                (IButtonEvent)FetchTimelineDependentEventFromImport(inputFieldImport.InputFieldButtonEvent)
            );
        }
        /// <summary>
        /// Creates a static input field from the importer class
        /// </summary>
        /// <param name="inputFieldImport"></param>
        /// <param name="block"></param>
        /// <returns></returns>
        InputField FetchStaticInputFieldFromImport(InputFieldImport inputFieldImport, Block block)
        {
            return new InputField(
                Game,
                block,
                inputFieldImport.XPosition,
                inputFieldImport.YPosition,
                inputFieldImport.ButtonYOffset,
                inputFieldImport.Width,
                inputFieldImport.Height,
                inputFieldImport.InputFieldPlaceholder,
                inputFieldImport.InputFieldButtonText,
                new Color()
                {
                    R = (byte)inputFieldImport.InputFieldColor[0],
                    G = (byte)inputFieldImport.InputFieldColor[1],
                    B = (byte)inputFieldImport.InputFieldColor[2],
                    A = (byte)inputFieldImport.InputFieldColor[3]
                },
                new Color()
                {
                    R = (byte)inputFieldImport.InputFieldBorderColor[0],
                    G = (byte)inputFieldImport.InputFieldBorderColor[1],
                    B = (byte)inputFieldImport.InputFieldBorderColor[2],
                    A = (byte)inputFieldImport.InputFieldBorderColor[3]
                },
                new Color()
                {
                    R = (byte)inputFieldImport.InputFieldHoverColor[0],
                    G = (byte)inputFieldImport.InputFieldHoverColor[1],
                    B = (byte)inputFieldImport.InputFieldHoverColor[2],
                    A = (byte)inputFieldImport.InputFieldHoverColor[3]
                },
                FetchTimelineIndependentEventFromImport(inputFieldImport.InputFieldButtonEvent)
            );
        }
        /// <summary>
        /// Creates a slider from the importer class
        /// </summary>
        /// <param name="rawSlider"></param>
        /// <param name="block"></param>
        /// <returns></returns>
        Slider FetchSliderFromImport(SliderImport rawSlider, Block block)
        {
            return new Slider(
                block,
                rawSlider.SliderXPosition,
                rawSlider.SliderYPosition,
                rawSlider.SliderWidth,
                rawSlider.SliderHeight,
                rawSlider.SliderBorderWidth,
                rawSlider.SliderDragRadius,
                new Color()
                {
                    R = (byte)rawSlider.SliderDragColor[0],
                    G = (byte)rawSlider.SliderDragColor[1],
                    B = (byte)rawSlider.SliderDragColor[2],
                    A = (byte)rawSlider.SliderDragColor[3]
                },
                new Color()
                {
                    R = (byte)rawSlider.SliderColor[0],
                    G = (byte)rawSlider.SliderColor[1],
                    B = (byte)rawSlider.SliderColor[2],
                    A = (byte)rawSlider.SliderColor[3]
                },
                new Color()
                {
                    R = (byte)rawSlider.SliderBorderColor[0],
                    G = (byte)rawSlider.SliderBorderColor[1],
                    B = (byte)rawSlider.SliderBorderColor[2],
                    A = (byte)rawSlider.SliderBorderColor[3]
                },
                FetchTimelineIndependentEventFromImport(rawSlider.SliderEvent)
            );
        }
        /// <summary>
        /// Creates a toggle from the importer class
        /// </summary>
        /// <param name="rawToggle"></param>
        /// <param name="block"></param>
        /// <returns></returns>
        Toggle FetchToggleFromImport(ToggleImport rawToggle, Block block)
        {
            return new Toggle(
                 block,
                 rawToggle.ToggleXPosition,
                 rawToggle.ToggleYPosition,
                 rawToggle.ToggleBoxSize,
                 rawToggle.ToggleTextXOffset,
                 rawToggle.ToggleText,
                 false,
                 new Color()
                 {
                     R = (byte)rawToggle.ToggleColor[0],
                     G = (byte)rawToggle.ToggleColor[1],
                     B = (byte)rawToggle.ToggleColor[2],
                     A = (byte)rawToggle.ToggleColor[3]
                 },
                 new Color()
                 {
                     R = (byte)rawToggle.BorderColor[0],
                     G = (byte)rawToggle.BorderColor[1],
                     B = (byte)rawToggle.BorderColor[2],
                     A = (byte)rawToggle.BorderColor[3]
                 },
                 new Color()
                 {
                     R = (byte)rawToggle.ToggledColor[0],
                     G = (byte)rawToggle.ToggledColor[1],
                     B = (byte)rawToggle.ToggledColor[2],
                     A = (byte)rawToggle.ToggledColor[3]
                 },
                 FetchTimelineIndependentEventFromImport(rawToggle.ToggleEvent)
             );
        }
        /// <summary>
        /// Creates a text field from the importer class
        /// </summary>
        /// <param name="rawTextField"></param>
        /// <param name="block"></param>
        /// <returns></returns>
        TextField FetchTextFieldFromImport(TextFieldImport rawTextField, Block block)
        {
            return new TextField(
                block,
                rawTextField.XPosition,
                rawTextField.YPosition,
                rawTextField.Width,
                rawTextField.Height,
                rawTextField.HorizontalTextMargin,
                rawTextField.VerticalTextMargin,
                rawTextField.Text,
                new Font() { BaseSize = 30, GlyphPadding = 5 },
                rawTextField.IsVisible == "True",
                rawTextField.WordWrap == "True",
                new Color()
                {
                    R = (byte)rawTextField.TextFieldColor[0],
                    G = (byte)rawTextField.TextFieldColor[1],
                    B = (byte)rawTextField.TextFieldColor[2],
                    A = (byte)rawTextField.TextFieldColor[3]
                }
            );
        }
        /// <summary>
        /// Creates a block from the importer class
        /// </summary>
        /// <param name="rawBlock"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        Block FetchBlockFromImport(BlockImport rawBlock)
        {
            var newBlock = new Block(
                rawBlock.BlockXPosition,
                rawBlock.BlockYPosition,
                null,
                rawBlock.BlockID
                );
            // The block has a button component.
            if (rawBlock.Button != null)
                newBlock.SetComponent(FetchButtonFromImport(rawBlock.Button, newBlock));
            // The block has an InputField component.
            else if (rawBlock.InputField != null)
                newBlock.SetComponent(FetchInputFieldFromImport(rawBlock.InputField, newBlock));
            // The block has a TextField component.
            else if (rawBlock.TextField != null)
                newBlock.SetComponent(FetchTextFieldFromImport(rawBlock.TextField, newBlock));
            // The block has a Sprite component.
            else if (rawBlock.Sprite != null)
                newBlock.SetComponent(FetchSpriteFromImport(rawBlock.Sprite, newBlock));
            else
                throw new InvalidOperationException("Failed to load scene settings, because either the component type attached to the block is not recognized or the type is static.");
            BlockListCache.Add(newBlock);
            return newBlock;
        }
        /// <summary>
        /// Creates a static block from the importer class
        /// </summary>
        /// <param name="rawBlock"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        Block FetchStaticBlockFromImport(BlockImport rawBlock)
        {
            var newBlock = new Block(
                    rawBlock.BlockXPosition,
                    rawBlock.BlockYPosition,
                    null,
                    rawBlock.BlockID
                );
            // The block has a static button component.
            if (rawBlock.StaticButton != null)
                newBlock.SetComponent(FetchStaticButtonFromImport(rawBlock.StaticButton, newBlock));
            // The block has a dropbox component.
            else if (rawBlock.DropBox != null)
                newBlock.SetComponent(FetchDropBoxFromImport(rawBlock.DropBox, newBlock));
            // The block has an InputField component.
            else if (rawBlock.StaticInputField != null)
                newBlock.SetComponent(FetchStaticInputFieldFromImport(rawBlock.StaticInputField, newBlock));
            // The block has a Slider component.
            else if (rawBlock.Slider != null)
                newBlock.SetComponent(FetchSliderFromImport(rawBlock.Slider, newBlock));
            // The block has a Toggle component.
            else if (rawBlock.Toggle != null)
                newBlock.SetComponent(FetchToggleFromImport(rawBlock.Toggle, newBlock));
            // The block has a TextField component.
            else if (rawBlock.TextField != null)
                newBlock.SetComponent(FetchTextFieldFromImport(rawBlock.TextField, newBlock));
            // The block has a Sprite component.
            else if (rawBlock.Sprite != null)
                newBlock.SetComponent(FetchSpriteFromImport(rawBlock.Sprite, newBlock));
            else
                throw new InvalidOperationException("Failed to load scene settings, because either the component type attached to the block is not recognized or the type is not static.");
            BlockListCache.Add(newBlock);
            return newBlock;
        }
        /// <summary>
        /// Creates a menu with static or non-static components from the importer class
        /// </summary>
        /// <param name="rawMenu"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        Menu FetchMenuFromImport(ActionImport rawMenu)
        {
            if (rawMenu.MenuXPosition == null)
            {
                throw new InvalidOperationException("Failed to load scene settings, because the menu X position is null.");
            }
            if (rawMenu.MenuYPosition == null)
            {
                throw new InvalidOperationException("Failed to load scene settings, because the menu Y position is null.");
            }
            if (rawMenu.MenuWidth == null)
            {
                throw new InvalidOperationException("Failed to load scene settings, because the menu width is null.");
            }
            if (rawMenu.MenuHeight == null)
            {
                throw new InvalidOperationException("Failed to load scene settings, because the menu height is null.");
            }
            if (rawMenu.MenuFullScreen == null)
            {
                throw new InvalidOperationException("Failed to load scene settings, because the menu fullscreen is null.");
            }
            if (rawMenu.MenuColor == null)
            {
                throw new InvalidOperationException("Failed to load scene settings, because the menu color is null.");
            }
            if (rawMenu.MenuBorderColor == null)
            {
                throw new InvalidOperationException("Failed to load scene settings, because the menu border color is null.");
            }
            if (rawMenu.MenuID == null)
            {
                throw new InvalidOperationException("Failed to load scene settings, because the menu ID is null.");
            }
            var menuXPosition = rawMenu.MenuXPosition.Value;
            var menuYPosition = rawMenu.MenuYPosition.Value;
            var menuWidth = rawMenu.MenuWidth.Value;
            var menuHeight = rawMenu.MenuHeight.Value;
            var menuFullScreen = rawMenu.MenuFullScreen == "True";
            var windowColor = new Color()
            {
                R = (byte)rawMenu.MenuColor[0],
                G = (byte)rawMenu.MenuColor[1],
                B = (byte)rawMenu.MenuColor[2],
                A = (byte)rawMenu.MenuColor[3]
            };
            var windowBorderColor = new Color()
            {
                R = (byte)rawMenu.MenuBorderColor[0],
                G = (byte)rawMenu.MenuBorderColor[1],
                B = (byte)rawMenu.MenuBorderColor[2],
                A = (byte)rawMenu.MenuBorderColor[3]
            };
            var id = rawMenu.MenuID.Value;
            var menu = new Menu(
                Game,
                id,
                menuXPosition,
                menuYPosition,
                menuWidth,
                menuHeight,
                menuFullScreen,
                [],
                windowColor,
                windowBorderColor);
            MenuListCache.Add(menu);
            if (rawMenu.MenuBlockList == null) return menu;
            menu.BlockList.AddRange(rawMenu.MenuStatic == "True" ? rawMenu.MenuBlockList.Select(block => FetchStaticBlockFromImport(block)) : rawMenu.MenuBlockList.Select(block => FetchBlockFromImport(block)));
            return menu;
        }
        /// <summary>
        /// Creates a static menu from the importer class
        /// </summary>
        /// <param name="rawMenu"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        internal IEvent FetchTimelineDependentEventFromImport(ActionImport rawAction)
        {
            switch (rawAction.Type)
            {
                case "TextBoxCreateAction":
                    // Add the textbox to the timeline.
                    if (rawAction.CharactersPerSecond.HasValue is false)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the characters per second is null.");
                    }
                    if (rawAction.Font == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the font is null.");
                    }
                    if (rawAction.TextBoxColor == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the textbox color is null.");
                    }
                    if (rawAction.TextBoxBorder == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the textbox border is null.");
                    }
                    if (rawAction.PositionType.HasValue is false)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the position type is null.");
                    }
                    if (rawAction.HorizontalTextMargin.HasValue is false)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the horizontal text margin is null.");
                    }
                    if (rawAction.VerticalTextMargin.HasValue is false)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the vertical text margin is null.");
                    }
                    if (rawAction.WordWrap.HasValue is false)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the word wrap is null.");
                    }
                    if (rawAction.TextBoxTitle == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the textbox title is null.");
                    }
                    if (rawAction.TextBoxContent == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the textbox content is null.");
                    }
                    var charactersPerSecond = rawAction.CharactersPerSecond.Value;
                    var font = new Font() { BaseSize = 30, GlyphPadding = 5 };
                    var textboxcolor = rawAction.TextBoxColor == null || rawAction.TextBoxColor.Length < 4 ?
                    new Color() { R = 0, G = 0, B = 0, A = 255 } :
                    new Color()
                    {
                        R = (byte)rawAction.TextBoxColor[0],
                        G = (byte)rawAction.TextBoxColor[1],
                        B = (byte)rawAction.TextBoxColor[2],
                        A = (byte)rawAction.TextBoxColor[3]
                    };
                    var textboxborder = rawAction.TextBoxBorder == null || rawAction.TextBoxBorder.Length < 4 ?
                    new Color() { R = 0, G = 0, B = 0, A = 255 } :
                    new Color()
                    {
                        R = (byte)rawAction.TextBoxBorder[0],
                        G = (byte)rawAction.TextBoxBorder[1],
                        B = (byte)rawAction.TextBoxBorder[2],
                        A = (byte)rawAction.TextBoxBorder[3]
                    };
                    var positionType = (TextBox.PositionType)rawAction.PositionType.Value;
                    var horizontalTextMargin = rawAction.HorizontalTextMargin.Value;
                    var verticalTextMargin = rawAction.VerticalTextMargin.Value;
                    var wordWrap = rawAction.WordWrap.Value;
                    var textBoxTitle = rawAction.TextBoxTitle;
                    var textBoxContent = rawAction.TextBoxContent;
                    //
                    var textBox = TextBox.CreateNewTextBox(
                        Game,
                        charactersPerSecond,
                        font,
                        textboxcolor,
                        textboxborder,
                        positionType,
                        horizontalTextMargin,
                        verticalTextMargin,
                        wordWrap,
                        textBoxTitle,
                        [.. textBoxContent]);
                    //
                    return new TextBoxCreateAction(textBox);
                case "AddSpriteAction":
                    // Add the sprite to the timeline.
                    if (rawAction.Sprite == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the sprite is null.");
                    }
                    var sprite = FetchSpriteFromImport(rawAction.Sprite);
                    return new AddSpriteAction(sprite, Game);
                case "TintSpriteAction":
                    // Add the tint action to the timeline.
                    if (rawAction.Sprite == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the sprite is null.");
                    }
                    if (rawAction.TintColor == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the tint color is null.");
                    }
                    var tintSprite = FetchSpriteFromImport(rawAction.Sprite);
                    var tintColor = new Color()
                    {
                        R = (byte)rawAction.TintColor[0],
                        G = (byte)rawAction.TintColor[1],
                        B = (byte)rawAction.TintColor[2],
                        A = (byte)rawAction.TintColor[3]
                    };
                    return new TintSpriteAction(tintSprite, tintColor, Game);
                case "RemoveSpriteAction":
                    // Add the remove action to the timeline.
                    if (rawAction.Sprite == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the sprite is null.");
                    }
                    var removeSprite = FetchSpriteFromImport(rawAction.Sprite);
                    return new RemoveSpriteAction(removeSprite, Game);
                case "LoadSceneAction":
                    // Add the load scene action to the timeline.
                    if (rawAction.SceneID.HasValue is false)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the scene id is null.");
                    }
                    var sceneId = rawAction.SceneID.Value;
                    return new LoadSceneAction(Game, sceneId, rawAction.TriggerVariableName);
                    break;
                case "NativeLoadSceneAction":
                    // Add the native load scene action to the timeline.
                    if (rawAction.SceneID.HasValue is false)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the scene id is null.");
                    }
                    var nativeSceneId = rawAction.SceneID.Value;
                    return new NativeLoadSceneAction(Game, nativeSceneId);
                case "CreateVariableAction":
                    // Add the create variable action to the timeline.
                    var variable = FetchVariableFromImport(rawAction);
                    return new CreateVariableAction(Game, variable);
                case "IncrementVariableAction":
                    // Add the increment variable action to the timeline.
                    if (rawAction.VariableName == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the variable name is null.");
                    }
                    if (rawAction.VariableValue == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the variable value is null.");
                    }
                    if (rawAction.ImpendingVariableName == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the impending variable type is null.");
                    }
                    return new IncrementVariableAction(Game, rawAction.VariableName, rawAction.ImpendingVariableName);
                case "DecrementVariableAction":
                    // Add the decrement variable action to the timeline.
                    if (rawAction.VariableName == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the variable name is null.");
                    }
                    if (rawAction.VariableValue == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the variable value is null.");
                    }
                    if (rawAction.ImpendingVariableName == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the impending variable type is null.");
                    }
                    return new DecrementVariableAction(Game, rawAction.VariableName, rawAction.ImpendingVariableName);
                case "SetVariableTrueAction":
                    // Add the set variable true action to the timeline.
                    if (rawAction.VariableName == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the variable name is null.");
                    }
                    return new SetVariableTrueAction(Game, rawAction.VariableName);
                case "SetVariableFalseAction":
                    // Add the set variable false action to the timeline.
                    if (rawAction.VariableName == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the variable name is null.");
                    }
                    return new SetVariableFalseAction(Game, rawAction.VariableName);
                case "SetBoolVariableAction":
                    // Add the set variable action to the timeline.
                    if (rawAction.VariableName == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the variable name is null.");
                    }
                    if (rawAction.VariableValue == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the variable value is null.");
                    }
                    return new SetBoolVariableAction(Game, rawAction.VariableName, bool.Parse(rawAction.VariableValue));
                case "ToggleVariableAction":
                    // Add the toggle variable action to the timeline.
                    if (rawAction.VariableName == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the variable name is null.");
                    }
                    return new ToggleVariableAction(Game, rawAction.VariableName);
                case "CreateMenuAction":
                    // Add the create menu action to the timeline.
                    var menu = FetchMenuFromImport(rawAction);
                    return new CreateMenuAction(Game, menu, [.. menu.BlockList]);
                default:
                    throw new InvalidOperationException("Failed to load scene settings, because Either the action type is not recognized, or the event is not a timeline dependent or a general one.");
            }
        }
        /// <summary>
        /// Creates a timeline independent event from the importer class
        /// Get event from the union of the timeline independent and general events.
        /// </summary>
        /// <param name="rawAction"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        private ISettingsEvent FetchTimelineIndependentEventFromImport(ActionImport rawAction)
        {
            switch (rawAction.Type)
            {
                case "NativeLoadSceneAction":
                    // Add the native load scene action to the timeline.
                    if (rawAction.SceneID.HasValue is false)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the scene id is null.");
                    }
                    var nativeSceneId = rawAction.SceneID.Value;
                    ISettingsEvent NativeLoadSceneAction = (ISettingsEvent)new NativeLoadSceneAction(Game, nativeSceneId);
                    return NativeLoadSceneAction;
                case "LoadSceneAction":
                    // Add the load scene action to the timeline.
                    if (rawAction.SceneID.HasValue is false)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the scene id is null.");
                    }
                    var sceneId = rawAction.SceneID.Value;
                    IEvent LoadSceneAction = new LoadSceneAction(Game, sceneId, rawAction.TriggerVariableName);
                    return (ISettingsEvent)LoadSceneAction;
                case "SetVariableValueAction":
                    // Add the set variable value action to the timeline.
                    if (rawAction.VariableName == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the variable name is null.");
                    }
                    ISettingsEvent SetVariableValueAction = new SetVariableValueAction(Game, rawAction.VariableName, this, rawAction.BlockComponentID);
                    return SetVariableValueAction;
                case "CreateMenuAction":
                    // Add the create menu action to the timeline.
                    var menu = FetchMenuFromImport(rawAction);
                    return new CreateMenuAction(Game, menu, [.. menu.BlockList]);
                case "SwitchStaticMenuAction":
                    // Add the switch static menu action to the timeline.
                    if (rawAction.DisablingMenuID == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the disabling menu id is null.");
                    }
                    if (rawAction.EnablingMenuID == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the enabling menu id is null.");
                    }
                    return new SwitchStaticMenuAction(Game, this, rawAction.DisablingMenuID.Value, rawAction.EnablingMenuID.Value);
                default:
                    throw new InvalidOperationException("Failed to load scene settings,  because Either the action type is not recognized, or the event is not a timeline independent or a general one.");
            }
        }
    }
    /// <summary>
    /// The Game is the main class of the game.
    /// The Game class contains the path to the following files: gamesettings, scenes, variables
    /// Game class accesses the GameLoader Class
    /// </summary>
    public class Game
    {
        /// <summary>
        /// The GameLoader deals with the raw data which is loaded into the game.
        /// </summary>
        private GameLoader GameLoader;
        /// <summary>
        /// The current folder path.
        /// </summary>
        internal const string currentFolderPath = "../../../src/Data/";
        /// <summary>
        /// The Game settings path.
        /// </summary>
        internal const string relativeGameSettingsPath = currentFolderPath + "GameSettings.json";
        /// <summary>
        /// The Scene path.
        /// </summary>
        internal const string relativeScenePath = currentFolderPath + "Scenes.json";
        /// <summary>
        /// The Variable path.
        /// </summary>
        internal const string relativeVariablePath = currentFolderPath + "Variables.json";
        /// <summary>
        /// Temporary Game settings.
        /// </summary>
        internal GameImport GameSettings { get; private set; }
        /// <summary>
        /// List of scenes.
        /// </summary>
        public List<Scene> Scenes { get; set; }
        /// <summary>
        /// List of variables.
        /// </summary>
        public List<Variable> VariableList { get; set; }
        /// <summary>
        /// The active scene.
        /// </summary>
        public Scene ActiveScene { get; private set; }

        public Game()
        {
            SetupGameSettings();
        }

        /// <summary>
        /// Fetches all the correspondant json files, and loads them in the game.
        /// </summary>
        internal void SetupGameSettings()
        {
            //Fetch game settings.
            SetupGameWindow();
            //Fetch variables
            SetupVariables();
            //Fetch scenes
            SetupScenes();
        }

        /// <summary>
        /// Fetches the game settings from the json file.
        /// </summary>
        private void SetupGameWindow()
        {
            string rawFile = File.ReadAllText(relativeGameSettingsPath);
            var rawSettings = JsonSerializer.Deserialize<GameImport>(rawFile);
            if (rawSettings != null) GameSettings = rawSettings;
            else
            {
                throw new InvalidOperationException("Failed to load game settings, because the file is null.");
            }
            // Set the window title and size.
            Raylib.SetWindowTitle(GameSettings.Title);
            Raylib.SetWindowSize(GameSettings.WindowWidth, GameSettings.WindowHeigth);
        }
        /// <summary>
        /// Fetches the saved variables from the json file.
        /// </summary>
        private void SetupVariables()
        {
            // Initialize the list of variables.
            VariableList = [];
            //
            string rawFile = File.ReadAllText(relativeVariablePath);
            var rawVariables = JsonSerializer.Deserialize<List<VariableImport>>(rawFile);
            if (rawVariables != null)
            {
                foreach (var variable in rawVariables)
                {
                    switch (variable.Type)
                    {
                        case 1:
                            VariableList.Add(new Variable(variable.Name, variable.Value, VariableType.String));
                            break;
                        case 2:
                            VariableList.Add(new Variable(variable.Name, variable.Value, VariableType.Int));
                            break;
                        case 3:
                            VariableList.Add(new Variable(variable.Name, variable.Value, VariableType.Float));
                            break;
                        case 4:
                            VariableList.Add(new Variable(variable.Name, variable.Value, VariableType.Boolean));
                            break;
                        default:
                            throw new InvalidOperationException("Failed to load variable settings, because the variable type is not recognized.");
                    }
                }
            }
        }
        /// <summary>
        /// Fetches the scene configuration from the json file.
        /// </summary>
        private void SetupScenes()
        {
            // Initialize the list of scenes.
            Scenes = [];
            // Initialize the game loader.
            GameLoader = new(this);
            // Fetch the scene settings.
            string rawFile = File.ReadAllText(relativeScenePath);
            var rawScenes = JsonSerializer.Deserialize<List<SceneImport>>(rawFile);
            if (rawScenes != null)
            {
                foreach (var scene in rawScenes)
                {
                    Timeline timeline = new();
                    if (scene.ActionList == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the action list is null.");
                    }
                    else
                        for (int i = 0; i < scene.ActionList.Length; i++)
                        {
                            timeline.ActionList.Add(GameLoader.FetchTimelineDependentEventFromImport(scene.ActionList[i]));
                        }
                    Scenes.Add(new Scene(scene.Name, this)
                    {
                        Id = scene.ID,
                        Background = Enum.Parse<Scene.BackgroundOption>(scene.Background),
                        solidColor = scene.SolidColor == null ? Color.Black : scene.SolidColor.Length == 0 ? new() : new()
                        {
                            R = (byte)scene.SolidColor[0],
                            G = (byte)scene.SolidColor[1],
                            B = (byte)scene.SolidColor[2],
                            A = (byte)scene.SolidColor[3]
                        },
                        gradientColor = scene.GradientColor == null ? [] : scene.GradientColor.Length == 0 ? [] : [new()
                        {
                                                                        R = (byte)scene.GradientColor[0],
                                                                        G = (byte)scene.GradientColor[1],
                                                                        B = (byte)scene.GradientColor[2],
                                                                        A = (byte)scene.GradientColor[3]
                                                                        },
                                                                        new()
                                                                        {
                                                                        R = (byte)scene.GradientColor[4],
                                                                        G = (byte)scene.GradientColor[5],
                                                                        B = (byte)scene.GradientColor[6],
                                                                        A = (byte)scene.GradientColor[7]
                                                                        }],
                        imageTexture = scene.ImageTexture == null ? Raylib.LoadTexture("") : Raylib.LoadTexture(scene.ImageTexture),
                        Timeline = timeline
                    });
                }
            }
            else
            {
                // If the file is null, throw an exception.
                // A file with empty project will always generate a file. This exception will never be thrown in normal use.
                throw new InvalidOperationException("Failed to load scene settings, because the file is null.");
            }
            ActiveScene = Scenes[0];
            ActiveScene.Timeline.StartTimeline();
        }
        /// <summary>
        /// Loads the scene.
        /// </summary>
        /// <param name="scene">To be loaded scene.</param>
        internal void LoadScene(Scene scene)
        {
            ActiveScene = scene;
            ActiveScene.Timeline.StartTimeline();
        }
        /// <summary>
        /// Updates the scene.
        /// </summary>
        public void UpdateScene()
        {
            switch (ActiveScene.Background)
            {
                default:
                    // If the background is not set, clear the screen with black.
                    Raylib.ClearBackground(Color.Black);
                    break;
                case Scene.BackgroundOption.SolidColor:
                    // If the background is set to solid color, clear the screen with the color.
                    Raylib.ClearBackground(ActiveScene.solidColor);
                    break;
                case Scene.BackgroundOption.GradientHorizontal:
                    // If the background is set to gradient horizontal, draw the gradient and clear the screen with black.
                    Raylib.DrawRectangleGradientH(0, 0, Raylib.GetScreenWidth(), Raylib.GetScreenHeight(), ActiveScene.gradientColor[0], ActiveScene.gradientColor[1]);
                    Raylib.ClearBackground(Color.Black);
                    break;
                case Scene.BackgroundOption.GradientVertical:
                    // If the background is set to gradient vertical, draw the gradient and clear the screen with black.
                    Raylib.DrawRectangleGradientV(0, 0, Raylib.GetScreenWidth(), Raylib.GetScreenHeight(), ActiveScene.gradientColor[0], ActiveScene.gradientColor[1]);
                    Raylib.ClearBackground(Color.Black);
                    break;
                case Scene.BackgroundOption.Image:
                    // If the background is set to image, draw the image and clear the screen with black.
                    ActiveScene.imageTexture.Width = Raylib.GetScreenWidth();
                    ActiveScene.imageTexture.Height = Raylib.GetScreenHeight();
                    //
                    Raylib.DrawTexture(ActiveScene.imageTexture,
                    Raylib.GetScreenWidth() / 2 - ActiveScene.imageTexture.Width / 2,
                    Raylib.GetScreenHeight() / 2 - ActiveScene.imageTexture.Height / 2,
                    Color.White);
                    Raylib.ClearBackground(Color.Black);
                    break;
            }
            //
            ActiveScene.Timeline.ExecuteAction();
            ActiveScene.Timeline.RenderSprites();
        }
        /// <summary>
        /// Checks if the left mouse button is pressed.
        /// </summary>
        /// <returns></returns>
        public static bool IsLeftMouseButtonPressed() => Raylib.IsMouseButtonPressed(MouseButton.Left);
        /// <summary>
        /// Checks if the right mouse button is pressed.
        /// </summary>
        /// <returns></returns>
        public static bool IsRightMouseButtonPressed() => Raylib.IsMouseButtonPressed(MouseButton.Right);
        /// <summary>
        /// Checks if the escape button is pressed.
        /// </summary>
        /// <returns></returns>
        public static bool IsEscapeButtonPressed() => Raylib.WindowShouldClose();
        public static int GetMouseXPosition() => Raylib.GetMouseX();
        public static int GetMouseYPosition() => Raylib.GetMouseY();
        public static bool IsKeyPressed(KeyboardKey key) => Raylib.IsKeyPressed(key);
        public static bool IsKeyDown(KeyboardKey key) => Raylib.IsKeyDown(key);
    }
}
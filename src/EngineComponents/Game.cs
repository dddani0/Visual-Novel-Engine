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
        public string? ImpendingVariableName { get; set; }
        [JsonPropertyName("Menu")]
        public MenuImport? Menu { get; set; }
        [JsonPropertyName("StaticMenu")]
        public MenuImport? StaticMenu { get; set; }
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
    /// The MenuImport class is a helper class to import the list of menus from a json file.
    /// </summary>
    internal class MenuImport
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
        public BlockImport[]? BlockList { get; set; }
        [JsonPropertyName("Color")]
        public int[]? Color { get; set; }
        [JsonPropertyName("BorderColor")]
        public int[]? BorderColor { get; set; }
    }

    /// <summary>
    /// The BlockImport class is a helper class to import the list of blocks from a json file.
    /// </summary>
    internal class BlockImport
    {
        [JsonPropertyName("ID")]
        public required long ID { get; set; }
        [JsonPropertyName("XPosition")]
        public int XPosition { get; set; }
        [JsonPropertyName("YPosition")]
        public int YPosition { get; set; }
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
        [JsonPropertyName("Event")]
        public required ActionImport Event { get; set; }
    }
    /// <summary>
    /// The TextBoxCreateAction class is a helper class to import the TextField component.
    /// </summary>
    internal class InputFieldImport
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
        [JsonPropertyName("ButtonEvent")]
        public required ActionImport ButtonEvent { get; set; }
        [JsonPropertyName("BorderWidth")]
        public int BorderWidth { get; set; }
        [JsonPropertyName("Color")]
        public required int[] Color { get; set; }
        [JsonPropertyName("BorderColor")]
        public required int[] BorderColor { get; set; }
        [JsonPropertyName("HoverColor")]
        public required int[] HoverColor { get; set; }
    }
    /// <summary>
    /// The DropBoxImport class is a helper class to import the DropBox component.
    /// </summary>
    internal class DropBoxImport
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
        public required DropBoxOptionImport[] Options { get; set; }
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
    /// The DropBoxOptionImport class is a helper class to import the DropBoxOption component.
    /// </summary>
    internal class DropBoxOptionImport
    {
        [JsonPropertyName("Text")]
        public required string Text { get; set; }
        [JsonPropertyName("Event")]
        public required ActionImport Event { get; set; }
    }
    /// <summary>
    /// The SliderImport class is a helper class to import the Slider component.
    /// </summary>
    internal class SliderImport
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
        [JsonPropertyName("DragColor")]
        public required int[] DragColor { get; set; }
        [JsonPropertyName("Color")]
        public required int[] Color { get; set; }
        [JsonPropertyName("BorderColor")]
        public required int[] BorderColor { get; set; }
        [JsonPropertyName("Event")]
        public required ActionImport Event { get; set; }
    }
    /// <summary>
    /// The ToggleImport class is a helper class to import the Toggle component.
    /// </summary>
    internal class ToggleImport
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
        [JsonPropertyName("Color")]
        public required int[] Color { get; set; }
        [JsonPropertyName("BorderColor")]
        public required int[] BorderColor { get; set; }
        [JsonPropertyName("ActivatedColor")]
        public required int[] ActivatedColor { get; set; }
        [JsonPropertyName("Event")]
        public required ActionImport Event { get; set; }
    }
    /// <summary>
    /// The TextFieldImport class is a helper class to import the TextField component.
    /// </summary>
    internal class TextFieldImport
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
    /// The SpriteImport class is a helper class to import the Sprite component.
    /// </summary>
    internal class SpriteImport
    {
        [JsonPropertyName("Path")]
        public required string Path { get; set; }
        [JsonPropertyName("XPosition")]
        public int? XPosition { get; set; }
        [JsonPropertyName("YPosition")]
        public int? YPosition { get; set; }
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
            if (rawAction.Path == null)
            {
                throw new InvalidOperationException("Failed to load scene settings, because the sprite path is null.");
            }
            if (File.Exists(Game.currentFolderPath + rawAction.Path) is false)
            {
                throw new InvalidOperationException("Failed to load scene settings, because the sprite path is invalid.");
            }
            return new Sprite(Game.currentFolderPath + rawAction.Path);
        }
        /// <summary>
        /// Creates a variable from the importer class
        /// </summary>
        /// <param name="spriteImport"></param>
        /// <param name="block"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        Sprite FetchSpriteFromImport(SpriteImport spriteImport, Block block)
        {
            if (spriteImport.Path == null)
            {
                throw new InvalidOperationException("Failed to load scene settings, because the sprite path is null.");
            }
            if (spriteImport.XPosition == null)
            {
                throw new InvalidOperationException("Failed to load scene settings, because the sprite x position is null.");
            }
            if (spriteImport.YPosition == null)
            {
                throw new InvalidOperationException("Failed to load scene settings, because the sprite y position is null.");
            }
            if (File.Exists(Game.currentFolderPath + spriteImport.Path) is false)
            {
                throw new InvalidOperationException("Failed to load scene settings, because the sprite path is invalid.");
            }
            return new Sprite(Game.currentFolderPath + spriteImport.Path, block, spriteImport.XPosition.Value, spriteImport.YPosition.Value);
        }
        /// <summary>
        /// Creates a variable from the importer class
        /// </summary>
        /// <param name="variableImport"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        Variable FetchVariableFromImport(ActionImport variableImport)
        {
            if (variableImport.VariableName == null)
            {
                throw new InvalidOperationException("Failed to load scene settings, because the variable name is null.");
            }
            if (variableImport.VariableValue == null)
            {
                throw new InvalidOperationException("Failed to load scene settings, because the variable value is null.");
            }
            if (variableImport.VariableType == null)
            {
                throw new InvalidOperationException("Failed to load scene settings, because the variable type is null.");
            }
            return new Variable(variableImport.VariableName, variableImport.VariableValue, (VariableType)variableImport.VariableType);
        }
        /// <summary>
        /// Creates a button with a Timeline dependent or a general event attached to it from the importer class
        /// </summary>
        /// <param name="buttonImport"></param>
        /// <param name="block"></param>
        /// <returns></returns>
        Button FetchButtonFromImport(ButtonComponentImport buttonImport, Block block)
        {
            return new Button(
                Game,
                block,
                new Font() { BaseSize = 30, GlyphPadding = 5 },
                buttonImport.XPosition,
                buttonImport.YPosition,
                buttonImport.BorderWidth,
                buttonImport.Width,
                buttonImport.Height,
                buttonImport.Text,
                new Color()
                {
                    R = (byte)buttonImport.TextColor[0],
                    G = (byte)buttonImport.TextColor[1],
                    B = (byte)buttonImport.TextColor[2],
                    A = (byte)buttonImport.TextColor[3]
                },
                new Color()
                {
                    R = (byte)buttonImport.Color[0],
                    G = (byte)buttonImport.Color[1],
                    B = (byte)buttonImport.Color[2],
                    A = (byte)buttonImport.Color[3]
                },
                new Color()
                {
                    R = (byte)buttonImport.BorderColor[0],
                    G = (byte)buttonImport.BorderColor[1],
                    B = (byte)buttonImport.BorderColor[2],
                    A = (byte)buttonImport.BorderColor[3]
                },
                new Color()
                {
                    R = (byte)buttonImport.HoverColor[0],
                    G = (byte)buttonImport.HoverColor[1],
                    B = (byte)buttonImport.HoverColor[2],
                    A = (byte)buttonImport.HoverColor[3]
                },
                (IButtonEvent)FetchTimelineDependentEventFromImport(buttonImport.Event)
            );
        }
        /// <summary>
        /// Creates a button with a Timeline independent or a general event attached to it from the importer class
        /// </summary>
        /// <param name="staticButtonImport"></param>
        /// <param name="block"></param>
        /// <returns></returns>
        Button FetchStaticButtonFromImport(ButtonComponentImport staticButtonImport, Block block)
        {
            return new Button(
                            Game,
                            block,
                            new Font() { BaseSize = 30, GlyphPadding = 5 },
                            staticButtonImport.XPosition,
                            staticButtonImport.YPosition,
                            staticButtonImport.BorderWidth,
                            staticButtonImport.Width,
                            staticButtonImport.Height,
                            staticButtonImport.Text,
                            new Color()
                            {
                                R = (byte)staticButtonImport.TextColor[0],
                                G = (byte)staticButtonImport.TextColor[1],
                                B = (byte)staticButtonImport.TextColor[2],
                                A = (byte)staticButtonImport.TextColor[3]
                            },
                            new Color()
                            {
                                R = (byte)staticButtonImport.Color[0],
                                G = (byte)staticButtonImport.Color[1],
                                B = (byte)staticButtonImport.Color[2],
                                A = (byte)staticButtonImport.Color[3]
                            },
                            new Color()
                            {
                                R = (byte)staticButtonImport.BorderColor[0],
                                G = (byte)staticButtonImport.BorderColor[1],
                                B = (byte)staticButtonImport.BorderColor[2],
                                A = (byte)staticButtonImport.BorderColor[3]
                            },
                            new Color()
                            {
                                R = (byte)staticButtonImport.HoverColor[0],
                                G = (byte)staticButtonImport.HoverColor[1],
                                B = (byte)staticButtonImport.HoverColor[2],
                                A = (byte)staticButtonImport.HoverColor[3]
                            },
                            FetchTimelineIndependentEventFromImport(staticButtonImport.Event)
                        );
        }
        /// <summary>
        /// Creates an option from the importer class
        /// </summary>
        /// <param name="DropBoxOptionImport"></param>
        /// <param name="block"></param>
        /// <returns></returns>
        Button FetchDropBoxOptionFromImport(DropBoxImport dropBoxImport, DropBoxOptionImport DropBoxOptionImport, Block block)
        {
            //Each dropbox option's Y position is calculated by the height of the dropbox and the index of the option in the DropBox.cs class.
            return DropBox.GetDropBoxOption(
                Game,
                block,
                new Font() { BaseSize = 10, GlyphPadding = 2 },
                0,
                0,
                0,
                dropBoxImport.Width,
                dropBoxImport.Height,
                DropBoxOptionImport.Text,
                new Color()
                {
                    R = (byte)dropBoxImport.TextColor[0],
                    G = (byte)dropBoxImport.TextColor[1],
                    B = (byte)dropBoxImport.TextColor[2],
                    A = (byte)dropBoxImport.TextColor[3]
                },
                new Color()
                {
                    R = (byte)dropBoxImport.Color[0],
                    G = (byte)dropBoxImport.Color[1],
                    B = (byte)dropBoxImport.Color[2],
                    A = (byte)dropBoxImport.Color[3]
                },
                new Color()
                {
                    R = (byte)dropBoxImport.BorderColor[0],
                    G = (byte)dropBoxImport.BorderColor[1],
                    B = (byte)dropBoxImport.BorderColor[2],
                    A = (byte)dropBoxImport.BorderColor[3]
                },
                new Color()
                {
                    R = (byte)dropBoxImport.HoverColor[0],
                    G = (byte)dropBoxImport.HoverColor[1],
                    B = (byte)dropBoxImport.HoverColor[2],
                    A = (byte)dropBoxImport.HoverColor[3]
                },
                FetchTimelineIndependentEventFromImport(DropBoxOptionImport.Event)
            );
        }
        /// <summary>
        /// Creates a dropbox from the importer class
        /// </summary>
        /// <param name="dropBoxImport"></param>
        /// <param name="block"></param>
        /// <returns></returns>
        DropBox FetchDropBoxFromImport(DropBoxImport dropBoxImport, Block block)
        {
            return new DropBox(
                                    block,
                                    dropBoxImport.XPosition,
                                    dropBoxImport.YPosition,
                                    dropBoxImport.Width,
                                    dropBoxImport.Height,
                                    dropBoxImport.Options.Select(option => FetchDropBoxOptionFromImport(dropBoxImport, option, block)).ToArray(),
                                    new Color()
                                    {
                                        R = (byte)dropBoxImport.Color[0],
                                        G = (byte)dropBoxImport.Color[1],
                                        B = (byte)dropBoxImport.Color[2],
                                        A = (byte)dropBoxImport.Color[3]
                                    },
                                    new Color()
                                    {
                                        R = (byte)dropBoxImport.BorderColor[0],
                                        G = (byte)dropBoxImport.BorderColor[1],
                                        B = (byte)dropBoxImport.BorderColor[2],
                                        A = (byte)dropBoxImport.BorderColor[3]
                                    },
                                    new Color()
                                    {
                                        R = (byte)dropBoxImport.HoverColor[0],
                                        G = (byte)dropBoxImport.HoverColor[1],
                                        B = (byte)dropBoxImport.HoverColor[2],
                                        A = (byte)dropBoxImport.HoverColor[3]
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
                inputFieldImport.PlaceholderText,
                inputFieldImport.ButtonText,
                new Color()
                {
                    R = (byte)inputFieldImport.Color[0],
                    G = (byte)inputFieldImport.Color[1],
                    B = (byte)inputFieldImport.Color[2],
                    A = (byte)inputFieldImport.Color[3]
                },
                new Color()
                {
                    R = (byte)inputFieldImport.BorderColor[0],
                    G = (byte)inputFieldImport.BorderColor[1],
                    B = (byte)inputFieldImport.BorderColor[2],
                    A = (byte)inputFieldImport.BorderColor[3]
                },
                new Color()
                {
                    R = (byte)inputFieldImport.HoverColor[0],
                    G = (byte)inputFieldImport.HoverColor[1],
                    B = (byte)inputFieldImport.HoverColor[2],
                    A = (byte)inputFieldImport.HoverColor[3]
                },
                (IButtonEvent)FetchTimelineDependentEventFromImport(inputFieldImport.ButtonEvent)
            );
        }
        /// <summary>
        /// Creates a static input field from the importer class
        /// </summary>
        /// <param name="staticInputFieldImport"></param>
        /// <param name="block"></param>
        /// <returns></returns>
        InputField FetchStaticInputFieldFromImport(InputFieldImport staticInputFieldImport, Block block)
        {
            return new InputField(
                Game,
                block,
                staticInputFieldImport.XPosition,
                staticInputFieldImport.YPosition,
                staticInputFieldImport.ButtonYOffset,
                staticInputFieldImport.Width,
                staticInputFieldImport.Height,
                staticInputFieldImport.PlaceholderText,
                staticInputFieldImport.ButtonText,
                new Color()
                {
                    R = (byte)staticInputFieldImport.Color[0],
                    G = (byte)staticInputFieldImport.Color[1],
                    B = (byte)staticInputFieldImport.Color[2],
                    A = (byte)staticInputFieldImport.Color[3]
                },
                new Color()
                {
                    R = (byte)staticInputFieldImport.BorderColor[0],
                    G = (byte)staticInputFieldImport.BorderColor[1],
                    B = (byte)staticInputFieldImport.BorderColor[2],
                    A = (byte)staticInputFieldImport.BorderColor[3]
                },
                new Color()
                {
                    R = (byte)staticInputFieldImport.HoverColor[0],
                    G = (byte)staticInputFieldImport.HoverColor[1],
                    B = (byte)staticInputFieldImport.HoverColor[2],
                    A = (byte)staticInputFieldImport.HoverColor[3]
                },
                FetchTimelineIndependentEventFromImport(staticInputFieldImport.ButtonEvent)
            );
        }
        /// <summary>
        /// Creates a slider from the importer class
        /// </summary>
        /// <param name="sliderImport"></param>
        /// <param name="block"></param>
        /// <returns></returns>
        Slider FetchSliderFromImport(SliderImport sliderImport, Block block)
        {
            return new Slider(
                block,
                sliderImport.XPosition,
                sliderImport.YPosition,
                sliderImport.Width,
                sliderImport.Height,
                sliderImport.BorderWidth,
                sliderImport.DragRadius,
                new Color()
                {
                    R = (byte)sliderImport.DragColor[0],
                    G = (byte)sliderImport.DragColor[1],
                    B = (byte)sliderImport.DragColor[2],
                    A = (byte)sliderImport.DragColor[3]
                },
                new Color()
                {
                    R = (byte)sliderImport.Color[0],
                    G = (byte)sliderImport.Color[1],
                    B = (byte)sliderImport.Color[2],
                    A = (byte)sliderImport.Color[3]
                },
                new Color()
                {
                    R = (byte)sliderImport.BorderColor[0],
                    G = (byte)sliderImport.BorderColor[1],
                    B = (byte)sliderImport.BorderColor[2],
                    A = (byte)sliderImport.BorderColor[3]
                },
                FetchTimelineIndependentEventFromImport(sliderImport.Event)
            );
        }
        /// <summary>
        /// Creates a toggle from the importer class
        /// </summary>
        /// <param name="toggleImport"></param>
        /// <param name="block"></param>
        /// <returns></returns>
        Toggle FetchToggleFromImport(ToggleImport toggleImport, Block block)
        {
            return new Toggle(
                 block,
                 toggleImport.XPosition,
                 toggleImport.YPosition,
                 toggleImport.BoxSize,
                 toggleImport.TextXOffset,
                 toggleImport.Text,
                 false,
                 new Color()
                 {
                     R = (byte)toggleImport.Color[0],
                     G = (byte)toggleImport.Color[1],
                     B = (byte)toggleImport.Color[2],
                     A = (byte)toggleImport.Color[3]
                 },
                 new Color()
                 {
                     R = (byte)toggleImport.BorderColor[0],
                     G = (byte)toggleImport.BorderColor[1],
                     B = (byte)toggleImport.BorderColor[2],
                     A = (byte)toggleImport.BorderColor[3]
                 },
                 new Color()
                 {
                     R = (byte)toggleImport.ActivatedColor[0],
                     G = (byte)toggleImport.ActivatedColor[1],
                     B = (byte)toggleImport.ActivatedColor[2],
                     A = (byte)toggleImport.ActivatedColor[3]
                 },
                 FetchTimelineIndependentEventFromImport(toggleImport.Event)
             );
        }
        /// <summary>
        /// Creates a text field from the importer class
        /// </summary>
        /// <param name="textFieldImport"></param>
        /// <param name="block"></param>
        /// <returns></returns>
        TextField FetchTextFieldFromImport(TextFieldImport textFieldImport, Block block)
        {
            return new TextField(
                block,
                textFieldImport.XPosition,
                textFieldImport.YPosition,
                textFieldImport.Width,
                textFieldImport.Height,
                textFieldImport.BorderWidth,
                textFieldImport.HorizontalTextMargin,
                textFieldImport.VerticalTextMargin,
                textFieldImport.Text,
                new Font() { BaseSize = 30, GlyphPadding = 5 },
                textFieldImport.IsVisible == "True",
                textFieldImport.WordWrap == "True",
                new Color()
                {
                    R = (byte)textFieldImport.Color[0],
                    G = (byte)textFieldImport.Color[1],
                    B = (byte)textFieldImport.Color[2],
                    A = (byte)textFieldImport.Color[3]
                },
                new Color()
                {
                    R = (byte)textFieldImport.BorderColor[0],
                    G = (byte)textFieldImport.BorderColor[1],
                    B = (byte)textFieldImport.BorderColor[2],
                    A = (byte)textFieldImport.BorderColor[3]
                }
            );
        }
        /// <summary>
        /// Creates a block from the importer class
        /// </summary>
        /// <param name="blockImport"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        Block FetchBlockFromImport(BlockImport blockImport)
        {
            var newBlock = new Block(
                blockImport.XPosition,
                blockImport.YPosition,
                null,
                blockImport.ID
                );
            // The block has a button component.
            if (blockImport.Button != null)
                newBlock.SetComponent(FetchButtonFromImport(blockImport.Button, newBlock));
            // The block has an InputField component.
            else if (blockImport.InputField != null)
                newBlock.SetComponent(FetchInputFieldFromImport(blockImport.InputField, newBlock));
            // The block has a TextField component.
            else if (blockImport.TextField != null)
                newBlock.SetComponent(FetchTextFieldFromImport(blockImport.TextField, newBlock));
            // The block has a Sprite component.
            else if (blockImport.Sprite != null)
                newBlock.SetComponent(FetchSpriteFromImport(blockImport.Sprite, newBlock));
            else
                throw new InvalidOperationException("Failed to load scene settings, because either the component type attached to the block is not recognized or the type is static.");
            BlockListCache.Add(newBlock);
            return newBlock;
        }
        /// <summary>
        /// Creates a static block from the importer class
        /// </summary>
        /// <param name="staticBlockImport"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        Block FetchStaticBlockFromImport(BlockImport staticBlockImport)
        {
            var newBlock = new Block(
                    staticBlockImport.XPosition,
                    staticBlockImport.YPosition,
                    null,
                    staticBlockImport.ID
                );
            // The block has a static button component.
            if (staticBlockImport.StaticButton != null)
                newBlock.SetComponent(FetchStaticButtonFromImport(staticBlockImport.StaticButton, newBlock));
            // The block has a dropbox component.
            else if (staticBlockImport.DropBox != null)
                newBlock.SetComponent(FetchDropBoxFromImport(staticBlockImport.DropBox, newBlock));
            // The block has an InputField component.
            else if (staticBlockImport.StaticInputField != null)
                newBlock.SetComponent(FetchStaticInputFieldFromImport(staticBlockImport.StaticInputField, newBlock));
            // The block has a Slider component.
            else if (staticBlockImport.Slider != null)
                newBlock.SetComponent(FetchSliderFromImport(staticBlockImport.Slider, newBlock));
            // The block has a Toggle component.
            else if (staticBlockImport.Toggle != null)
                newBlock.SetComponent(FetchToggleFromImport(staticBlockImport.Toggle, newBlock));
            // The block has a TextField component.
            else if (staticBlockImport.TextField != null)
                newBlock.SetComponent(FetchTextFieldFromImport(staticBlockImport.TextField, newBlock));
            // The block has a Sprite component.
            else if (staticBlockImport.Sprite != null)
                newBlock.SetComponent(FetchSpriteFromImport(staticBlockImport.Sprite, newBlock));
            else
                throw new InvalidOperationException("Failed to load scene settings, because either the component type attached to the block is not recognized or the type is not static.");
            BlockListCache.Add(newBlock);
            return newBlock;
        }
        /// <summary>
        /// Creates a menu with static or non-static components from the importer class
        /// </summary>
        /// <param name="menuImport"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        Menu FetchMenuFromImport(MenuImport menuImport)
        {
            if (menuImport.XPosition == null)
            {
                throw new InvalidOperationException("Failed to load scene settings, because the menu X position is null.");
            }
            if (menuImport.YPosition == null)
            {
                throw new InvalidOperationException("Failed to load scene settings, because the menu Y position is null.");
            }
            if (menuImport.Width == null)
            {
                throw new InvalidOperationException("Failed to load scene settings, because the menu width is null.");
            }
            if (menuImport.Height == null)
            {
                throw new InvalidOperationException("Failed to load scene settings, because the menu height is null.");
            }
            if (menuImport.FullScreen == null)
            {
                throw new InvalidOperationException("Failed to load scene settings, because the menu fullscreen is null.");
            }
            if (menuImport.Color == null)
            {
                throw new InvalidOperationException("Failed to load scene settings, because the menu color is null.");
            }
            if (menuImport.BorderColor == null)
            {
                throw new InvalidOperationException("Failed to load scene settings, because the menu border color is null.");
            }
            var menuXPosition = menuImport.XPosition.Value;
            var menuYPosition = menuImport.YPosition.Value;
            var menuWidth = menuImport.Width.Value;
            var menuHeight = menuImport.Height.Value;
            var menuFullScreen = menuImport.FullScreen == "True";
            var windowColor = new Color()
            {
                R = (byte)menuImport.Color[0],
                G = (byte)menuImport.Color[1],
                B = (byte)menuImport.Color[2],
                A = (byte)menuImport.Color[3]
            };
            var windowBorderColor = new Color()
            {
                R = (byte)menuImport.BorderColor[0],
                G = (byte)menuImport.BorderColor[1],
                B = (byte)menuImport.BorderColor[2],
                A = (byte)menuImport.BorderColor[3]
            };
            var id = menuImport.ID;
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
            if (menuImport.BlockList == null) return menu;
            menu.BlockList.AddRange(menuImport.Static == "True" ? menuImport.BlockList.Select(block => FetchStaticBlockFromImport(block)) : menuImport.BlockList.Select(block => FetchBlockFromImport(block)));
            return menu;
        }
        /// <summary>
        /// Creates a timeline independent event from the importer class
        /// </summary>
        /// <param name="actionImport"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        internal IEvent FetchTimelineDependentEventFromImport(ActionImport actionImport)
        {
            switch (actionImport.Type)
            {
                case "TextBoxCreateAction":
                    // Add the textbox to the timeline.
                    if (actionImport.CharactersPerSecond.HasValue is false)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the characters per second is null.");
                    }
                    if (actionImport.Font == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the font is null.");
                    }
                    if (actionImport.TextBoxColor == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the textbox color is null.");
                    }
                    if (actionImport.TextBoxBorder == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the textbox border is null.");
                    }
                    if (actionImport.PositionType.HasValue is false)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the position type is null.");
                    }
                    if (actionImport.HorizontalTextMargin.HasValue is false)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the horizontal text margin is null.");
                    }
                    if (actionImport.VerticalTextMargin.HasValue is false)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the vertical text margin is null.");
                    }
                    if (actionImport.WordWrap.HasValue is false)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the word wrap is null.");
                    }
                    if (actionImport.TextBoxTitle == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the textbox title is null.");
                    }
                    if (actionImport.TextBoxContent == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the textbox content is null.");
                    }
                    var charactersPerSecond = actionImport.CharactersPerSecond.Value;
                    var font = new Font() { BaseSize = 30, GlyphPadding = 5 };
                    var textboxcolor = actionImport.TextBoxColor == null || actionImport.TextBoxColor.Length < 4 ?
                    new Color() { R = 0, G = 0, B = 0, A = 255 } :
                    new Color()
                    {
                        R = (byte)actionImport.TextBoxColor[0],
                        G = (byte)actionImport.TextBoxColor[1],
                        B = (byte)actionImport.TextBoxColor[2],
                        A = (byte)actionImport.TextBoxColor[3]
                    };
                    var textboxborder = actionImport.TextBoxBorder == null || actionImport.TextBoxBorder.Length < 4 ?
                    new Color() { R = 0, G = 0, B = 0, A = 255 } :
                    new Color()
                    {
                        R = (byte)actionImport.TextBoxBorder[0],
                        G = (byte)actionImport.TextBoxBorder[1],
                        B = (byte)actionImport.TextBoxBorder[2],
                        A = (byte)actionImport.TextBoxBorder[3]
                    };
                    var positionType = (TextBox.PositionType)actionImport.PositionType.Value;
                    var horizontalTextMargin = actionImport.HorizontalTextMargin.Value;
                    var verticalTextMargin = actionImport.VerticalTextMargin.Value;
                    var wordWrap = actionImport.WordWrap.Value;
                    var textBoxTitle = actionImport.TextBoxTitle;
                    var textBoxContent = actionImport.TextBoxContent;
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
                    if (actionImport.Sprite == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the sprite is null.");
                    }
                    var sprite = FetchSpriteFromImport(actionImport.Sprite);
                    return new AddSpriteAction(sprite, Game);
                case "TintSpriteAction":
                    // Add the tint action to the timeline.
                    if (actionImport.Sprite == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the sprite is null.");
                    }
                    if (actionImport.TintColor == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the tint color is null.");
                    }
                    var tintSprite = FetchSpriteFromImport(actionImport.Sprite);
                    var tintColor = new Color()
                    {
                        R = (byte)actionImport.TintColor[0],
                        G = (byte)actionImport.TintColor[1],
                        B = (byte)actionImport.TintColor[2],
                        A = (byte)actionImport.TintColor[3]
                    };
                    return new TintSpriteAction(tintSprite, tintColor, Game);
                case "RemoveSpriteAction":
                    // Add the remove action to the timeline.
                    if (actionImport.Sprite == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the sprite is null.");
                    }
                    var removeSprite = FetchSpriteFromImport(actionImport.Sprite);
                    return new RemoveSpriteAction(removeSprite, Game);
                case "LoadSceneAction":
                    // Add the load scene action to the timeline.
                    if (actionImport.SceneID.HasValue is false)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the scene id is null.");
                    }
                    var sceneId = actionImport.SceneID.Value;
                    return new LoadSceneAction(Game, sceneId, actionImport.TriggerVariableName);
                    break;
                case "NativeLoadSceneAction":
                    // Add the native load scene action to the timeline.
                    if (actionImport.SceneID.HasValue is false)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the scene id is null.");
                    }
                    var nativeSceneId = actionImport.SceneID.Value;
                    return new NativeLoadSceneAction(Game, nativeSceneId);
                case "CreateVariableAction":
                    // Add the create variable action to the timeline.
                    var variable = FetchVariableFromImport(actionImport);
                    return new CreateVariableAction(Game, variable);
                case "IncrementVariableAction":
                    // Add the increment variable action to the timeline.
                    if (actionImport.VariableName == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the variable name is null.");
                    }
                    if (actionImport.VariableValue == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the variable value is null.");
                    }
                    if (actionImport.ImpendingVariableName == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the impending variable type is null.");
                    }
                    return new IncrementVariableAction(Game, actionImport.VariableName, actionImport.ImpendingVariableName);
                case "DecrementVariableAction":
                    // Add the decrement variable action to the timeline.
                    if (actionImport.VariableName == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the variable name is null.");
                    }
                    if (actionImport.VariableValue == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the variable value is null.");
                    }
                    if (actionImport.ImpendingVariableName == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the impending variable type is null.");
                    }
                    return new DecrementVariableAction(Game, actionImport.VariableName, actionImport.ImpendingVariableName);
                case "SetVariableTrueAction":
                    // Add the set variable true action to the timeline.
                    if (actionImport.VariableName == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the variable name is null.");
                    }
                    return new SetVariableTrueAction(Game, actionImport.VariableName);
                case "SetVariableFalseAction":
                    // Add the set variable false action to the timeline.
                    if (actionImport.VariableName == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the variable name is null.");
                    }
                    return new SetVariableFalseAction(Game, actionImport.VariableName);
                case "SetBoolVariableAction":
                    // Add the set variable action to the timeline.
                    if (actionImport.VariableName == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the variable name is null.");
                    }
                    if (actionImport.VariableValue == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the variable value is null.");
                    }
                    return new SetBoolVariableAction(Game, actionImport.VariableName, bool.Parse(actionImport.VariableValue));
                case "ToggleVariableAction":
                    // Add the toggle variable action to the timeline.
                    if (actionImport.VariableName == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the variable name is null.");
                    }
                    return new ToggleVariableAction(Game, actionImport.VariableName);
                case "CreateMenuAction":
                    // Add the create menu action to the timeline.
                    if (actionImport.Menu == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the menu is null.");
                    }
                    var menu = FetchMenuFromImport(actionImport.Menu);
                    return new CreateMenuAction(Game, menu, [.. menu.BlockList]);
                default:
                    throw new InvalidOperationException("Failed to load scene settings, because Either the action type is not recognized, or the event is not a timeline dependent or a general one.");
            }
        }
        /// <summary>
        /// Creates a timeline independent event from the importer class
        /// Get event from the union of the timeline independent and general events.
        /// </summary>
        /// <param name="actionImport"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        private ISettingsEvent FetchTimelineIndependentEventFromImport(ActionImport actionImport)
        {
            switch (actionImport.Type)
            {
                case "NativeLoadSceneAction":
                    // Add the native load scene action to the timeline.
                    if (actionImport.SceneID.HasValue is false)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the scene id is null.");
                    }
                    var nativeSceneId = actionImport.SceneID.Value;
                    ISettingsEvent NativeLoadSceneAction = (ISettingsEvent)new NativeLoadSceneAction(Game, nativeSceneId);
                    return NativeLoadSceneAction;
                case "LoadSceneAction":
                    // Add the load scene action to the timeline.
                    if (actionImport.SceneID.HasValue is false)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the scene id is null.");
                    }
                    var sceneId = actionImport.SceneID.Value;
                    IEvent LoadSceneAction = new LoadSceneAction(Game, sceneId, actionImport.TriggerVariableName);
                    return (ISettingsEvent)LoadSceneAction;
                case "SetVariableValueAction":
                    // Add the set variable value action to the timeline.
                    if (actionImport.VariableName == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the variable name is null.");
                    }
                    ISettingsEvent SetVariableValueAction = new SetVariableValueAction(Game, actionImport.VariableName, this, actionImport.BlockComponentID);
                    return SetVariableValueAction;
                case "CreateMenuAction":
                    // Add the create menu action to the timeline.
                    if (actionImport.StaticMenu == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the static menu is null.");
                    }
                    var menu = FetchMenuFromImport(actionImport.StaticMenu);
                    return new CreateMenuAction(Game, menu, [.. menu.BlockList]);
                case "SwitchStaticMenuAction":
                    // Add the switch static menu action to the timeline.
                    if (actionImport.DisablingMenuID == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the disabling menu id is null.");
                    }
                    if (actionImport.EnablingMenuID == null)
                    {
                        throw new InvalidOperationException("Failed to load scene settings, because the enabling menu id is null.");
                    }
                    return new SwitchStaticMenuAction(Game, this, actionImport.DisablingMenuID.Value, actionImport.EnablingMenuID.Value);
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
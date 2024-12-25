using System.Text.Json;
using System.Text.Json.Serialization;
using EngineComponents.Actions;
using EngineComponents.Interfaces;
using Raylib_cs;

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
        [JsonPropertyName("Id")]
        public required long Id { get; set; } //remove setter!
        [JsonPropertyName("Background")]
        public required string Background { get; set; }
        [JsonPropertyName("Color")]
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
        [JsonPropertyName("SpritePath")]
        public string? SpritePath { get; set; }
        [JsonPropertyName("CharactersPerSecond")]
        public double? CharactersPerSecond { get; set; }
        [JsonPropertyName("Font")] //Need font importer
        public string Font { get; set; }
        [JsonPropertyName("TextBoxColor")]
        public int[]? TextBoxColor { get; set; }
        [JsonPropertyName("TextBoxBorder")]
        public int[]? TextBoxBorder { get; set; }
        [JsonPropertyName("PositionType")]
        public int? PositionType { get; set; }
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
        [JsonPropertyName("VariableValue")]
        public string? VariableValue { get; set; }
        [JsonPropertyName("VariableType")]
        public int? VariableType { get; set; }
        [JsonPropertyName("MenuXPosition")]
        public int? MenuXPosition { get; set; }
        [JsonPropertyName("MenuYPosition")]
        public int? MenuYPosition { get; set; }
        [JsonPropertyName("MenuWidth")]
        public int? MenuWidth { get; set; }
        [JsonPropertyName("MenuHeight")]
        public int? MenuHeight { get; set; }
        [JsonPropertyName("MenuFullScreen")]
        public string? MenuFullScreen { get; set; }
        [JsonPropertyName("MenuBlockList")]
        public BlockImport[]? MenuBlockList { get; set; }
        [JsonPropertyName("MenuColor")]
        public int[]? MenuColor { get; set; }
        [JsonPropertyName("MenuBorderColor")]
        public int[]? MenuBorderColor { get; set; }
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
        [JsonPropertyName("BlockXPosition")]
        public int BlockXPosition { get; set; }
        [JsonPropertyName("BlockYPosition")]
        public int BlockYPosition { get; set; }
        [JsonPropertyName("Button")]
        public ButtonComponentImport? Button { get; set; }

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
    /// The Game is a Gamemanager which changes the changes and loads the correct configuration and data into the template game.
    /// The Game is a singleton!
    /// The major difference between the editor and the Game, is that the latter updates everyframe.
    /// </summary>
    public class Game
    {
        /// <summary>
        /// The current folder path.
        /// </summary>
        const string currentFolderPath = "../../../src/Data/";
        /// <summary>
        /// The Game settings path.
        /// </summary>
        const string relativeGameSettingsPath = currentFolderPath + "GameSettings.json";
        /// <summary>
        /// The Scene path.
        /// </summary>
        const string relativeScenePath = currentFolderPath + "Scenes.json";
        /// <summary>
        /// The Variable path.
        /// </summary>
        const string relativeVariablePath = currentFolderPath + "Variables.json";
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
                            VariableList.Add(new Variable(variable.Name, variable.Value, VariableType.Bool));
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
                        for (int i = 0; i < scene.ActionList.Count(); i++)
                        {
                            switch (scene.ActionList[i].Type)
                            {
                                case "TextBoxCreateAction":
                                    // Add the textbox to the timeline.
                                    if (scene.ActionList[i].CharactersPerSecond.HasValue is false)
                                    {
                                        throw new InvalidOperationException("Failed to load scene settings, because the characters per second is null.");
                                    }
                                    var charactersPerSecond = scene.ActionList[i].CharactersPerSecond.Value;
                                    var font = new Font() { BaseSize = 30, GlyphPadding = 5 };
                                    var textboxcolor = scene.ActionList[i].TextBoxColor == null || scene.ActionList[i].TextBoxColor.Length < 4 ?
                                    new Color() { R = 0, G = 0, B = 0, A = 255 } :
                                    new Color()
                                    {
                                        R = (byte)scene.ActionList[i].TextBoxColor[0],
                                        G = (byte)scene.ActionList[i].TextBoxColor[1],
                                        B = (byte)scene.ActionList[i].TextBoxColor[2],
                                        A = (byte)scene.ActionList[i].TextBoxColor[3]
                                    };
                                    var textboxborder = scene.ActionList[i].TextBoxBorder == null || scene.ActionList[i].TextBoxBorder.Length < 4 ?
                                    new Color() { R = 0, G = 0, B = 0, A = 255 } :
                                    new Color()
                                    {
                                        R = (byte)scene.ActionList[i].TextBoxBorder[0],
                                        G = (byte)scene.ActionList[i].TextBoxBorder[1],
                                        B = (byte)scene.ActionList[i].TextBoxBorder[2],
                                        A = (byte)scene.ActionList[i].TextBoxBorder[3]
                                    };
                                    var positionType = (TextBox.PositionType)scene.ActionList[i].PositionType.Value;
                                    var wordWrap = scene.ActionList[i].WordWrap.Value;
                                    var textBoxTitle = scene.ActionList[i].TextBoxTitle;
                                    var textBoxContent = scene.ActionList[i].TextBoxContent;
                                    //
                                    var textBox = TextBox.CreateNewTextBox(
                                        this,
                                        charactersPerSecond,
                                        font,
                                        textboxcolor,
                                        textboxborder,
                                        positionType,
                                        wordWrap,
                                        textBoxTitle,
                                        [.. textBoxContent]);
                                    //
                                    timeline.ActionList.Add(new TextBoxCreateAction(textBox));
                                    break;
                                case "AddSpriteAction":
                                    // Add the sprite to the timeline.
                                    if (scene.ActionList[i].SpritePath == null)
                                    {
                                        throw new InvalidOperationException("Failed to load scene settings, because the sprite path is null.");
                                    }
                                    var spritePath = currentFolderPath + scene.ActionList[i].SpritePath;
                                    var sprite = new Sprite(spritePath);
                                    timeline.ActionList.Add(new AddSpriteAction(sprite, this));
                                    break;
                                case "TintSpriteAction":
                                    // Add the tint action to the timeline.
                                    var tintSprite = new Sprite(scene.ActionList[i].SpritePath);
                                    var tintColor = new Color()
                                    {
                                        R = (byte)scene.ActionList[i].TintColor[0],
                                        G = (byte)scene.ActionList[i].TintColor[1],
                                        B = (byte)scene.ActionList[i].TintColor[2],
                                        A = (byte)scene.ActionList[i].TintColor[3]
                                    };
                                    timeline.ActionList.Add(new TintSpriteAction(tintSprite, tintColor, this));
                                    break;
                                case "RemoveSpriteAction":
                                    // Add the remove action to the timeline.
                                    var removeSprite = new Sprite(currentFolderPath + scene.ActionList[i].SpritePath);
                                    timeline.ActionList.Add(new RemoveSpriteAction(removeSprite, this));
                                    break;
                                case "LoadSceneAction":
                                    // Add the load scene action to the timeline.
                                    if (scene.ActionList[i].SceneID.HasValue is false)
                                    {
                                        throw new InvalidOperationException("Failed to load scene settings, because the scene id is null.");
                                    }
                                    var sceneId = scene.ActionList[i].SceneID.Value;
                                    timeline.ActionList.Add(new LoadSceneAction(this, sceneId));
                                    break;
                                case "NativeLoadSceneAction":
                                    // Add the native load scene action to the timeline.
                                    if (scene.ActionList[i].SceneID.HasValue is false)
                                    {
                                        throw new InvalidOperationException("Failed to load scene settings, because the scene id is null.");
                                    }
                                    var nativeSceneId = scene.ActionList[i].SceneID.Value;
                                    timeline.ActionList.Add(new NativeLoadSceneAction(this, nativeSceneId));
                                    break;
                                case "CreateVariableAction":
                                    // Add the create variable action to the timeline.
                                    timeline.ActionList.Add(
                                        new CreateVariableAction(
                                            this,
                                            new Variable(
                                                scene.ActionList[i].VariableName,
                                                scene.ActionList[i].VariableValue,
                                                (VariableType)scene.ActionList[i].VariableType.Value)));
                                    break;
                                case "IncrementVariableAction":
                                    // Add the increment variable action to the timeline.
                                    timeline.ActionList.Add(
                                        new IncrementVariableAction(
                                            this,
                                            scene.ActionList[i].VariableName,
                                            int.Parse(scene.ActionList[i].VariableValue)));
                                    break;
                                case "DecrementVariableAction":
                                    // Add the decrement variable action to the timeline.
                                    timeline.ActionList.Add(
                                        new DecrementVariableAction(
                                            this,
                                            scene.ActionList[i].VariableName,
                                            int.Parse(scene.ActionList[i].VariableValue)));
                                    break;
                                case "SetVariableTrueAction":
                                    // Add the set variable true action to the timeline.
                                    timeline.ActionList.Add(
                                        new SetVariableTrueAction(
                                            this,
                                            scene.ActionList[i].VariableName));
                                    break;
                                case "SetVariableFalseAction":
                                    // Add the set variable false action to the timeline.
                                    timeline.ActionList.Add(
                                        new SetVariableFalseAction(
                                            this,
                                            scene.ActionList[i].VariableName));
                                    break;
                                case "SetBoolVariableAction":
                                    // Add the set variable action to the timeline.
                                    timeline.ActionList.Add(
                                        new SetBoolVariableAction(
                                            this,
                                            scene.ActionList[i].VariableName,
                                            bool.Parse(scene.ActionList[i].VariableValue)));
                                    break;
                                case "ToggleVariableAction":
                                    // Add the toggle variable action to the timeline.
                                    timeline.ActionList.Add(
                                        new ToggleVariableAction(
                                            this,
                                            scene.ActionList[i].VariableName));
                                    break;
                                case "CreateMenuAction":
                                    // Add the create menu action to the timeline.
                                    var menuXPosition = scene.ActionList[i].MenuXPosition.Value;
                                    var menuYPosition = scene.ActionList[i].MenuYPosition.Value;
                                    var menuWidth = scene.ActionList[i].MenuWidth.Value;
                                    var menuHeight = scene.ActionList[i].MenuHeight.Value;
                                    var menuFullScreen = scene.ActionList[i].MenuFullScreen == "True";
                                    var windowColor = new Color()
                                    {
                                        R = (byte)scene.ActionList[i].MenuColor[0],
                                        G = (byte)scene.ActionList[i].MenuColor[1],
                                        B = (byte)scene.ActionList[i].MenuColor[2],
                                        A = (byte)scene.ActionList[i].MenuColor[3]
                                    };
                                    var windowBorderColor = new Color()
                                    {
                                        R = (byte)scene.ActionList[i].MenuBorderColor[0],
                                        G = (byte)scene.ActionList[i].MenuBorderColor[1],
                                        B = (byte)scene.ActionList[i].MenuBorderColor[2],
                                        A = (byte)scene.ActionList[i].MenuBorderColor[3]
                                    };
                                    var menu = new Menu(
                                        this,
                                        menuXPosition,
                                        menuYPosition,
                                        menuWidth,
                                        menuHeight,
                                        menuFullScreen,
                                        [],
                                        windowColor,
                                        windowBorderColor);
                                    foreach (BlockImport? block in scene.ActionList[i].MenuBlockList)
                                    {
                                        //Futher implementation needed.
                                        IButtonEvent newEvent = block.Button.Event.Type switch
                                        {
                                            "TextBoxCreateAction" => new TextBoxCreateAction(
                                                TextBox.CreateNewTextBox(
                                                    this,
                                                    block.Button.Event.CharactersPerSecond.Value,
                                                    new Font() { BaseSize = 30, GlyphPadding = 5 },
                                                    new Color() { R = 0, G = 0, B = 0, A = 255 },
                                                    new Color() { R = 0, G = 0, B = 0, A = 255 },
                                                    (TextBox.PositionType)block.Button.Event.PositionType.Value,
                                                    block.Button.Event.WordWrap.Value,
                                                    block.Button.Event.TextBoxTitle,
                                                    [.. block.Button.Event.TextBoxContent])),
                                            "AddSpriteAction" => new AddSpriteAction(
                                                new Sprite(block.Button.Event.SpritePath),
                                                this),
                                            "TintSpriteAction" => new TintSpriteAction(
                                                new Sprite(block.Button.Event.SpritePath),
                                                new Color()
                                                {
                                                    R = (byte)block.Button.Event.TintColor[0],
                                                    G = (byte)block.Button.Event.TintColor[1],
                                                    B = (byte)block.Button.Event.TintColor[2],
                                                    A = (byte)block.Button.Event.TintColor[3]
                                                },
                                                this),
                                            "RemoveSpriteAction" => new RemoveSpriteAction(
                                                new Sprite(block.Button.Event.SpritePath),
                                                this),
                                            "NativeLoadSceneAction" => new NativeLoadSceneAction(
                                                this,
                                                block.Button.Event.SceneID.Value),
                                            "CreateVariableAction" => new CreateVariableAction(
                                                this,
                                                new Variable(
                                                    block.Button.Event.VariableName,
                                                    block.Button.Event.VariableValue,
                                                    (VariableType)block.Button.Event.VariableType.Value)),
                                            "IncrementVariableAction" => new IncrementVariableAction(
                                                this,
                                                block.Button.Event.VariableName,
                                                int.Parse(block.Button.Event.VariableValue)),
                                            "DecrementVariableAction" => new DecrementVariableAction(
                                                this,
                                                block.Button.Event.VariableName,
                                                int.Parse(block.Button.Event.VariableValue)),
                                            "SetVariableTrueAction" => new SetVariableTrueAction(
                                                this,
                                                block.Button.Event.VariableName),
                                            "SetVariableFalseAction" => new SetVariableFalseAction(
                                                this,
                                                block.Button.Event.VariableName),
                                            null => throw new InvalidOperationException("Failed to load scene settings, because the event type is not recognized."),
                                            _ => throw new NotImplementedException()
                                        };
                                        var newBlock = new Block(
                                            block.BlockXPosition,
                                            block.BlockYPosition,
                                            new Button(
                                                this,
                                                menu,
                                                block.Button.ButtonXPosition,
                                                block.Button.ButtonYPosition,
                                                block.Button.ButtonWidth,
                                                block.Button.ButtonHeight,
                                                block.Button.ButtonText,
                                                new Color()
                                                {
                                                    R = (byte)block.Button.ButtonColor[0],
                                                    G = (byte)block.Button.ButtonColor[1],
                                                    B = (byte)block.Button.ButtonColor[2],
                                                    A = (byte)block.Button.ButtonColor[3]
                                                },
                                                new Color()
                                                {
                                                    R = (byte)block.Button.ButtonBorderColor[0],
                                                    G = (byte)block.Button.ButtonBorderColor[1],
                                                    B = (byte)block.Button.ButtonBorderColor[2],
                                                    A = (byte)block.Button.ButtonBorderColor[3]
                                                },
                                                new Color()
                                                {
                                                    R = (byte)block.Button.ButtonHoverColor[0],
                                                    G = (byte)block.Button.ButtonHoverColor[1],
                                                    B = (byte)block.Button.ButtonHoverColor[2],
                                                    A = (byte)block.Button.ButtonHoverColor[3]
                                                },
                                                newEvent
                                            )
                                        );
                                        menu.BlockList.Add(newBlock);
                                    }
                                    timeline.ActionList.Add(new CreateMenuAction(this, menu, [.. menu.BlockList]));
                                    break;
                                default:
                                    throw new InvalidOperationException("Failed to load scene settings, because the action type is not recognized.");
                            }
                        }
                    Scenes.Add(new Scene(scene.Name, this)
                    {
                        Id = scene.Id,
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
        public static bool IsEscapeButtonPressed() => Raylib.IsKeyPressed(KeyboardKey.Escape);
    }
}
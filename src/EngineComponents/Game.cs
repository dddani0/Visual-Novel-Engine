using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using EngineComponents.Actions;
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
        [JsonPropertyName("ActionType")]
        public ActionImport[]? actions { get; set; }
    }
    /// <summary>
    /// The ActionImport class is a helper class to import the list of actions from a json file.
    /// </summary>
    internal class ActionImport
    {
        [JsonPropertyName("Type")]
        public string Type { get; set; }
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
    }

    public class Game
    {
        /// <summary>
        /// The Game is a Gamemanager which changes the changes and loads the correct configuration and data into the template game.
        /// The Game is a singleton!
        /// The major difference between the editor and the Game, is that the latter updates everyframe.
        /// </summary>
        const string currentFolderPath = "../../../src/";
        const string relativeGameSettingsPath = currentFolderPath + "GameSettings.json";
        const string relativeScenePath = currentFolderPath + "Scenes.json";
        internal GameImport gameSettings { get; private set; }
        public List<Scene> Scenes { get; set; }
        public Scene ActiveScene { get; private set; }
        public int sceneIndex;
        private int sceneCount;

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
            //Fetch scene settings
            SetupScenes();
        }

        /// <summary>
        /// Fetches the game settings from the json file.
        /// </summary>
        private void SetupGameWindow()
        {
            string rawFile = File.ReadAllText(relativeGameSettingsPath);
            var rawSettings = JsonSerializer.Deserialize<GameImport>(rawFile);
            if (rawSettings != null)
            {
                gameSettings = rawSettings;
            }
            else
            {
                throw new InvalidOperationException("Failed to load game settings, because the file is null.");
            }
            // Set the window title and size.
            Raylib.SetWindowTitle(gameSettings.Title);
            Raylib.SetWindowSize(gameSettings.WindowWidth, gameSettings.WindowHeigth);
        }

        /// <summary>
        /// Fetches the scene settings from the json file.
        /// </summary>
        private void SetupScenes()
        {
            // Initialize the list of scenes.
            Scenes = [];
            //
            string rawFile = File.ReadAllText(relativeScenePath);
            var rawScenes = JsonSerializer.Deserialize<List<SceneImport>>(rawFile);
            if (rawScenes != null)
            {
                //Need to parse the actions from the scenes.json file. But how?
                foreach (var scene in rawScenes)
                {
                    Timeline timeline = new();
                    for (int i = 0; i < scene.actions.Count(); i++)
                    {
                        switch (scene.actions[i].Type)
                        {
                            case "AddSpriteAction":
                                // Add the sprite to the timeline.
                                timeline.ActionList.Add(
                                    new AddSpriteAction(new Sprite(scene.actions[i].SpritePath), this));
                                break;
                            case "TextBoxCreateAction":
                                // Add the textbox to the timeline.
                                timeline.ActionList.Add(
                                    (IEvent)TextBox.CreateNewTextBox(
                                        this,
                                        scene.actions[i].CharactersPerSecond.Value,
                                        new Font() { BaseSize = 32, GlyphPadding = 5 },
                                        new Color(
                                            scene.actions[i].TextBoxColor[0],
                                            scene.actions[i].TextBoxColor[1],
                                            scene.actions[i].TextBoxColor[2],
                                            scene.actions[i].TextBoxColor[3]
                                        ),
                                        new Color(
                                            scene.actions[i].TextBoxBorder[0],
                                            scene.actions[i].TextBoxBorder[1],
                                            scene.actions[i].TextBoxBorder[2],
                                            scene.actions[i].TextBoxBorder[3]
                                        ),
                                        (TextBox.PositionType)scene.actions[i].PositionType.Value,
                                        scene.actions[i].WordWrap.Value,
                                        scene.actions[i].TextBoxTitle,
                                        scene.actions[i].TextBoxContent.ToList()));
                                break;
                            default:
                                throw new InvalidOperationException("Failed to load scene settings, because the action type is not recognized.");
                        }
                    }
                    Scenes.Add(new Scene(scene.Name, this)
                    {
                        Background = Enum.Parse<Scene.BackgroundOption>(scene.Background),
                        solidColor = scene.SolidColor.Length == 0 ? new() : new(
                                                                        r: scene.SolidColor[0],
                                                                        g: scene.SolidColor[1],
                                                                        b: scene.SolidColor[2],
                                                                        a: scene.SolidColor[3]
                                                                        ),
                        gradientColor = scene.GradientColor.Length == 0 ? [] : [new(
                                                                        r: scene.GradientColor[0],
                                                                        g: scene.GradientColor[1],
                                                                        b: scene.GradientColor[2],
                                                                        a: scene.GradientColor[3]
                                                                        ),
                                                                        new(
                                                                        r: scene.GradientColor[4],
                                                                        g: scene.GradientColor[5],
                                                                        b: scene.GradientColor[6],
                                                                        a: scene.GradientColor[7]
                                                                        ),
                                                                        new(
                                                                        r: scene.GradientColor[8],
                                                                        g: scene.GradientColor[9],
                                                                        b: scene.GradientColor[10],
                                                                        a: scene.GradientColor[11]
                                                                        ),
                                                                        new(
                                                                        r: scene.GradientColor[12],
                                                                        g: scene.GradientColor[13],
                                                                        b: scene.GradientColor[14],
                                                                        a: scene.GradientColor[15]
                                                                        )],
                        imageTexture = Raylib.LoadTexture(scene.ImageTexture),
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
        }

        /// <summary>
        /// Loads the scene.
        /// </summary>
        /// <param name="sceneIdx"></param>
        internal void LoadScene(int sceneIdx)
        {
            sceneIndex = sceneIdx;
            ActiveScene = Scenes[sceneIndex];
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
                case Scene.BackgroundOption.SolidColor:
                    Raylib.ClearBackground(ActiveScene.solidColor);
                    break;
                case Scene.BackgroundOption.GradientHorizontal:
                    Raylib.DrawRectangleGradientH(0, 0, Raylib.GetScreenWidth(), Raylib.GetScreenHeight(), ActiveScene.gradientColor[0], ActiveScene.gradientColor[1]);
                    break;
                case Scene.BackgroundOption.GradientVertical:
                    Raylib.DrawRectangleGradientV(0, 0, Raylib.GetScreenWidth(), Raylib.GetScreenHeight(), ActiveScene.gradientColor[0], ActiveScene.gradientColor[1]);
                    break;
                case Scene.BackgroundOption.Image:
                    Raylib.ClearBackground(Color.Black);
                    // Set the image to the screen size
                    ActiveScene.imageTexture.Width = Raylib.GetScreenWidth();
                    ActiveScene.imageTexture.Height = Raylib.GetScreenHeight();
                    //
                    Raylib.DrawTexture(ActiveScene.imageTexture,
                    Raylib.GetScreenWidth() / 2 - ActiveScene.imageTexture.Width / 2,
                    Raylib.GetScreenHeight() / 2 - ActiveScene.imageTexture.Height / 2,
                    Color.White);
                    break;
            }
            //
            ActiveScene.Timeline.RenderSprites();
            ActiveScene.Timeline.ExecuteAction();
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
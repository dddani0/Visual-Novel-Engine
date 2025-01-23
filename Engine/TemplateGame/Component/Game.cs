using System.Text.Json;
using System.Text.Json.Serialization;
using Engine.PortData;
using Raylib_cs;

namespace TemplateGame.Component
{
    /// <summary>
    /// The Game is the main class of the game.
    /// The Game class contains the path to the following files: gamesettings, scenes, variables
    /// </summary>
    public class Game
    {
        /// <summary>
        /// The GameLoader deals with the raw data which is loaded into the game.
        /// </summary>
        private GameImporter GameImport;
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
        internal GameExim GameSettings { get; private set; }
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
            var rawSettings = JsonSerializer.Deserialize<GameExim>(rawFile);
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
            var rawVariables = JsonSerializer.Deserialize<List<VariableExim>>(rawFile);
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
            GameImport = new(this);
            // Fetch the scene settings.
            string rawFile = File.ReadAllText(relativeScenePath);
            var rawScenes = JsonSerializer.Deserialize<List<SceneExim>>(rawFile);
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
                            timeline.ActionList.Add(GameImport.FetchTimelineDependentActionFromImport(scene.ActionList[i]));
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
using System.Text.Json;
using System.Text.Json.Serialization;
using Raylib_cs;
using VisualNovelEngine.Engine.PortData;

namespace VisualNovelEngine.Engine.Game.Component
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
        internal GameEximManager GameImport;
        /// <summary>
        /// The current folder path.
        /// </summary>
        internal string ProjectPath { get; set; }
        /// <summary>
        /// The Scene path.
        /// </summary>
        internal string BuildPath { get; set; }
        /// <summary>
        /// List of scenes.
        /// </summary>
        public List<Scene> Scenes { get; set; }
        /// <summary>
        /// List of variables.
        /// </summary>
        public List<Variable> Variables { get; set; }
        /// <summary>
        /// The active scene.
        /// </summary>
        public Scene ActiveScene { get; private set; }

        public Game(string projectPath)
        {
            ProjectPath = projectPath;
            BuildPath = ProjectPath;
            GameImport = new(this, BuildPath);
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
            Raylib.SetWindowSize(GameImport.GameExim.WindowWidth, GameImport.GameExim.WindowHeigth);
        }
        /// <summary>
        /// Fetches the saved variables from the json file.
        /// </summary>
        private void SetupVariables()
        {
            // Initialize the list of variables.
            Variables = [.. GameImport.GameExim.Variables.Select(GameImport.FetchVariableFromImport)];
        }
        /// <summary>
        /// Fetches the scene configuration from the json file.
        /// </summary>
        private void SetupScenes()
        {
            // Initialize the game loader.
            GameImport = new(this, BuildPath);
            // Initialize the list of scenes.
            Scenes = [.. GameImport.GameExim.Scenes.Select(GameImport.FetchSceneFromImport)];
            // Initialize the list of variables.
            if (Scenes.Count <= 0) return;
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
        public void Update()
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
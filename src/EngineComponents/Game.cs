using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using EngineComponents.Actions;
using Raylib_cs;

namespace EngineComponents
{
    internal class GameSettings
    {
        [JsonPropertyName("Title")]
        public string Title { get; set; }
        [JsonPropertyName("WindowWidth")]
        public int WindowWidth { get; set; }
        [JsonPropertyName("WindowHeight")]
        public int WindowHeigth { get; set; }
    }

    public class Game
    {
        /// <summary>
        /// The Game is a Gamemanager which changes the changes and loads the correct configuration and data into the template game.
        /// The Game is a singleton!
        /// The major difference between the editor and the Game, is that the latter updates everyframe.
        /// </summary>
        const string relativeGameSettingsPath = "../../../src/GameSettings.json";
        const string relativeScenePath = "../../../src/Scene.json";
        const string projectFolder = "";
        internal GameSettings gameSettings { get; private set; }
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
        public void SetupGameSettings()
        {
            //Fetch game settings.
            string rawFile = File.ReadAllText(relativeGameSettingsPath);
            var rawSettings = JsonSerializer.Deserialize<GameSettings>(rawFile);
            gameSettings = rawSettings;
            //Fetch scene settings
            //...
            //
            Raylib.SetWindowTitle(gameSettings.Title);
            //
            Raylib.SetWindowSize(gameSettings.WindowWidth, gameSettings.WindowHeigth);
            //for example
            var getImage = Raylib.LoadImage("../../../src/test.png");
            ActiveScene = new("DemoScene", this)
            {
                Background = Scene.BackgroundOption.Image,
                imageTexture = Raylib.LoadTextureFromImage(getImage)
            };
            Raylib.UnloadImage(getImage);
            //Raylib.UnloadTexture(ActiveScene.imageTexture);
            ActiveScene.AddActionsToTimeline([new TextBoxCreateAction(TextBox.CreateNewTextBox(ActiveScene.Timeline,
                40,
                new Font() { BaseSize = 32, GlyphPadding = 5 },
                0,
                0,
                250,
                500,
                false,
                ["Elég"])), new TextBoxCreateAction(
                TextBox.CreateNewTextBox(ActiveScene.Timeline,
                    40,
                    new Font() { BaseSize = 32, GlyphPadding = 5 },
                    0,
                    0,
                    500,
                    500,
                    false,
                    ["BBBAJA"]))]);
        }

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
                    Raylib.DrawTexture(ActiveScene.imageTexture, 0, 0, Color.White);
                    break;
            }
            //
            ActiveScene.Timeline.ExecuteAction();
        }
        //Inputs
        public static bool IsLeftMouseButtonPressed() => Raylib.IsMouseButtonPressed(MouseButton.Left);
        public static bool IsRightMouseButtonPressed() => Raylib.IsMouseButtonPressed(MouseButton.Right);
        public static bool IsEscapeButtonPressed() => Raylib.IsKeyPressed(KeyboardKey.Escape);
    }
}
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Raylib_cs;

namespace EngineComponents
{
    class GameSettings
    {
        [JsonPropertyName("Title")]
        public string Title { get; set; }
        [JsonPropertyName("WindowWidth")]
        public int WindowWidth { get; set; }
        [JsonPropertyName("WindowHeight")]
        public int WindowHeigth { get; set; }
    }

    class Game
    {
        /// <summary>
        /// The Game is a Gamemanager which changes the changes and loads the correct configuration and data into the template game.
        /// The Game is a singleton!
        /// The major difference between the editor and the Game, is that the latter updates everyframe.
        /// </summary>
        const string relativeGameSettingsPath = "../../../src/GameSettings.json";
        const string relativeScenePath = "../../../src/Scene.json";
        const string projectFolder = "";
        public GameSettings gameSettings { get; private set; }
        public List<Scene> Scenes { get; set; }
        public Scene ActiveScene { get; private set; }

        public Game()
        {
            SetupGameSettings();
        }

        public void SetupGameSettings()
        {
            //Read GameSettings
            string rawFile = File.ReadAllText(relativeGameSettingsPath);
            var rawSettings = JsonSerializer.Deserialize<GameSettings>(rawFile);
            gameSettings = rawSettings;
            //
            Raylib.SetWindowTitle(gameSettings.Title);
            //
            Raylib.SetWindowSize(gameSettings.WindowWidth, gameSettings.WindowHeigth);
            //for example
            var getImage = Raylib.LoadImage("../../../src/test.png");
            ActiveScene = new("DemoScene")
            {
                Background = Scene.BackgroundOption.Image,
                imageTexture = Raylib.LoadTextureFromImage(getImage)
            };
            Raylib.UnloadImage(getImage);
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
        }
        public void PlayGame()
        {

        }
    }
}
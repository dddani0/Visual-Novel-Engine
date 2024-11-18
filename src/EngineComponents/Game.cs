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
        //
        [JsonPropertyName("WindowWidth")]
        public int WindowWidth { get; set; }
        //
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

        public Game() => SetupGameSettings();

        /// <summary>
        /// Fetches all the correspondant json files, and loads them in the game.
        /// </summary>
        internal void SetupGameSettings()
        {
            //Fetch game settings.
            string rawFile = File.ReadAllText(relativeGameSettingsPath);
            var rawSettings = JsonSerializer.Deserialize<GameSettings>(rawFile);
            gameSettings = rawSettings;
            //Fetch scene settings

            //
            var getImage = Raylib.LoadImage("../../../src/test.png");
            Scenes = [new Scene("Menu", this){
                Background = Scene.BackgroundOption.SolidColor,
                solidColor = Color.DarkPurple
            }];
            //
            ActiveScene = Scenes[0];
            //
            Raylib.SetWindowTitle(gameSettings.Title);
            //
            Raylib.SetWindowSize(gameSettings.WindowWidth, gameSettings.WindowHeigth);
            ActiveScene.AddActionsToTimeline(new AddSpriteAction(new Sprite("../../../src/test.png"), this));
        }

        internal void LoadScene(int sceneIdx)
        {
            sceneIndex = sceneIdx;
            ActiveScene = Scenes[sceneIndex];
            ActiveScene.Timeline.StartTimeline();
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
                    Raylib.ClearBackground(Color.Black);
                    Raylib.DrawTexture(ActiveScene.imageTexture,
                    Raylib.GetScreenWidth() / 2 - ActiveScene.imageTexture.Width / 2,
                    Raylib.GetScreenHeight() / 2 - ActiveScene.imageTexture.Height / 2,
                    Color.White);
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
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
                Background = Scene.BackgroundOption.GradientVertical,
                gradientColor = [Color.Purple, Color.Blue],
            },new Scene("game", this){
                Background = Scene.BackgroundOption.GradientHorizontal,
                gradientColor = [Color.Red, Color.Brown]}];
            //
            ActiveScene = Scenes[0];
            //
            Raylib.SetWindowTitle(gameSettings.Title);
            //
            Raylib.SetWindowSize(gameSettings.WindowWidth, gameSettings.WindowHeigth);
            //Raylib.UnloadTexture(ActiveScene.imageTexture);
            Scenes[0].AddActionsToTimeline([new TextBoxCreateAction(TextBox.CreateNewTextBox(this,
                40,
                new Font() { BaseSize = 32, GlyphPadding = 5 },
                TextBox.PositionType.defaultPosition,
                false,
                ["Menü dialógus: első szegmens","Menü dialógus: második szegmens"])), new NativeLoadSceneAction(this, 1)]);
            Scenes[1].AddActionsToTimeline([new TextBoxCreateAction(TextBox.CreateNewTextBox(this,
                40,
                new Font() { BaseSize = 32, GlyphPadding = 5 },
                TextBox.PositionType.defaultPosition,
                false,
                ["Ingame dialógus: első szemgens", "Ingame dialógus: második szegmens, még mindig ugyanaz az opció"])), new TextBoxCreateAction(
                TextBox.CreateNewTextBox(this,
                    40,
                    new Font() { BaseSize = 32, GlyphPadding = 5 },
                    TextBox.PositionType.defaultPosition,
                    false,
                    ["Ingame dialógus: harmadik szemgens", "Ingame dialógus: negyedik szegmens"])), new NativeLoadSceneAction(this, 0)]);
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
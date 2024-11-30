using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using EngineComponents.Actions;
using Raylib_cs;

namespace EngineComponents
{
    /// <summary>
    /// The GameImport class is a helper class to import the game settings from a json file.
    /// </summary>
    internal class GameImport
    {
        [JsonPropertyName("Title")]
        public required string Title { get; set; }
        //
        [JsonPropertyName("WindowWidth")]
        public int WindowWidth { get; set; }
        //
        [JsonPropertyName("WindowHeight")]
        public int WindowHeigth { get; set; }
    }

    /// <summary>
    /// The SceneImport class is a helper class to import the scene settings from a json file.
    /// </summary>
    internal class SceneImport
    {
        [JsonPropertyName("Name")]
        public required string Name { get; set; }
        [JsonPropertyName("Id")]
        public required long Id { get; set; } //remove setter!
        //
        [JsonPropertyName("Background")]
        public Scene.BackgroundOption Background { get; set; }
        //
        [JsonPropertyName("SolidColor")]
        public Color? SolidColor { get; set; }
        //
        [JsonPropertyName("GradientColor")]
        public Color[]? GradientColor { get; set; }
        //
        [JsonPropertyName("ImageTexture")]
        public string? ImageTexture { get; set; }
        [JsonPropertyName("Timeline")]
        public required List<IEvent> Timeline { get; set; }
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
        internal GameImport gameSettings { get; private set; }
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
            var rawSettings = JsonSerializer.Deserialize<GameImport>(rawFile);
            gameSettings = rawSettings;
            //Fetch scene settings

            //
            var getImage = Raylib.LoadImage("../../../src/backdrop.png");
            Scenes = [new Scene("Menu", this){
                Background = Scene.BackgroundOption.Image,
                imageTexture = Raylib.LoadTextureFromImage(getImage)
            },new Scene("MasikScene",this) {
                Background = Scene.BackgroundOption.GradientHorizontal,
                gradientColor = [Color.Purple, Color.Blue]
            }];
            //
            ActiveScene = Scenes[0];
            //
            Raylib.SetWindowTitle(gameSettings.Title);
            //
            Raylib.SetWindowSize(gameSettings.WindowWidth, gameSettings.WindowHeigth);
            Sprite drhousesprite = new("../../../src/drhouse.png");
            Sprite replacementSprite = new("../../../src/empty.png");
            ActiveScene.AddActionsToTimeline([
                new TextBoxCreateAction(TextBox.CreateNewTextBox(this, 35, new Font(){ BaseSize = 32, GlyphPadding = 5}, TextBox.PositionType.defaultPosition, true, ["Ez egy üres szöveges doboz. Itt használatban van a wordwrap, ami nem vágja le a szavakat a közepénél."])),
                new AddSpriteAction(drhousesprite, this),
                new TextBoxCreateAction(TextBox.CreateNewTextBox(this, 15, new Font(){ BaseSize = 32, GlyphPadding = 5}, TextBox.PositionType.defaultPosition, true,"Narrátor", ["Sprite megjelenítés is működik."])),
                new TextBoxCreateAction(TextBox.CreateNewTextBox(this, 15, new Font(){ BaseSize = 32, GlyphPadding = 5}, TextBox.PositionType.defaultPosition, true,"Narrátor", ["Lehetek narrátor néven feltüntetve."])),
                new TextBoxCreateAction(TextBox.CreateNewTextBox(this, 15, new Font(){ BaseSize = 32, GlyphPadding = 5},TextBox.PositionType.defaultPosition, false, "Dr. House",["Ki kapcsoltam a wordwrap-et, ami esetében belevág a szó közepébe."])),
                new TintSpriteAction(drhousesprite, Color.SkyBlue, this),
                new TextBoxCreateAction(TextBox.CreateNewTextBox(this, 15, new Font(){ BaseSize = 32, GlyphPadding = 5},TextBox.PositionType.defaultPosition, true, "Dr. House",["Kék lettem."])),
                new ChangeSpriteAction(drhousesprite, "../../../src/test.png", this),
                new TextBoxCreateAction(TextBox.CreateNewTextBox(this, 15, new Font(){ BaseSize = 32, GlyphPadding = 5},TextBox.PositionType.defaultPosition, true, "Dr. House",["Sprite eltüntetés is létezik."])),
                new RemoveSpriteAction(drhousesprite, this),
                new TextBoxCreateAction(TextBox.CreateNewTextBox(this, 15, new Font(){ BaseSize = 32, GlyphPadding = 5},TextBox.PositionType.defaultPosition, true, "Eltünt karakter",["Valahogy így"])),
                new TextBoxCreateAction(TextBox.CreateNewTextBox(this, 50, new Font(){ BaseSize = 32, GlyphPadding = 5},Color.Yellow,Color.DarkBlue,TextBox.PositionType.defaultPosition, true, "",["Textboxnál nem csak a füle változtatható; van sebességváltoztatás. Színváltoztatás is lehetséges."])),
                new TextBoxCreateAction(TextBox.CreateNewTextBox(this, 15, new Font(){ BaseSize = 32, GlyphPadding = 5},TextBox.PositionType.defaultPosition, true, ["Betöltök egy másik scenet."])),
                new NativeLoadSceneAction(this,Scenes[1])]);
            Scenes[1].AddActionToTimeline(new TextBoxCreateAction(TextBox.CreateNewTextBox(this, 15, new Font() { BaseSize = 32, GlyphPadding = 5 }, Color.DarkBrown, Color.Orange, TextBox.PositionType.defaultPosition, true, ["Betöltöttem a másik scenet."])));
            Scenes[1].AddActionToTimeline(new TextBoxCreateAction(TextBox.CreateNewTextBox(this, 15, new Font() { BaseSize = 32, GlyphPadding = 5 }, Color.DarkBrown, Color.Orange, TextBox.PositionType.defaultPosition, true, ["Visszatöltöm a korábbi jelenetet, ami hatására úja indul az egész."])));
            Scenes[1].AddActionToTimeline(new NativeLoadSceneAction(this, Scenes[0]));
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
        //Inputs
        public static bool IsLeftMouseButtonPressed() => Raylib.IsMouseButtonPressed(MouseButton.Left);
        public static bool IsRightMouseButtonPressed() => Raylib.IsMouseButtonPressed(MouseButton.Right);
        public static bool IsEscapeButtonPressed() => Raylib.IsKeyPressed(KeyboardKey.Escape);
    }
}
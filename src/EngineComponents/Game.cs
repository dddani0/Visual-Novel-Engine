using System.Text.Json;
using Raylib_cs;

namespace EngineComponents
{
    class GameSettings
    {
        internal string Title { get; set; }
        internal int WindowWidth { get; set; }
        internal int WindowHeigth { get; set; }
    }

    class Game
    {
        /// <summary>
        /// The Game is a Gamemanager which changes the changes and loads the correct configuration and data into the template game.
        /// The Game is a singleton!
        /// The major difference between the editor and the Game, is that the latter updates everyframe.
        /// </summary>

        public GameSettings gameSettings { get; private set; }
        public List<Scene> Scenes { get; set; }
        public Scene ActiveScene { get; private set; }

        public Game()
        {
            SetupGameSettings();
            SetupActiveScene();
        }

        public void SetupGameSettings()
        {
            string rawFile = File.ReadAllText("../../../src/GameSettings.json"); //doesnt work
            gameSettings = JsonSerializer.Deserialize<GameSettings>(rawFile)!;
            //
            Raylib.SetWindowTitle(gameSettings.Title);
            //
            Raylib.SetWindowSize(gameSettings.WindowWidth, gameSettings.WindowHeigth);
        }

        public void SetupActiveScene()
        {
            //Raylib.ClearBackground(ActiveScene.)
        }

        public void UpdateScene()
        {

        }
        public void PlayGame()
        {

        }
    }
}
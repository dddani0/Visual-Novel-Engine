namespace EngineComponents
{
    class Scene
    {
        /// <summary>
        /// A level, which a player can create, modify and add depth to it.
        /// </summary>
        enum BackgroundOption
        {
            SolidColor,
            Gradient,
            Image,
            Gif
        }

        string Name { get; set; }
        Timeline Timeline { get; set; }
        BackgroundOption Background { get; set; }
        Game ConcurrentGame { get; }
        public Scene(string name)
        {
            Name = name;
            ConcurrentGame = null;
        }

        public Scene(string name, Game activeGame)
        {
            Name = name;
            ConcurrentGame = activeGame;
        }

        internal void LoadScene()
        {

        }

        internal void RestartScene()
        {

        }
    }
}
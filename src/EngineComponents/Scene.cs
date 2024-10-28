using Raylib_cs;

namespace EngineComponents
{
    class Scene
    {
        /// <summary>
        /// A level, which a player can create, modify and add depth to it.
        /// </summary>
        internal long Id { get; }
        internal enum BackgroundOption
        {
            SolidColor,
            GradientVertical, GradientHorizontal,
            Image
        }
        internal string Name { get; set; }
        internal Timeline Timeline { get; set; }
        internal BackgroundOption Background { get; set; } = BackgroundOption.SolidColor;
        internal Color solidColor = Color.Gray;
        internal Texture2D imageTexture;
        internal Color[] gradientColor;
        internal Game ConcurrentGame { get; }
        public Scene(string name)
        {
            Name = name;
        }

        internal void LoadScene()
        {
            //Timeline.
        }

        internal void RestartScene()
        {

        }
    }
}
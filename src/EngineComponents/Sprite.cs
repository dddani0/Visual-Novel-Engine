using Raylib_cs;

namespace EngineComponents
{
    /// <summary>
    /// A sprite that can be rendered on the screen.
    /// </summary>
    class Sprite : IPermanentRenderingObject
    {
        internal string Name { get; set; }
        internal Texture2D ImageTexture { get; set; }
        internal Color Color { get; set; }
        internal bool Enabled { get; set; } = false;
        internal int X { get; set; }
        internal int Y { get; set; }
        /// <summary>
        /// Creates a sprite.
        /// </summary>
        /// <param name="path">Path to the sprite image.</param>
        public Sprite(string path)
        {
            Name = path;
            ImageTexture = Raylib.LoadTexture(path);
            //default position
            Y = Raylib.GetScreenHeight() / 2 - ImageTexture.Width / 2;
            X = Raylib.GetScreenWidth() / 2 - ImageTexture.Height / 2;
            // default color
            Color = Color.White;
        }
        /// <summary>
        /// Changes the tint of the sprite.
        /// </summary>
        /// <param name="newColor">Sprites new color</param>
        public void ChangeTint(Color newColor) => Color = newColor;
        /// <summary>
        /// Aligns the sprites according to the screen.
        /// </summary>
        /// <param name="numberOfActiveSprite">The sprites added to the rendering list.</param>
        /// <param name="spriteIndex">Number of divident</param>
        public void AlignItems(int numberOfActiveSprite, int spriteIndex)
        {
            //Aligns the sprites according to the screen.
            //Show x sprites, but divide with x + 1.
            var spriteShowcaseNumber = Raylib.GetScreenWidth() / (numberOfActiveSprite + 2);
            X = spriteShowcaseNumber  * (spriteIndex + 1);
        }

        /// <summary>
        /// Renders the sprite on the screen.
        /// </summary>
        public void Render()
        {
            if (Enabled is false) return;
            Raylib.DrawTexture(ImageTexture, X, Y, Color);
        }
        bool IPermanentRenderingObject.Enabled() => Enabled;
    }
}
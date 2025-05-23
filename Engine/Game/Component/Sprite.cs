using System.Diagnostics;
using Raylib_cs;
using VisualNovelEngine.Engine.Game.Interface;

namespace VisualNovelEngine.Engine.Game.Component
{
    /// <summary>
    /// A sprite that can be rendered on the screen.
    /// </summary>
    public class Sprite : IRenderingObject
    {
        /// <summary>
        /// The name of the sprite.
        /// </summary>
        internal string Name { get; set; }
        /// <summary>
        /// The path to the sprite.
        /// </summary>
        internal string Path;
        /// <summary>
        /// Compatible path for the game.
        /// </summary>
        internal string GameCompatiblePath { get; set; }
        /// <summary>
        /// The texture of the sprite.
        /// </summary>
        internal Texture2D ImageTexture { get; set; }
        /// <summary>
        /// The color of the sprite.
        /// </summary>
        internal Color Color { get; set; }
        /// <summary>
        /// The state of the sprite.
        /// </summary>
        internal bool Enabled { get; set; } = true;
        /// <summary>
        /// Position of the sprite on the X axis.
        /// </summary>
        internal int X { get; set; }
        /// <summary>
        /// Position of the sprite on the Y axis.
        /// </summary>
        internal int Y { get; set; }
        /// <summary>
        /// The width of the sprite.
        /// </summary>
        internal Block Block { get; set; }
        /// <summary>
        /// Creates a sprite.
        /// </summary>
        /// <param name="path">Path to the sprite image.</param>
        public Sprite(string path)
        {
            Path = path.Replace("/", "\\");
            Name = Path.Split("\\").Last();
            GameCompatiblePath = @$"{Environment.CurrentDirectory}/../../../../../{Path[3..]}";
            ImageTexture = Raylib.LoadTexture(GameCompatiblePath);
            //default position
            Y = Raylib.GetScreenHeight() / 2 - ImageTexture.Width / 2;
            X = Raylib.GetScreenWidth() / 2 - ImageTexture.Height / 2;
            // default color
            Color = Color.White;
        }
        /// <summary>
        /// Creates a sprite with a parent block.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="block"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Sprite(string path, Block block, int x, int y)
        {
            Path = path.Replace("/", "\\");
            Name = Path.Split("\\").Last();
            ImageTexture = Raylib.LoadTexture(@$"{Environment.CurrentDirectory}/../../../../../{Path[3..]}");
            Block = block;
            X = Block.XPosition + x;
            Y = Block.YPosition + y;
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

        /// <summary>
        /// Changes the texture of the sprite.
        /// </summary>
        /// <param name="newTexture">The new texture of the sprite.</param>
        public void ChangeTexture(Texture2D newTexture)
        {
            //Raylib.UnloadTexture(ImageTexture);
            ImageTexture = newTexture;
        }
        public void AlignItems(int numberOfActiveSprite, int spriteIndex)
        {
            //Aligns the sprites according to the screen.
            //Show x sprites, but divide with x + 1.
            var spriteShowcaseNumber = Raylib.GetScreenWidth() / (numberOfActiveSprite + 2);
            X = spriteShowcaseNumber * (spriteIndex + 1);
        }
        /// <summary>
        /// Renders the sprite on the screen.
        /// </summary>
        public void Render()
        {
            if (Enabled is false) return;
            Raylib.DrawTexture(ImageTexture, X, Y, Color);
        }
        /// <summary>
        /// Checks if the sprite is enabled.
        /// </summary>
        /// <returns></returns>
        bool IRenderingObject.Enabled() => Enabled;
    }
}
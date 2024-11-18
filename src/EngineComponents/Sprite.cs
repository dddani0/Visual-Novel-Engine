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
        public Sprite(string path)
        {
            Name = path;
            ImageTexture = Raylib.LoadTexture(path);
            //default
            Y = Raylib.GetScreenHeight() / 2;
            X = Raylib.GetScreenWidth() / 2;
        }

        public void ChangeTint(Color newColor) => Color = newColor;
        public void AlignItems(List<Sprite> sprites, int numbering)
        {
            //Aligns the sprites on the screen.
            X = Raylib.GetScreenWidth() / sprites.Count * (numbering + 1);
        }

        public void Render()
        {
            if (Enabled is false) return;
            //
            Raylib.DrawTexture(ImageTexture, X, Y, Color);
        }

        bool IPermanentRenderingObject.Enabled() => Enabled;
    }
}
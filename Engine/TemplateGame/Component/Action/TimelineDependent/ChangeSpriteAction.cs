using Raylib_cs;
using TemplateGame.Interface;

namespace TemplateGame.Component.Action.TimelineDependent
{
    /// <summary>
    /// Changes the sprite texture.
    /// </summary>
    class ChangeSpriteAction : IAction
    {
        readonly Game Game;
        internal Sprite sprite;
        internal Texture2D replacementTexture;
        internal Sprite replacementSprite;
        /// <summary>
        /// Creates a new ChangeSpriteAction with existing texture.
        /// </summary>
        /// <param name="sprite">Selected sprite</param>
        /// <param name="texture">Replacement texture</param>
        /// <param name="game">Concurrent game</param>
        public ChangeSpriteAction(Sprite sprite, Texture2D texture, Game game)
        {
            Game = game;
            this.sprite = sprite;
            replacementTexture = texture;
        }
        /// <summary>
        /// Creates a new ChangeSpriteAction with path to the texture.
        /// </summary>
        /// <param name="sprite">Selected sprite</param>
        /// <param name="path">Path to the texture</param>
        /// <param name="game">Concurrent game</param>
        public ChangeSpriteAction(Sprite sprite, string path, Game game)
        {
            Game = game;
            this.sprite = sprite;
            replacementTexture = Raylib.LoadTexture(path);
        }
        /// <summary>
        /// Changes the sprite texture.
        /// </summary>
        public void PerformAction()
        {
            Game.ActiveScene.Timeline.SpriteRenderList.First(theSprite => theSprite.Equals(sprite)).ChangeTexture(replacementTexture);
            Game.ActiveScene.Timeline.NextStep();
        }
    }
}
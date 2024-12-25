using EngineComponents.Interfaces;

namespace EngineComponents.Actions
{
    /// <summary>
    /// Adds a sprite to the scene.
    /// </summary>
    class AddSpriteAction : IEvent
    {
        readonly Game Game;
        readonly Sprite sprite;
        /// <summary>
        /// Adds a sprite to the scene.
        /// </summary>
        /// <param name="sprite">Sprite to be added to the scene.</param>
        /// <param name="game">Active game.</param>
        public AddSpriteAction(Sprite sprite, Game game)
        {
            Game = game;
            this.sprite = sprite;
        }
        public void PerformEvent()
        {
            sprite.Enabled = true; //Enable sprite
            Game.ActiveScene.Timeline.SpriteRenderList.Add(sprite); //Add sprite to rendering list
            Game.ActiveScene.Timeline.SpriteRenderList.First(theSprite => theSprite.Equals(sprite)).ChangeTexture(sprite.ImageTexture);
            Game.ActiveScene.Timeline.NextStep(); //Move to next step
        }
    }
}
using EngineComponents.Interfaces;
using Raylib_cs;

namespace EngineComponents.Actions
{
    /// <summary>
    /// Tints a sprite with a color.
    /// </summary>
    class TintSpriteAction : IEvent, IButtonEvent
    {
        readonly Game Game;
        readonly Sprite sprite;
        readonly Color color;
        /// <summary>
        /// Tints a sprite with a color.
        /// </summary>
        /// <param name="sprite">The sprite which is to be tinted.</param>
        /// <param name="color">New color</param>
        /// <param name="game">concurrent game</param>
        public TintSpriteAction(Sprite sprite, Color color, Game game)
        {
            Game = game;
            this.sprite = sprite;
            this.color = color;
        }
        public void PerformEvent()
        {
            Game.ActiveScene.Timeline.SpriteRenderList.First(theSprite => theSprite.Equals(sprite)).ChangeTint(color);
            Game.ActiveScene.Timeline.NextStep();
        }
    }
}
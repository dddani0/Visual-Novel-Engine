using Raylib_cs;
using TemplateGame.Interface;

namespace TemplateGame.Component.Action.TimelineDependent
{
    /// <summary>
    /// Tints a sprite with a color.
    /// </summary>
    class TintSpriteAction : IEvent, IButtonEvent
    {
        private readonly Game Game;
        private readonly Sprite sprite;
        private readonly Color color;
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
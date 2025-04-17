using Raylib_cs;
using VisualNovelEngine.Engine.Game.Interface;

namespace VisualNovelEngine.Engine.Game.Component.Action.TimelineDependent
{
    /// <summary>
    /// Tints a sprite with a color.
    /// </summary>
    class TintSpriteAction : IAction, IButtonAction
    {
        private readonly Game Game;
        internal Sprite sprite;
        internal Color color;
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
        public void PerformAction()
        {
            Game.ActiveScene.Timeline.SpriteRenderList.First(theSprite => theSprite.Equals(sprite)).ChangeTint(color);
            Game.ActiveScene.Timeline.NextStep();
        }
    }
}
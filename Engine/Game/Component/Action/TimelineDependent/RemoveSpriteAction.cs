using VisualNovelEngine.Engine.Game.Interface;

namespace VisualNovelEngine.Engine.Game.Component.Action.TimelineDependent
{
    class RemoveSpriteAction : IAction, IButtonAction
    {
        readonly Game Game;
        internal Sprite sprite;
        public RemoveSpriteAction(Sprite sprite, Game game)
        {
            Game = game;
            this.sprite = sprite;
        }
        public void PerformAction()
        {
            Game.ActiveScene.Timeline.SpriteRenderList.Remove(sprite);
            sprite.Enabled = false;
            Game.ActiveScene.Timeline.NextStep();
        }
    }
}
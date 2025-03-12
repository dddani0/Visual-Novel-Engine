using TemplateGame.Interface;

namespace TemplateGame.Component.Action.TimelineDependent
{
    class RemoveSpriteAction : IAction, IButtonEvent
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
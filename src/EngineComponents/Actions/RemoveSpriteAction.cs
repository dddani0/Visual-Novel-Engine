using EngineComponents.Interfaces;

namespace EngineComponents.Actions
{
    class RemoveSpriteAction : IEvent
    {
        readonly Game Game;
        readonly Sprite sprite;
        public RemoveSpriteAction(Sprite sprite, Game game)
        {
            Game = game;
            this.sprite = sprite;
        }
        public void PerformEvent()
        {
            Game.ActiveScene.Timeline.SpriteRenderList.Remove(sprite);
            sprite.Enabled = false;
            Game.ActiveScene.Timeline.NextStep();
        }
    }
}
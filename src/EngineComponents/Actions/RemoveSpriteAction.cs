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
            Game.ActiveScene.Timeline.RenderList.Remove(sprite);
            sprite.Enabled = false;
            Game.ActiveScene.Timeline.NextStep();
        }
    }
}
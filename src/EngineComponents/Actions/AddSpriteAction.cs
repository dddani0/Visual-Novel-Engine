namespace EngineComponents.Actions
{
    /// <summary>
    /// Adds a sprite to the scene.
    /// </summary>
    class AddSpriteAction : IEvent
    {
        readonly Game Game;
        readonly Sprite sprite;
        public AddSpriteAction(Sprite sprite, Game game)
        {
            Game = game;
            this.sprite = sprite;
        }
        public void PerformEvent()
        {
            Game.ActiveScene.Timeline.RenderList.Add(sprite);
            sprite.Enabled = true;
            Game.ActiveScene.Timeline.NextStep();
        }
    }
}
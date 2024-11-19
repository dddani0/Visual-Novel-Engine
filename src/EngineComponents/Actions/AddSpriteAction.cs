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
            sprite.Enabled = true; //Enable sprite
            Game.ActiveScene.Timeline.RenderList.Add(sprite); //Add sprite to rendering list
            Game.ActiveScene.Timeline.NextStep(); //Move to next step
        }
    }
}
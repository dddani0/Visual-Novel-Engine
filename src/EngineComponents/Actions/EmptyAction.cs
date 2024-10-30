namespace EngineComponents.Actions
{
    /// <summary>
    /// Empty action.
    /// </summary>
    class EmptyAction : IEvent
    {
        readonly Game Game;
        public EmptyAction(Game game)
        {
            Game = game;
        }
        public void PerformEvent()
        {
            //Empty action
            Game.ActiveScene.Timeline.NextStep();
        }
    }
}
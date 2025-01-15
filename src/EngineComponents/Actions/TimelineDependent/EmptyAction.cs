using EngineComponents.Interfaces;

namespace EngineComponents.Actions.TimelineDependent
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
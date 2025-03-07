using TemplateGame.Interface;

namespace TemplateGame.Component.Action.TimelineDependent
{
    /// <summary>
    /// Empty action.
    /// </summary>
    class EmptyAction : IAction
    {
        readonly Game Game;
        public EmptyAction(Game game)
        {
            Game = game;
        }
        public void PerformAction()
        {
            //Empty action
            Game.ActiveScene.Timeline.NextStep();
        }
    }
}
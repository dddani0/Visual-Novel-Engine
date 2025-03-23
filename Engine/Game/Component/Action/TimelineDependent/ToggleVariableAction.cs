using VisualNovelEngine.Engine.Game.Interface;

namespace VisualNovelEngine.Engine.Game.Component.Action.TimelineDependent
{
    /// <summary>
    /// Sets the already declaired boolean variable to true.
    /// </summary>
    public class ToggleVariableAction : IAction
    {
        private readonly Game Game;
        internal string VariableName;
        public ToggleVariableAction(Game game, string variableName)
        {
            Game = game;
            VariableName = variableName;
        }

        public void PerformAction()
        {
            var variable = Game.VariableList.First(s => s.Name.Equals(VariableName)) ?? throw new System.Exception("Variable not found!");
            if (variable.Type == VariableType.Boolean)
            {
                variable.SetValue(!Boolean.Parse(variable.Value));
                Game.ActiveScene.Timeline.NextStep();
            }
            else
            {
                throw new System.Exception("Variable is not a boolean");
            }
        }
    }
}
using EngineComponents.Interfaces;

namespace EngineComponents.Actions
{
    /// <summary>
    /// Sets the already declaired boolean variable to true.
    /// </summary>
    public class ToggleVariableAction : IEvent
    {
        readonly string VariableName;
        Game Game { get; set; }
        public ToggleVariableAction(Game game, string variableName)
        {
            Game = game;
            VariableName = variableName;
        }

        public void PerformEvent()
        {
            var variable = Game.VariableList.First(s => s.Name.Equals(VariableName)) ?? throw new System.Exception("Variable not found!");
            if (variable.Type == VariableType.Bool)
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
using EngineComponents.Interfaces;

namespace EngineComponents.Actions.TimelineDependent
{
    /// <summary>
    /// Sets the already declaired boolean variable to true.
    /// </summary>
    public class SetVariableTrueAction : IEvent, IButtonEvent
    {
        readonly string VariableName;
        Game Game { get; set; }
        public SetVariableTrueAction(Game game, string variableName)
        {
            Game = game;
            VariableName = variableName;
        }

        public void PerformEvent()
        {
            var variable = Game.VariableList.First(s => s.Name.Equals(VariableName)) ?? throw new System.Exception("Variable not found!");
            if (variable.Type == VariableType.Boolean)
            {
                variable.SetValue(true);
                Game.ActiveScene.Timeline.NextStep();
            }
            else
            {
                throw new System.Exception("Variable is not a boolean");
            }
        }
    }
}
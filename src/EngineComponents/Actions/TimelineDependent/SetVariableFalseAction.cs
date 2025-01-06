using EngineComponents.Interfaces;

namespace EngineComponents.Actions.TimelineDependent
{
    /// <summary>
    /// Sets the already declaired boolean variable to true.
    /// </summary>
    public class SetVariableFalseAction : IEvent, IButtonEvent
    {
        readonly string VariableName;
        Game Game { get; set; }
        public SetVariableFalseAction(Game game, string variableName)
        {
            Game = game;
            VariableName = variableName;
        }

        public void PerformEvent()
        {
            var variable = Game.VariableList.FirstOrDefault(s => s.Name.Equals(VariableName)) ?? throw new System.Exception("Variable not found!");
            if (variable.Type == VariableType.Bool)
            {
                variable.SetValue(false);
                Game.ActiveScene.Timeline.NextStep();
            }
            else
            {
                throw new Exception("Variable is not a boolean");
            }
        }
    }
}
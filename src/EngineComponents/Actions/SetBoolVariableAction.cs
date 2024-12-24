using EngineComponents.Interfaces;

namespace EngineComponents.Actions
{
    /// <summary>
    /// Sets the already declaired boolean variable to true.
    /// </summary>
    public class SetBoolVariableAction : IEvent
    {
        readonly string VariableName;
        Game Game { get; set; }
        bool Value { get; set; }
        public SetBoolVariableAction(Game game, string variableName, bool value)
        {
            Game = game;
            Value = value;
            VariableName = variableName;
        }

        public void PerformEvent()
        {
            var variable = Game.VariableList.First(s => s.Name.Equals(VariableName)) ?? throw new System.Exception("Variable not found!");
            if (variable.Type == VariableType.Bool)
            {
                variable.SetValue(Value);
                Game.ActiveScene.Timeline.NextStep();
            }
            else
            {
                throw new System.Exception("Variable is not a boolean");
            }
        }
    }
}
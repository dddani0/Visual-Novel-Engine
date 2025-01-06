using EngineComponents.Interfaces;

namespace EngineComponents.Actions.TimelineDependent
{
    /// <summary>
    /// Decrements the already declaired integer variable with a value.
    /// </summary>
    public class DecrementVariableAction : IEvent, IButtonEvent
    {
        Variable Variable;
        string VariableName { get; }
        public int DecrementValue { get; set; }
        Game Game { get; set; }
        public DecrementVariableAction(Game game, string variableName, int decrementValue)
        {
            DecrementValue = decrementValue;
            Game = game;
            VariableName = variableName;
        }

        public void PerformEvent()
        {
            Variable = Game.VariableList.FirstOrDefault(s => s.Name.Equals(VariableName)) ?? throw new System.Exception("Variable not found!");
            if (Variable.Type == VariableType.Int)
            {
                var value = int.Parse(Variable.Value);
                value -= DecrementValue;
                Variable.SetValue(value);
                Game.ActiveScene.Timeline.NextStep();
            }
            else
            {
                throw new System.Exception("Variable is not an integer");
            }
        }
    }
}
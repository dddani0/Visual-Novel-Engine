namespace EngineComponents.Actions
{
    /// <summary>
    /// Increments the already declaired integer variable with a value.
    /// </summary>
    public class IncrementVariableAction : IEvent
    {
        Variable Variable;
        string VariableName { get; }
        public int IncrementValue { get; set; }
        Game Game { get; set; }
        public IncrementVariableAction(Game game, string variableName, int incrementValue)
        {
            IncrementValue = incrementValue;
            Game = game;
            VariableName = variableName;
        }

        public void PerformEvent()
        {
            Variable = Game.Variables.First(s => s.Name.Equals(VariableName)) ?? throw new System.Exception("Variable not found!");
            if (Variable.Type == VariableType.Int)
            {
                var value = int.Parse(Variable.Value);
                value += IncrementValue;
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
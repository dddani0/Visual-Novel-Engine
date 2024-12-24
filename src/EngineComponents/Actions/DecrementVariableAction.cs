namespace EngineComponents.Actions
{
    /// <summary>
    /// Decrements the already declaired integer variable with a value.
    /// </summary>
    public class DecrementVariableAction : IEvent
    {
        Variable Variable;
        string VariableName { get; }
        public int IncrementValue { get; set; }
        Game Game { get; set; }
        public DecrementVariableAction(Game game, string variableName, int incrementValue)
        {
            IncrementValue = incrementValue;
            Game = game;
            VariableName = variableName;
        }

        public void PerformEvent()
        {
            Variable = Game.ActiveScene.Timeline.VariableList.FirstOrDefault(s => s.Name.Equals(VariableName)) ?? throw new System.Exception("Variable not found!");
            if (Variable.Type == VariableType.Int)
            {
                var value = int.Parse(Variable.Value);
                value -= IncrementValue;
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
namespace EngineComponents.Actions
{
    /// <summary>
    /// Increments the already declaired integer variable with a value.
    /// </summary>
    public class IncrementVariableAction : IEvent
    {
        readonly Variable variable;
        public int IncrementValue { get; set; }
        Game Game { get; set; }
        public IncrementVariableAction(Game game, string variableName, int incrementValue)
        {
            IncrementValue = incrementValue;
            Game = game;
            variable = Game.Variables.First(s => s.Name.Equals(variableName)) ?? throw new System.Exception("Variable not found!");
        }

        public void PerformEvent()
        {
            if (variable.Type == VariableType.Int)
            {
                var value = int.Parse(variable.Value);
                value += IncrementValue;
                variable.SetValue(value);
                Game.ActiveScene.Timeline.NextStep();
            }
            else
            {
                throw new System.Exception("Variable is not an integer");
            }
        }
    }
}
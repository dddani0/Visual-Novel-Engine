namespace EngineComponents.Actions
{
    /// <summary>
    /// Sets the already declaired boolean variable to true.
    /// </summary>
    public class SetBoolVariableAction : IEvent
    {
        readonly Variable variable;
        Game Game { get; set; }
        bool Value { get; set; }
        public SetBoolVariableAction(Game game, string variableName, bool value)
        {
            Game = game;
            variable = Game.Variables.First(s => s.Name.Equals(variableName)) ?? throw new System.Exception("Variable not found!");
            Value = value;
        }

        public void PerformEvent()
        {
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
namespace EngineComponents.Actions
{
    /// <summary>
    /// Sets the already declaired boolean variable to true.
    /// </summary>
    public class SetVariableTrueAction : IEvent
    {
        readonly Variable variable;
        Game Game { get; set; }
        public SetVariableTrueAction(Game game, string variableName)
        {
            Game = game;
            variable = Game.Variables.First(s => s.Name.Equals(variableName)) ?? throw new System.Exception("Variable not found!");
        }

        public void PerformEvent()
        {
            if (variable.Type == VariableType.Bool)
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
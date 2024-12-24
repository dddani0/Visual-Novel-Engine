namespace EngineComponents
{
    /// <summary>
    /// Creates a new variable.
    /// </summary>
    /// <param name="concurrentGame">Active game</param>
    /// <param name="variable">To be cerated variable</param>
    class CreateVariableAction(Game concurrentGame, Variable variable) : IEvent
    {
        Variable Variable { get; } = variable;
        Game Game { get; } = concurrentGame;

        public void PerformEvent()
        {
            var variable = Game.ActiveScene.Timeline.VariableList.FirstOrDefault(s => s.Name.Equals(Variable.Name));
            if (variable != null)
            {
                throw new System.Exception("Variable with the declaired name already exists.");
            }
            Game.ActiveScene.Timeline.VariableList.Add(Variable);
            Game.ActiveScene.Timeline.NextStep();
        }
    }
}
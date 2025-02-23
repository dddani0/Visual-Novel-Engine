

using TemplateGame.Interface;

namespace TemplateGame.Component.Action.TimelineDependent
{
    /// <summary>
    /// Creates a new variable.
    /// </summary>
    /// <param name="game">Active game</param>
    /// <param name="variable">To be cerated variable</param>
    class CreateVariableAction(Game game, Variable variable) : IEvent, IButtonEvent
    {
        Variable Variable { get; } = variable;
        Game Game { get; } = game;

        public void PerformEvent()
        {
            var variable = Game.VariableList.FirstOrDefault(s => s.Name.Equals(Variable.Name));
            if (variable != null)
            {
                throw new System.Exception("Variable with the declaired name already exists.");
            }
            Game.VariableList.Add(Variable);
            Game.ActiveScene.Timeline.NextStep();
            //Write to file.
        }
    }
}
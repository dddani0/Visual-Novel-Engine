using VisualNovelEngine.Engine.Game.Interface;

namespace VisualNovelEngine.Engine.Game.Component.Action.TimelineDependent
{
    /// <summary>
    /// Sets the already declaired boolean variable to true.
    /// </summary>
    public class SetVariableFalseAction : IAction, IButtonAction
    {
        internal string VariableName;
        Game Game { get; set; }
        public SetVariableFalseAction(Game game, string variableName)
        {
            Game = game;
            VariableName = variableName;
        }

        public void PerformAction()
        {
            var variable = Game.Variables.FirstOrDefault(s => s.Name.Equals(VariableName)) ?? throw new System.Exception("Variable not found!");
            if (variable.Type == VariableType.Boolean)
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
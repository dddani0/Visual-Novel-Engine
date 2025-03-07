using TemplateGame.Interface;

namespace TemplateGame.Component.Action.TimelineDependent
{
    /// <summary>
    /// Sets the already declaired boolean variable to true.
    /// </summary>
    public class SetBoolVariableAction : IAction, IButtonEvent
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

        public void PerformAction()
        {
            var variable = Game.VariableList.First(s => s.Name.Equals(VariableName)) ?? throw new System.Exception("Variable not found!");
            if (variable.Type == VariableType.Boolean)
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
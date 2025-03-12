using TemplateGame.Interface;

namespace TemplateGame.Component.Action.TimelineDependent
{
    /// <summary>
    /// Increments the already declaired integer or float variable with a constant value or variable value.
    /// </summary>
    public class IncrementVariableAction : IAction, IButtonEvent
    {
        private Variable Variable { get; set; }
        private Variable IncrementVariable { get; set; }
        internal string VariableName { get; set; }
        string IncrementVariableName { get; }
        internal int IncrementIntegerValue { get; set; }
        private float IncrementFloatValue { get; set; }
        private bool IsIntegerIncrement { get; set; }
        Game Game { get; set; }
        /// <summary>
        /// Constructor for incrementing the variable with a constant integer value.
        /// </summary>
        /// <param name="game">Active game</param>
        /// <param name="variableName">The variable which is to be increased</param>
        /// <param name="incrementValue">The incrementing value of the parameter</param>
        public IncrementVariableAction(Game game, string variableName, string incrementVariableName)
        {
            Game = game;
            IncrementVariableName = incrementVariableName;
            VariableName = variableName;
        }
        /// <summary>
        /// Constructor for incrementing the variable with a constant float value.
        /// </summary>
        /// <param name="game">Active game</param>
        /// <param name="variableName">The variable which is to be increased</param>
        /// <param name="incrementValue">The incrementing value of the parameter</param>
        /// <summary>
        /// Increments the variable with the constant value or with a variable value.
        /// </summary>
        /// <exception cref="System.Exception"></exception>
        public void PerformAction()
        {
            Variable = Game.VariableList.FirstOrDefault(s => s.Name.Equals(VariableName)) ?? throw new System.Exception("Variable not found!");
            IncrementVariable = Game.VariableList.FirstOrDefault(s => s.Name.Equals(IncrementVariableName)) ?? throw new System.Exception("Variable not found!");
            if (IncrementVariable.Type == VariableType.Int)
            {
                IsIntegerIncrement = true;
                IncrementIntegerValue = int.Parse(IncrementVariable.Value);
            }
            else if (IncrementVariable.Type == VariableType.Float)
            {
                IsIntegerIncrement = false;
                IncrementFloatValue = float.Parse(IncrementVariable.Value);
            }
            else
            {
                throw new System.Exception("Variable is not an integer or float");
            }
            var value = IsIntegerIncrement ? int.Parse(Variable.Value) : float.Parse(Variable.Value);
            value += IsIntegerIncrement ? IncrementIntegerValue : IncrementFloatValue;
            Variable.SetValue(value);
            Game.ActiveScene.Timeline.NextStep();
        }
    }
}
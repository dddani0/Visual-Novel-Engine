using EngineComponents.Interfaces;

namespace EngineComponents.Actions.TimelineDependent
{
    /// <summary>
    /// Decrements the already declaired integer variable with a value.
    /// </summary>
    public class DecrementVariableAction : IEvent, IButtonEvent
    {
        private Variable Variable { get; set; }
        private Variable DecrementVariable { get; set; }
        private string VariableName { get; }
        private string DecrementVariableName { get; }
        public int DecrementIntegerValue { get; set; }
        private float DecrementFloatValue { get; set; }
        private bool IsIntegerIncrement { get; set; }
        Game Game { get; set; }
        /// <summary>
        /// Constructor for decrementing the variable with a constant integer value.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="variableName"></param>
        /// <param name="decrementVariableName"></param>
        public DecrementVariableAction(Game game, string variableName, string decrementVariableName)
        {
            Game = game;
            DecrementVariableName = decrementVariableName;
            VariableName = variableName;
        }
        /// <summary>
        /// Decrements the variable with the constant value or with a variable value.
        /// </summary>
        /// <exception cref="System.Exception"></exception>
        public void PerformEvent()
        {
            Variable = Game.VariableList.FirstOrDefault(s => s.Name.Equals(VariableName)) ?? throw new System.Exception("Variable not found!");
            DecrementVariable = Game.VariableList.FirstOrDefault(s => s.Name.Equals(DecrementVariableName)) ?? throw new System.Exception("Variable not found!");
            if (DecrementVariable.Type == VariableType.Int)
            {
                IsIntegerIncrement = true;
                DecrementIntegerValue = int.Parse(DecrementVariable.Value);
            }
            else if (DecrementVariable.Type == VariableType.Float)
            {
                IsIntegerIncrement = false;
                DecrementFloatValue = float.Parse(DecrementVariable.Value);
            }
            else
            {
                throw new System.Exception("Variable is not an integer or float");
            }

            var value = IsIntegerIncrement ? int.Parse(Variable.Value) : float.Parse(Variable.Value);
            value += IsIntegerIncrement ? DecrementIntegerValue : DecrementFloatValue;
            Variable.SetValue(value);
            Game.ActiveScene.Timeline.NextStep();
        }
    }
}
using EngineComponents.Interfaces;

namespace EngineComponents.Actions.TimelineIndependent
{
    /// <summary>
    /// Update a value of an existing variable with a renderable components value.
    /// Slider: Int, Float
    /// Toggle: Boolean
    /// </summary>
    public class SetVariableValueAction : IEvent
    {
        private Variable Variable { get; set; }
        private string VariableName { get; }
        private string StringValue { get; set; }
        private Slider SliderComponent { get; set; }
        private Toggle ToggleComponent { get; set; }
        private Game Game { get; set; }
        /// <summary>
        /// Constructor for setting the variable with a constant integer value.
        /// </summary>
        /// <param name="game">Active game.</param>
        /// <param name="variableName">Updating variable name.</param>
        /// <param name="component">Rendering component.</param>
        public SetVariableValueAction(Game game, string variableName, IPermanentRenderingObject component)
        {
            Game = game;
            VariableName = variableName;
            switch (component)
            {
                case Slider slider:
                    SliderComponent = slider;
                    break;
                case Toggle toggle:
                    ToggleComponent = toggle;
                    break;
            }
        }
        /// <summary>
        /// Constructor for setting the variable with a constant string value.
        /// Example use case: inputfield
        /// </summary>
        /// <param name="game"></param>
        /// <param name="variableName"></param>
        /// <param name="value"></param>
        public SetVariableValueAction(Game game, string variableName, String value)
        {
            Game = game;
            VariableName = variableName;
            StringValue = value;
        }

        /// <summary>
        /// Sets the variable with the constant value or with a variable value.
        /// </summary>
        /// <exception cref="System.Exception"></exception>
        public void PerformEvent()
        {
            Variable = Game.VariableList.FirstOrDefault(s => s.Name.Equals(VariableName)) ?? throw new System.Exception("Variable not found!");
            if (SliderComponent != null)
            {
                if (Variable.Type == VariableType.Int || Variable.Type == VariableType.Float)
                {
                    Variable.SetValue(SliderComponent.GetSliderValue().ToString());
                }
                else
                {
                    throw new System.Exception("Variable is not an integer or float");
                }

            }
            else if (ToggleComponent != null)
            {
                if (Variable.Type == VariableType.Boolean)
                {
                    Variable.SetValue(ToggleComponent.IsToggled.ToString());
                }
                else
                {
                    throw new System.Exception("Variable is not a Boolean");
                }
            }
            else if (StringValue != null)
            {
                Variable.SetValue(StringValue);
            }
            else
            {
                throw new System.Exception("Component is not a slider or a toggle!");
            }
        }
    }
}
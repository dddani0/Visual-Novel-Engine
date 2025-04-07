using VisualNovelEngine.Engine.Game.Interface;

namespace VisualNovelEngine.Engine.Game.Component.Action.TimelineIndependent
{
    /// <summary>
    /// Update a value of an existing variable with a renderable components value.
    /// Slider: Int, Float
    /// Toggle: Boolean
    /// </summary>
    public class SetVariableValueAction : IAction, ISettingsEvent
    {
        private readonly Game Game;
        private GameImporter GameLoader { get; set; }
        internal int ComponentID { get; set; }
        private Variable Variable;
        internal string VariableName;
        internal Slider SliderComponent;
        internal Toggle ToggleComponent;
        internal InputField InputField;
        private bool IsComponentFound = false;
        /// <summary>
        /// Constructor for setting the variable with a constant integer value.
        /// </summary>
        /// <param name="game">Active game.</param>
        /// <param name="variableName">Updating variable name.</param>
        /// <param name="component">Rendering component.</param>
        public SetVariableValueAction(Game game, string variableName, GameImporter gameLoader, int componentID)
        {
            Game = game;
            VariableName = variableName;
            GameLoader = gameLoader;
            ComponentID = componentID;
        }

        /// <summary>
        /// Sets the variable with the constant value or with a variable value.
        /// </summary>
        /// <exception cref="System.Exception"></exception>
        public void PerformAction()
        {
            Variable = Game.Variables.FirstOrDefault(s => s.Name.Equals(VariableName)) ?? throw new System.Exception("Variable not found!");
            if (IsComponentFound is false)
            {
                foreach (var block in GameLoader.BlockListCache)
                {
                    if (block.ID == ComponentID && IsComponentFound is false)
                    {
                        if (block.Component is Slider slider)
                        {
                            SliderComponent = slider;
                            IsComponentFound = true;
                        }
                        else if (block.Component is Toggle toggle)
                        {
                            ToggleComponent = toggle;
                            IsComponentFound = true;
                        }
                        else if (block.Component is InputField inputField)
                        {
                            InputField = inputField;
                            IsComponentFound = true;
                        }
                        else
                        {
                            throw new System.Exception("Component is not a slider or a toggle!");
                        }
                    }
                }
            }
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
            else if (InputField != null)
            {
                if (Variable.Type == VariableType.String)
                {
                    Variable.SetValue(InputField.Text);
                }
                else
                {
                    throw new System.Exception("Variable is not a String");
                }
            }
            else
            {
                throw new System.Exception("Component is not a slider or a toggle!");
            }
        }
    }
}
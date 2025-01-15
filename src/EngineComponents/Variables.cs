namespace EngineComponents
{
    public enum VariableType
    {
        String,
        Int,
        Float,
        Boolean
    }
    /// <summary>
    /// Represents a variable that can be used in the game.
    /// </summary>
    public class Variable
    {
        /// <summary>
        /// The name of the variable.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The value of the variable.
        /// Allowed types: String, Integer, Float, Boolean.
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// The type of the variable.
        /// </summary>
        public VariableType Type { get; set; }
        /// <summary>
        /// Creates a variable.
        /// </summary>
        /// <param name="name">Name of the variable</param>
        /// <param name="value">Value of the varaible</param>
        /// <param name="type">Type of the variable.</param>
        public Variable(string name, string value, VariableType type)
        {
            Name = name;
            Value = value;
            Type = type;
        }
        /// <summary>
        /// Set the value of the string variable
        /// </summary>
        /// <param name="value">String variable's new value</param>
        public void SetValue(string value) => Value = value;
        /// <summary>
        /// Set the value of the integer variable
        /// </summary>
        /// <param name="value">Integer variable's new value</param>
        public void SetValue(int value) => Value = value.ToString();
        /// <summary>
        /// Set the value of the float variable
        /// </summary>
        /// <param name="value">Float variable's new value</param>
        public void SetValue(float value) => Value = value.ToString();
        /// <summary>
        /// Set the value of the boolean variable
        /// </summary>
        /// <param name="value">Boolean variable's new value</param>
        public void SetValue(bool value) => Value = value.ToString();
    }
}
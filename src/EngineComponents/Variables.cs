namespace EngineComponents
{
    public enum VariableType
    {
        String,
        Int,
        Float,
        Bool
    }
    /// <summary>
    /// A class to store variables
    /// </summary>
    public class Variable
    {
        public string Name { get; set; }
        public string Value { get; set; }
        VariableType Type { get; set; }
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
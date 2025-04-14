namespace VisualNovelEngine.Engine.Tests.Game.Component
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Raylib_cs;
    using VisualNovelEngine.Engine.Component;
    using VisualNovelEngine.Engine.Editor.Component.Command;
    using VisualNovelEngine.Engine.Editor.Interface;
    using VisualNovelEngine.Engine.Game.Component;

    [TestClass]
    public class GameVariableTest
    {
        /// <summary>
        /// Test method to check if the game variable is created correctly.
        /// </summary>
        [TestMethod]
        public void VariableCreationTest()
        {
            string name = "TestVariable";
            string value = "TestValue";
            Variable gameVariable = new(name, value, VariableType.String);

            // Assert
            Assert.AreEqual(name, gameVariable.Name);
            Assert.AreEqual(value, gameVariable.Value);
            Assert.AreEqual(VariableType.String, gameVariable.Type);
        }
        [TestMethod]
        public void VariableSetValueTest()
        {
            string name = "TestVariable";
            string value = "TestValue";
            Variable gameVariable = new(name, value, VariableType.String);
            string newValue = "NewValue";
            gameVariable.SetValue(newValue);
            Assert.AreEqual(newValue, gameVariable.Value);
        }
    }
}
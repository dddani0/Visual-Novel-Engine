namespace VisualNovelEngine.Engine.Tests.Engine.Component
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Raylib_cs;
    using VisualNovelEngine.Engine.Component;
    using VisualNovelEngine.Engine.Editor.Component.Command;
    using VisualNovelEngine.Engine.Editor.Interface;

    [TestClass]
    public class EngineTest
    {
        // Test method to check if the engine is created correctly
        [TestMethod]
        public void EngineCreationTest()
        {
            // Arrange
            Engine engine = new();

            // Act & Assert
            Assert.IsNotNull(engine);
        }

        [TestMethod]
        public void EngineDefaultStateTest()
        {
            // Arrange
            Engine engine = new();

            EngineState expectedState = EngineState.Default;

            // Act & Assert
            Assert.AreEqual(expectedState, engine.State);
        }
    }
}
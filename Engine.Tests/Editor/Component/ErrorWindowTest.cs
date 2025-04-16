using EngineEditor.Component;
using VisualNovelEngine.Engine.Editor.Component.Command;
using VisualNovelEngine.Engine.Editor.Interface;

namespace VisualNovelEngine.Engine.Tests.Editor.Component
{
    [TestClass]
    public class ErrorWindowTest
    {
        /// <summary>
        /// Test the creation of an ErrorWindow object.
        /// </summary>
        [TestMethod]
        public void ErrorWindowCreationTest()
        {
            VisualNovelEngine.Engine.Component.Engine engine = new();
            VisualNovelEngine.Engine.Editor.Component.Editor editor = engine.CreateEditor("Test", @"../../../Engine/Data/PlaceholderEditor.json");
            string message = "Error message/Warning message!";
            int width = 200;
            int height = 50;
            ICommand command = new EmptyCommand();
            ErrorWindow errorWindow = new(editor, message, [], width, height);

            Assert.IsNotNull(errorWindow);
            Assert.AreEqual(width, errorWindow.Width);
            Assert.AreEqual(height, errorWindow.Height);
        }
    }
}
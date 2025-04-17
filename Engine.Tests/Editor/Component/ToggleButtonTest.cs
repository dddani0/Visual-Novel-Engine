using VisualNovelEngine.Engine.Editor.Component;
using VisualNovelEngine.Engine.Editor.Component.Command;
using VisualNovelEngine.Engine.Editor.Interface;

namespace VisualNovelEngine.Engine.Tests.Editor.Component
{
    [TestClass]
    public class ToggleButtonTest
    {
        /// <summary>
        /// Test the creation of a ToggleButton object.
        /// </summary>
        [TestMethod]
        public void ToggleButtonCreationTest()
        {
            VisualNovelEngine.Engine.Component.Engine engine = new();
            VisualNovelEngine.Engine.Editor.Component.Editor editor = engine.CreateEditor("Test", @"../../../Engine/Data/PlaceholderEditor.json");
            int x = 0;
            int y = 0;
            string text = "Test";
            int size = 100;
            int borderWidth = 2;
            ICommand command = new EmptyCommand();
            ToggleButton toggleButton = new(editor, x, y, size, borderWidth, text, false);
            Assert.IsNotNull(toggleButton);
            Assert.AreEqual(x, toggleButton.XPosition);
            Assert.AreEqual(y, toggleButton.YPosition);
            Assert.AreEqual(size, toggleButton.Size);
            Assert.AreEqual(borderWidth, toggleButton.BorderWidth);
            Assert.AreEqual(text, toggleButton.Text);
            Assert.IsFalse(toggleButton.IsToggled);
        }
    }
}
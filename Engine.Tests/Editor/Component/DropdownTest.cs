using VisualNovelEngine.Engine.Editor.Component;
using VisualNovelEngine.Engine.Editor.Component.Command;
using VisualNovelEngine.Engine.Editor.Interface;

namespace VisualNovelEngine.Engine.Tests.Editor.Component
{
    [TestClass]
    public class DropdownTest
    {
        /// <summary>
        /// Test the creation of a Dropdown object.
        /// </summary>
        [TestMethod]
        public void DropdownCreationTest()
        {
            VisualNovelEngine.Engine.Component.Engine engine = new();
            VisualNovelEngine.Engine.Editor.Component.Editor editor = engine.CreateEditor("Test", @"../../../Engine/Data/PlaceholderEditor.json");
            int x = 100;
            int y = 100;
            string text = "Select Option";
            int width = 200;
            int height = 50;
            int borderWidth = 2;
            ICommand command = new EmptyCommand();
            Dropdown dropdown = new(editor, x, y, width, height, borderWidth, Dropdown.FilterType.None);

            Assert.IsNotNull(dropdown);
            Assert.AreEqual(x, dropdown.XPosition);
            Assert.AreEqual(y, dropdown.YPosition);
            Assert.AreEqual(width, dropdown.Width);
            Assert.AreEqual(height, dropdown.Height);
            Assert.AreEqual(borderWidth, dropdown.BorderWidth);
            Assert.AreEqual(Dropdown.FilterType.None, dropdown.Filter);
        }
    }
}
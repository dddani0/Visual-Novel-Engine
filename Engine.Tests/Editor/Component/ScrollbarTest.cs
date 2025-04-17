using VisualNovelEngine.Engine.Editor.Component;
using VisualNovelEngine.Engine.Editor.Component.Command;
using VisualNovelEngine.Engine.Editor.Interface;

namespace VisualNovelEngine.Engine.Tests.Editor.Component
{
    [TestClass]
    public class ScrollbarTest
    {
        /// <summary>
        /// Test the creation of a Scrollbar object.
        /// </summary>
        [TestMethod]
        public void ScrollbarCreationTest()
        {
            VisualNovelEngine.Engine.Component.Engine engine = new();
            VisualNovelEngine.Engine.Editor.Component.Editor editor = engine.CreateEditor("Test", @"../../../Engine/Data/PlaceholderEditor.json");
            int x = 0;
            int y = 0;
            int width = 200;
            int height = 100;
            ICommand command = new EmptyCommand();
            Scrollbar scrollbar = new(editor, x, y, height, width, Scrollbar.ScrollbarType.Vertical, false, []);

            Assert.IsNotNull(scrollbar);
            Assert.AreEqual(x, scrollbar.XPosition);
            Assert.AreEqual(y, scrollbar.YPosition);
            Assert.AreEqual(width, scrollbar.Width);
            Assert.AreEqual(height, scrollbar.Height);
        }
    }
}
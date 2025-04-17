using VisualNovelEngine.Engine.Editor.Component;

namespace VisualNovelEngine.Engine.Tests.Editor.Component
{
    [TestClass]
    public class LabelTest
    {
        /// <summary>
        /// Test the creation of a Label object.
        /// </summary>
        [TestMethod]
        public void LabelCreationTest()
        {
            string text = "Test Label";
            int x = 100;
            int y = 100;
            Label label = new(x, y, text);
            Assert.IsNotNull(label);
            Assert.AreEqual(x, label.XPosition);
            Assert.AreEqual(y, label.YPosition);
            Assert.AreEqual(text, label.Text);
        }
        /// <summary>
        /// Test the creation of a Label object with a specific font.
        /// </summary>
        [TestMethod]
        public void LabelSetTextTest()
        {
            string text = "Test Label";
            int x = 100;
            int y = 100;
            Label label = new(x, y, text);
            string newText = "New Text";
            label.Text = newText;
            Assert.AreEqual(newText, label.Text);
        }
        /// <summary>
        /// Test the creation of a Label object with a specific font.
        /// </summary>
        [TestMethod]
        public void LabelSetPositionTest()
        {
            string text = "Test Label";
            int x = 100;
            int y = 100;
            Label label = new(x, y, text);
            int newX = 200;
            int newY = 200;
            label.XPosition = newX;
            label.YPosition = newY;
            Assert.AreEqual(newX, label.XPosition);
            Assert.AreEqual(newY, label.YPosition);
        }
    }
}
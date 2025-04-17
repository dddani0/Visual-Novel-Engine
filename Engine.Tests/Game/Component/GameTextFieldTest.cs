using Raylib_cs;
using VisualNovelEngine.Engine.Game.Component;

namespace VisualNovelEngine.Engine.Tests.Game.Component
{
    /// <summary>
    /// Test the GameTextField class.
    /// </summary>
    [TestClass]
    public class GameTextFieldTest
    {
        /// <summary>
        /// Test the creation of a GameTextField object.
        /// </summary>
        [TestMethod]
        public void GameTextFieldCreationTest()
        {
            int x = 0;
            int y = 0;
            int id = 0;
            Block block = new(x, y, null, id);
            int width = 100;
            int height = 50;
            int borderWidth = 2;
            int horizontalTextMargin = 5;
            int verticalTextMargin = 5;
            string text = "Test TextField";
            bool wordWrap = true;
            bool visible = true;
            Color textColor = Color.Black;
            Color borderColor = Color.Beige;
            TextField textField = new(block, x, y, width, height, borderWidth, horizontalTextMargin, verticalTextMargin, text, Raylib.GetFontDefault(), wordWrap, visible, textColor, borderColor);
            Assert.IsNotNull(textField);
            Assert.AreEqual(x, textField.XPosition);
            Assert.AreEqual(y, textField.YPosition);
            Assert.AreEqual(width, textField.Width);
            Assert.AreEqual(height, textField.Height);
            Assert.AreEqual(borderWidth, textField.BorderWidth);
            Assert.AreEqual(horizontalTextMargin, textField.HorizontalTextMargin);
            Assert.AreEqual(verticalTextMargin, textField.VerticalTextMargin);
            Assert.AreEqual(text, textField.Text);
            Assert.AreEqual(wordWrap, textField.WordWrap);
        }
    }
}
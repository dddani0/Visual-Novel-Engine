using Raylib_cs;
using VisualNovelEngine.Engine.Game.Component;

namespace VisualNovelEngine.Engine.Tests.Game.Component
{
    /// <summary>
    /// Test the GameTextBox class.
    /// </summary>
    [TestClass]
    public class TextBoxTest
    {
        /// <summary>
        /// Test the creation of a GameTextBox object.
        /// </summary>
        [TestMethod]
        public void TextBoxCreationTest()
        {
            Color color = Color.Black;
            Color borderColor = Color.Gray;
            string title = "Test TextBox";
            double cps = 1.0;
            VisualNovelEngine.Engine.Component.Engine engine = new();
            VisualNovelEngine.Engine.Game.Component.Game game = engine.CreateGame(@"../../../Engine/Data/PlaceholderGameBuild.json");
            TextBox textBox = TextBox.CreateNewTextBox(game, cps, Raylib.GetFontDefault(), color, borderColor, TextBox.PositionType.defaultPosition, 5, 5, false, title, []);
            Assert.IsNotNull(textBox);
            Assert.AreEqual(color, textBox.Color);
            Assert.AreEqual(borderColor, textBox.BorderColor);
            Assert.AreEqual(TextBox.PositionType.defaultPosition, textBox.TextBoxPositionType);
            Assert.AreEqual(cps, textBox.CPSTextSpeed);
            Assert.AreEqual(title, textBox.Title);
        }
    }
}
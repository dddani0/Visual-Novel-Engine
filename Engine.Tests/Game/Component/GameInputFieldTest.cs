using Raylib_cs;
using VisualNovelEngine.Engine.Game.Component;
using VisualNovelEngine.Engine.Game.Component.Action;
using VisualNovelEngine.Engine.Game.Interface;

namespace VisualNovelEngine.Engine.Tests.Game.Component
{
    /// <summary>
    /// Test the GameInputField class.
    /// </summary>
    [TestClass]
    public class GameInputFieldTest
    {
        /// <summary>
        /// Test the creation of a GameInputField object.
        /// </summary>
        [TestMethod]
        public void GameInputFieldCreationTest()
        {
            int x = 0;
            int y = 0;
            int width = 100;
            int height = 100;
            int id = 0;
            string text = "Test Input Field";
            string buttonText = "Submit";
            int ButtonYOffset = 0;
            Block block = new(x, y, null, id);
            VisualNovelEngine.Engine.Component.Engine engine = new();
            VisualNovelEngine.Engine.Game.Component.Game game = engine.CreateGame(@"../../../Engine/Data/PlaceholderGameBuild.json");
            IButtonAction action = new NativeLoadSceneAction(game, 0);
            InputField inputField = new(game, block, x, y, ButtonYOffset, width, height, text, buttonText, Color.Black, Color.Gray, Color.DarkGray, Color.Gray, (IButtonAction)action);
            Assert.IsNotNull(inputField);
            Assert.AreEqual(x, inputField.XPosition);
            Assert.AreEqual(y, inputField.YPosition);
            Assert.AreEqual(width, inputField.Width);
            Assert.AreEqual(height, inputField.Height);
            Assert.AreEqual(text, inputField.Text);
        }
    }
}
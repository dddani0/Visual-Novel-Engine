using Raylib_cs;
using VisualNovelEngine.Engine.Game.Component;
using VisualNovelEngine.Engine.Game.Component.Action;
using VisualNovelEngine.Engine.Game.Interface;

namespace VisualNovelEngine.Engine.Tests.Game.Component
{
    /// <summary>
    /// Test the GameButton class.
    /// </summary>
    [TestClass]
    public class GameButtonTest
    {
        /// <summary>
        /// Test the creation of a GameButton object.
        /// </summary>
        [TestMethod]
        public void GameButtonCreationTest()
        {
            int x = 0;
            int y = 0;
            int width = 100;
            int height = 100;
            int id = 0;
            string text = "Test Button";
            int borderWidth = 2;
            Block block = new(x, y, null, id);
            VisualNovelEngine.Engine.Component.Engine engine = new();
            VisualNovelEngine.Engine.Game.Component.Game game = engine.CreateGame(@"../../../Engine/Data/PlaceholderGameBuild.json");
            IButtonAction action = new NativeLoadSceneAction(game, 0);
            Button button = new(game, block, Raylib.GetFontDefault(), x, y, borderWidth, width, height, text, Color.Black, Color.Gray, Color.Blue, Color.Beige, action);
            Assert.IsNotNull(button);
            Assert.AreEqual(x, button.XPosition);
            Assert.AreEqual(y, button.YPosition);
            Assert.AreEqual(width, button.Width);
            Assert.AreEqual(height, button.Height);
            Assert.AreEqual(text, button.Text);
        }
    }
}
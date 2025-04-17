using Raylib_cs;
using VisualNovelEngine.Engine.Game.Component;

namespace VisualNovelEngine.Engine.Tests.Game.Component
{
    /// <summary>
    /// Test the Menu class.
    /// </summary>
    [TestClass]
    public class MenuTest
    {
        /// <summary>
        /// Test the creation of a Menu object.
        /// </summary>
        [TestMethod]
        public void MenuCreationTest()
        {
            int x = 0;
            int y = 0;
            int width = 100;
            int height = 100;
            int id = 0;
            VisualNovelEngine.Engine.Component.Engine engine = new();
            VisualNovelEngine.Engine.Game.Component.Game game = engine.CreateGame(@"../../../Engine/Data/PlaceholderGameBuild.json");
            Color color = Color.Black;
            Color borderColor = Color.Gray;
            bool fullscreen = false;
            Menu menu = new(game, id, x, y, width, height, fullscreen, [], color, borderColor);
            Assert.IsNotNull(menu);
            Assert.AreEqual(width, menu.Width);
            Assert.AreEqual(height, menu.Height);
        }
    }
}
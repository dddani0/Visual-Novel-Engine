using VisualNovelEngine.Engine.Game.Component;

namespace VisualNovelEngine.Engine.Tests.Game.Component
{
    /// <summary>
    /// Test the GameSprite class.
    /// </summary>
    [TestClass]
    public class GameSpriteTest
    {
        /// <summary>
        /// Test the creation of a GameSprite object.
        /// </summary>
        [TestMethod]
        public void GameSpriteCreationTest()
        {
            string imagePath = "test.png";
            Sprite sprite = new(imagePath);
            Assert.IsNotNull(sprite);
            Assert.AreEqual(imagePath, sprite.Path);
        }
    }
}
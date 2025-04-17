using VisualNovelEngine.Engine.Game.Component;

namespace VisualNovelEngine.Engine.Tests.Game.Component
{
    /// <summary>
    /// Test the Block class.
    /// </summary>
    [TestClass]
    public class BlockTest
    {
        /// <summary>
        /// Test the creation of a Block object.
        /// </summary>
        [TestMethod]
        public void BlockCreationTest()
        {
            int x = 0;
            int y = 0;
            int id = 0;
            Sprite sprite = new("test.png");
            Block block = new(x, y, sprite, id);
            Assert.IsNotNull(block);
            Assert.AreEqual(x, block.XPosition);
            Assert.AreEqual(y, block.YPosition);
            Assert.AreEqual(sprite, block.Component);
        }
    }
}
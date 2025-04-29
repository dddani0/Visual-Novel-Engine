using Raylib_cs;
using VisualNovelEngine.Engine.Game.Component;

namespace VisualNovelEngine.Engine.Tests.Game.Component
{
    /// <summary>
    /// Test the DropBox class.
    /// </summary>
    [TestClass]
    public class DropBoxTest
    {
        /// <summary>
        /// Test the creation of a DropBox object.
        /// </summary>
        [TestMethod]
        public void DropBoxCreationTest()
        {
            int x = 0;
            int y = 0;
            int width = 100;
            int height = 100;
            int id = 0;
            Block block = new(x, y, null, id);
            Dropbox dropBox = new(block, x, y, width, height, [], Color.Black, Color.Gray, Color.Blue);
            Assert.IsNotNull(dropBox);
            Assert.AreEqual(x, dropBox.XPosition);
            Assert.AreEqual(y, dropBox.YPosition);
            Assert.AreEqual(width, dropBox.Width);
            Assert.AreEqual(height, dropBox.Height);
        }
    }
}
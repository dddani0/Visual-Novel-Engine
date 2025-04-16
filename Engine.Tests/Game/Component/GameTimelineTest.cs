using VisualNovelEngine.Engine.Game.Component;

namespace VisualNovelEngine.Engine.Tests.Game.Component
{
    /// <summary>
    /// Test the GameTimeline class.
    /// </summary>
    [TestClass]
    public class GameTimelineTest
    {
        /// <summary>
        /// Test the creation of a GameTimeline object.
        /// </summary>
        [TestMethod]
        public void GameTimelineCreationTest()
        {
            Timeline timeline = new();
            Assert.IsNotNull(timeline);
            Assert.AreEqual(0, timeline.ActionList.Count);
        }
    }
}
namespace VisualNovelEngine.Engine.Tests.Game.Component
{
    [TestClass]
    public class TimerTest
    {
        /// <summary>
        /// Test the creation of a Timer object.
        /// </summary>
        [TestMethod]
        public void TimerCreationTest()
        {
            VisualNovelEngine.Engine.Game.Component.Timer timer = new(1f);
            Assert.IsNotNull(timer);
        }
    }
}
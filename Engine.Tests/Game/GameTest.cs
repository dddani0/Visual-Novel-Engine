namespace VisualNovelEngine.Engine.Tests.Game
{
    [TestClass]
    public class GameTest
    {
        /// <summary>
        /// Test the creation of a Game object.
        /// </summary>
        [TestMethod]
        public void GameCreationTest()
        {
            VisualNovelEngine.Engine.Component.Engine engine = new();
            VisualNovelEngine.Engine.Game.Component.Game game = engine.CreateGame(@"../../../Engine/Data/PlaceholderGameBuild.json");
            Assert.IsNotNull(game);
        }
    }
}
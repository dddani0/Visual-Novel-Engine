using VisualNovelEngine.Engine.Game.Component;

namespace VisualNovelEngine.Engine.Tests.Game.Component
{
    /// <summary>
    /// Test the GameScene class.
    /// </summary>
    [TestClass]
    public class GameSceneTest
    {
        /// <summary>
        /// Test the creation of a GameScene object.
        /// </summary>
        [TestMethod]
        public void GameSceneCreationTest()
        {
            int x = 0;
            int y = 0;
            int id = 0;
            Block block = new(x, y, null, id);
            VisualNovelEngine.Engine.Component.Engine engine = new();
            VisualNovelEngine.Engine.Game.Component.Game game = engine.CreateGame(@"../../../Engine/Data/PlaceholderGameBuild.json");
            string name = "Test Scene";
            Scene scene = new(name, game);
            Assert.IsNotNull(scene);
            Assert.AreEqual(name, scene.Name);
            Assert.AreEqual(game, scene.Game);
        }
    }
}
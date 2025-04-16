using Raylib_cs;
using VisualNovelEngine.Engine.Game.Component;
using VisualNovelEngine.Engine.Game.Component.Action;
using VisualNovelEngine.Engine.Game.Interface;

namespace VisualNovelEngine.Engine.Tests.Game.Component
{
    /// <summary>
    /// Test the GameSlider class.
    /// </summary>
    [TestClass]
    public class GameSliderTest
    {
        /// <summary>
        /// Test the creation of a GameSlider object.
        /// </summary>
        [TestMethod]
        public void GameSliderCreationTest()
        {
            int x = 0;
            int y = 0;
            int width = 100;
            int height = 100;
            int id = 0;
            Block block = new(x, y, null, id);
            VisualNovelEngine.Engine.Component.Engine engine = new();
            VisualNovelEngine.Engine.Game.Component.Game game = engine.CreateGame(@"../../../Engine/Data/PlaceholderGameBuild.json");
            int borderWidth = 2;
            int sliderDragRadius = 5;
            ISettingsAction action = new NativeLoadSceneAction(game, 0);
            Slider slider = new(block, x, y, width, height, borderWidth, sliderDragRadius, Color.Black, Color.Gray, Color.Beige, action);
            Assert.IsNotNull(slider);
            Assert.AreEqual(x, slider.XPosition);
            Assert.AreEqual(y, slider.YPosition);
            Assert.AreEqual(width, slider.Width);
            Assert.AreEqual(height, slider.Height);
        }
    }
}
using Raylib_cs;
using VisualNovelEngine.Engine.Game.Component;
using VisualNovelEngine.Engine.Game.Component.Action;
using VisualNovelEngine.Engine.Game.Interface;

namespace VisualNovelEngine.Engine.Tests.Game.Component
{
    /// <summary>
    /// Test the GameToggle class.
    /// </summary>
    [TestClass]
    public class GameToggleTest
    {
        /// <summary>
        /// Test the creation of a GameToggle object.
        /// </summary>
        [TestMethod]
        public void GameToggleCreationTest()
        {
            int x = 0;
            int y = 0;
            int id = 0;
            Block block = new(x, y, null, id);
            int size = 50;
            int textXOffset = 10;
            string text = "Test Toggle";
            bool isLocked = false;
            Color color = Color.Black;
            Color borderColor = Color.Gray;
            Color toggledColor = Color.Blue;
            VisualNovelEngine.Engine.Component.Engine engine = new();
            VisualNovelEngine.Engine.Game.Component.Game game = engine.CreateGame(@"../../../Engine/Data/PlaceholderGameBuild.json");
            ISettingsAction settingsAction = new NativeLoadSceneAction(game, 0);
            Toggle toggle = new(block, x, y, size, textXOffset, text, isLocked, color, borderColor, toggledColor, settingsAction);
            Assert.IsNotNull(toggle);
            Assert.AreEqual(x, toggle.XPosition);
            Assert.AreEqual(y, toggle.YPosition);
            Assert.AreEqual(size, toggle.BoxSize);
            Assert.AreEqual(textXOffset, toggle.TextXOffset);
            Assert.AreEqual(text, toggle.Text);
            Assert.AreEqual(isLocked, toggle.IsLocked);
            Assert.AreEqual(color, toggle.Color);
            Assert.AreEqual(borderColor, toggle.BorderColor);
            Assert.AreEqual(toggledColor, toggle.ToggledColor);
            Assert.AreEqual((IAction)settingsAction, toggle.SettingsAction);
        }
    }
}
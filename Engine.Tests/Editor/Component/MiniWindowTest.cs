using Raylib_cs;
using VisualNovelEngine.Engine.Editor.Component;
using VisualNovelEngine.Engine.Editor.Component.Command;
using VisualNovelEngine.Engine.Editor.Interface;

namespace VisualNovelEngine.Engine.Tests.Editor.Component
{
    [TestClass]
    public class MiniWindowTest
    {
        /// <summary>
        /// Test the creation of a MiniWindow object.
        /// </summary>
        [TestMethod]
        public void MiniWindowCreationTest()
        {
            VisualNovelEngine.Engine.Component.Engine engine = new();
            VisualNovelEngine.Engine.Editor.Component.Editor editor = engine.CreateEditor("Test", @"../../../Engine/Data/PlaceholderEditor.json");
            int x = 0;
            int y = 0;
            int width = 200;
            int height = 100;
            int borderWidth = 2;
            Color color = Color.Black;
            Color borderColor = Color.Gray;
            bool hasVariableComponent = false;
            bool hasSceneComponent = false;
            bool closeButton = true;
            ICommand command = new EmptyCommand();
            MiniWindow miniWindow = new(editor, closeButton, hasVariableComponent, hasSceneComponent, x, y, width, height, borderWidth, color, borderColor, MiniWindowType.Vertical, []);

            Assert.IsNotNull(miniWindow);
            Assert.AreEqual(x, miniWindow.XPosition);
            Assert.AreEqual(y, miniWindow.YPosition);
            Assert.AreEqual(width, miniWindow.Width);
            Assert.AreEqual(height, miniWindow.Height);
            Assert.AreEqual(borderWidth, miniWindow.BorderWidth);
        }
    }
}
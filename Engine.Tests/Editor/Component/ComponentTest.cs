using Raylib_cs;
using VisualNovelEngine.Engine.Editor.Component;
using VisualNovelEngine.Engine.Editor.Component.Command;
using VisualNovelEngine.Engine.Editor.Interface;
using VisualNovelEngine.Engine.Game.Component;

namespace VisualNovelEngine.Engine.Tests.Engine.Component
{
    [TestClass]
    public class ComponentTest
    {
        /// <summary>
        /// Test the creation of a component.
        /// </summary>
        [TestMethod]
        public void ComponentCreationTest()
        {
            VisualNovelEngine.Engine.Component.Engine engine = new();
            VisualNovelEngine.Engine.Editor.Component.Editor editor = engine.CreateEditor("Test", @"../../../Engine/Data/PlaceholderEditor.json");
            int id = 1;
            int x = 100;
            int y = 100;
            string text = "Test Component";
            int width = 200;
            int height = 50;
            int borderWidth = 2;
            Color color = Color.White;
            Color borderColor = Color.Black;
            Color selectedColor = Color.Red;
            Color hoverColor = Color.Gray;
            Sprite sprite = new("test");
            VisualNovelEngine.Engine.Editor.Component.Component component = new(id, editor, null, text, x, y, width, height, borderWidth, color, borderColor, selectedColor, hoverColor, sprite);

            Assert.AreEqual(x, component.XPosition);
            Assert.AreEqual(y, component.YPosition);
            Assert.AreEqual(text, component.Name);
            Assert.AreEqual(width, component.Width);
            Assert.AreEqual(height, component.Height);
            Assert.AreEqual(borderWidth, component.BorderWidth);
            Assert.AreEqual(color, component.Color);
            Assert.AreEqual(borderColor, component.BorderColor);
            Assert.AreEqual(selectedColor, component.SelectedColor);
            Assert.AreEqual(hoverColor, component.HoverColor);
            Assert.AreEqual(sprite, component.RenderingObject);
        }
    }
}
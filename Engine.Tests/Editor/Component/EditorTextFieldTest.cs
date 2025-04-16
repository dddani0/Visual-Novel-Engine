using Raylib_cs;
using VisualNovelEngine.Engine.Editor.Component;
using VisualNovelEngine.Engine.Editor.Component.Command;
using VisualNovelEngine.Engine.Editor.Interface;

namespace VisualNovelEngine.Engine.Tests.Editor.Component
{
    [TestClass]
    public class EditorTextFieldTest
    {
        /// <summary>
        /// Test the creation of a TextField object.
        /// </summary>
        [TestMethod]
        public void EditorTextFieldCreationTest()
        {
            VisualNovelEngine.Engine.Component.Engine engine = new();
            VisualNovelEngine.Engine.Editor.Component.Editor editor = engine.CreateEditor("Test", @"../../../Engine/Data/PlaceholderEditor.json");
            int x = 100;
            int y = 100;
            string text = "Enter Text";
            int width = 200;
            int height = 50;
            int borderWidth = 2;
            ICommand command = new EmptyCommand();
            TextField textField = new(editor, x, y, width, height, borderWidth, text, Raylib.GetFontDefault(), true);

            Assert.IsNotNull(textField);
            Assert.AreEqual(x, textField.XPosition);
            Assert.AreEqual(y, textField.YPosition);
            Assert.AreEqual(width, textField.Width);
            Assert.AreEqual(height, textField.Height);
            Assert.AreEqual(borderWidth, textField.BorderWidth);
            Assert.IsTrue(textField.IsStatic is true);
        }
    }
}
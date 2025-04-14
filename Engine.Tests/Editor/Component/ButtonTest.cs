using Raylib_cs;
using VisualNovelEngine.Engine.Editor;
using VisualNovelEngine.Engine.Editor.Component;
using VisualNovelEngine.Engine.Editor.Component.Command;
using VisualNovelEngine.Engine.Editor.Interface;

namespace VisualNovelEngine.Engine.Tests.Engine.Component
{
    [TestClass]
    public class ButtonTest
    {
        [TestMethod]
        public void ButtonCreationTest()
        {
            VisualNovelEngine.Engine.Component.Engine engine = new();
            VisualNovelEngine.Engine.Editor.Component.Editor editor = engine.CreateEditor("Test", @"../../../Engine/Data/PlaceholderEditor.json");
            int x = 100;
            int y = 100;
            string text = "Click Me";
            int width = 200;
            int height = 50;
            int borderWidth = 2;
            Color color = Color.White;
            Color borderColor = Color.Black;
            Color hoverColor = Color.Gray;
            ICommand command = new EmptyCommand();
            Button button = new(editor,x, y, text,false, width, height,borderWidth, color,borderColor, hoverColor, command,Button.ButtonType.Trigger);

            Assert.AreEqual(x, button.XPosition);
            Assert.AreEqual(y, button.YPosition);
            Assert.AreEqual(text, button.Text);
            Assert.AreEqual(width, button.Width);
            Assert.AreEqual(height, button.Height);
            Assert.AreEqual(borderWidth, button.BorderWidth);
            Assert.AreEqual(color, button.Color);
            Assert.AreEqual(borderColor, button.BorderColor);
            Assert.AreEqual(hoverColor, button.HoverColor);
            Assert.AreEqual(command, button.Command);
            Assert.AreEqual(Button.ButtonType.Trigger, button.Type);
        }
        [TestMethod]
        public void ButtonAddCommandTest() {
            VisualNovelEngine.Engine.Component.Engine engine = new();
            VisualNovelEngine.Engine.Editor.Component.Editor editor = engine.CreateEditor("Test", @"../../../Engine/Data/PlaceholderEditor.json");
            int x = 100;
            int y = 100;
            string text = "Click Me";
            int width = 200;
            int height = 50;
            int borderWidth = 2;
            Color color = Color.White;
            Color borderColor = Color.Black;
            Color hoverColor = Color.Gray;
            ICommand command = new EmptyCommand();
            Button button = new(editor,x, y, text,false, width, height,borderWidth, color,borderColor, hoverColor, command,Button.ButtonType.Trigger);
            command = new ExitWindowCommand(editor);
            button.AddCommand(command);
            Assert.AreEqual(command, button.Command);
        }
    }
}
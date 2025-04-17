namespace VisualNovelEngine.Engine.Tests.Engine.Component
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Raylib_cs;
    using VisualNovelEngine.Engine.Component;
    using VisualNovelEngine.Engine.Editor.Component.Command;
    using VisualNovelEngine.Engine.Editor.Interface;

    [TestClass]
    public class InputFieldTest
    {
        // Test method to check if the input field is created correctly
        [TestMethod]
        public void InputFieldCreationTest()
        {
            // Arrange
            int x = 100;
            int y = 100;
            string text = "Input";
            string buttonText = "Enter text";
            int width = 200;
            int height = 50;
            Color color = Color.White;
            Color textColor = Color.Black;
            Color hoverColor = Color.Gray;
            ICommand command = new EmptyCommand();

            // Act
            InputField inputField = new(x, y, text, buttonText, width, height, color, textColor, hoverColor, command);

            // Assert
            Assert.AreEqual(x, inputField.XPosition);
            Assert.AreEqual(y, inputField.YPosition);
            Assert.AreEqual(text, inputField.Text);
            Assert.IsNotNull(inputField.Button);
            Assert.AreEqual(buttonText, inputField.Button?.Text);
            Assert.AreEqual(width, inputField.Width);
            Assert.AreEqual(height, inputField.Height);
            Assert.AreEqual(color, inputField.Color);
            Assert.AreEqual(textColor, inputField.TextColor);
            Assert.AreEqual(hoverColor, inputField.HoverColor);
        }
    }
}
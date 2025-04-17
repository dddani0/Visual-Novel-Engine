namespace VisualNovelEngine.Engine.Tests.Engine.Component
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Raylib_cs;
    using VisualNovelEngine.Engine.Component;
    using VisualNovelEngine.Engine.Editor.Component.Command;
    using VisualNovelEngine.Engine.Editor.Interface;

    [TestClass]
    public class WindowTest
    {
        // Test method to check if the window is created correctly
        [TestMethod]
        public void WindowNewProjectTypeCreationTest()
        {
            Engine engine = new();

            // Arrange
            int x = 100;
            int y = 100;
            int width = 200;
            int height = 50;
            WindowType windowType = WindowType.NewProject;

            // Act
            Window window = new(engine, x, y, width, height, windowType);

            // Assert
            Assert.AreEqual(x, window.XPosition);
            Assert.AreEqual(y, window.YPosition);
            Assert.AreEqual(width, window.Width);
            Assert.AreEqual(height, window.Height);
            Assert.AreEqual(windowType, window.Type);
        }
        [TestMethod]
        public void WindowImportProjectTypeCreationTest()
        {
            Engine engine = new();

            // Arrange
            int x = 100;
            int y = 100;
            int width = 200;
            int height = 50;
            WindowType windowType = WindowType.ImportProject;

            // Act
            Window window = new(engine, x, y, width, height, windowType);

            // Assert
            Assert.AreEqual(x, window.XPosition);
            Assert.AreEqual(y, window.YPosition);
            Assert.AreEqual(width, window.Width);
            Assert.AreEqual(height, window.Height);
            Assert.AreEqual(windowType, window.Type);
        }
        [TestMethod]
        public void WindowPlayProjectTypeCreationTest()
        {
            Engine engine = new();

            // Arrange
            int x = 100;
            int y = 100;
            int width = 200;
            int height = 50;
            WindowType windowType = WindowType.PlayProject;

            // Act
            Window window = new(engine, x, y, width, height, windowType);

            // Assert
            Assert.AreEqual(x, window.XPosition);
            Assert.AreEqual(y, window.YPosition);
            Assert.AreEqual(width, window.Width);
            Assert.AreEqual(height, window.Height);
            Assert.AreEqual(windowType, window.Type);
        }
    }
}
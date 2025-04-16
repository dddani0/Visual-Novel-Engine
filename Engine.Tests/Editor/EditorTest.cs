using Microsoft.VisualStudio.TestTools.UnitTesting;
using Raylib_cs;
using VisualNovelEngine.Engine.Component;
using VisualNovelEngine.Engine.Editor.Component;
using VisualNovelEngine.Engine.Editor.Component.Command;
using VisualNovelEngine.Engine.Editor.Interface;

namespace VisualNovelEngine.Engine.Tests.Editor.Component
{
    [TestClass]
    public class EditorTest
    {
        /// <summary>
        /// Test the creation of an Editor object.
        /// </summary>
        [TestMethod]
        public void EditorCreationTest()
        {
            VisualNovelEngine.Engine.Component.Engine engine = new();
            VisualNovelEngine.Engine.Editor.Component.Editor editor = engine.CreateEditor("Test", @"../../../Engine/Data/PlaceholderEditor.json");
            Assert.IsNotNull(editor);
        }
    }
}
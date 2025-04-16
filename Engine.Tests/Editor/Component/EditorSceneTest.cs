using VisualNovelEngine.Engine.Editor.Component;

namespace VisualNovelEngine.Engine.Tests.Editor.Component
{
    [TestClass]
    public class EditorSceneTest
    {
        /// <summary>
        /// Test the creation of a Scene object.
        /// </summary>
        [TestMethod]
        public void EditorSceneCreationTest()
        {
            VisualNovelEngine.Engine.Component.Engine engine = new();
            VisualNovelEngine.Engine.Editor.Component.Editor editor = engine.CreateEditor("Test", @"../../../Engine/Data/PlaceholderEditor.json");
            int x = 0;
            int y = 600;
            string name = "testName";
            Scene scene = new(editor, new(editor, x, y, [], []), name, [], []);
            Assert.IsNotNull(scene);
            Assert.AreEqual(name, scene.Name);
        }
    }
}
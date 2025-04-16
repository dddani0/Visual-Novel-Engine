using VisualNovelEngine.Engine.Editor.Component;

namespace VisualNovelEngine.Engine.Tests.Editor.Component
{
    [TestClass]
    public class InspectorWindowTest
    {
        /// <summary>
        /// Test the creation of an InspectorWindow object.
        /// </summary>
        [TestMethod]
        public void InspectorWindowCreationTest()
        {
            VisualNovelEngine.Engine.Component.Engine engine = new();
            VisualNovelEngine.Engine.Editor.Component.Editor editor = engine.CreateEditor("Test", @"../../../Engine/Data/PlaceholderEditor.json");
            int x = 0;
            int y = 0;
            int enabledHorizontalComponentCount = 1;
            InspectorWindow inspectorWindow = new(editor, x, y, enabledHorizontalComponentCount);
            Assert.IsNotNull(inspectorWindow);
            Assert.AreEqual(x, inspectorWindow.XPosition);
            Assert.AreEqual(y, inspectorWindow.YPosition);
            Assert.AreEqual(enabledHorizontalComponentCount, inspectorWindow.EnabledRowComponentCount);
        }
    }
}
using VisualNovelEngine.Engine.Editor.Component;
using VisualNovelEngine.Engine.Game.Component.Action;

namespace VisualNovelEngine.Engine.Tests.Editor.Component
{
    [TestClass]
    public class EditorTimelineTest
    {
        /// <summary>
        /// Test the creation of a Timeline object.
        /// </summary>
        [TestMethod]
        public void EditorTimelineCreationTest()
        {
            VisualNovelEngine.Engine.Component.Engine engine = new();
            VisualNovelEngine.Engine.Editor.Component.Editor editor = engine.CreateEditor("Test", @"../../../Engine/Data/PlaceholderEditor.json");
            Timeline timeline = new(editor, 0, 0, [], []);
            Assert.IsNotNull(timeline);
            Assert.AreEqual(0, timeline.XPosition);
            Assert.AreEqual(0, timeline.YPosition);
            Assert.IsNotNull(timeline.Actions);
            Assert.IsNotNull(timeline.TimelineIndepententActions);
        }
        /// <summary>
        /// Test the addition of an action to a Timeline object.
        /// </summary>
        [TestMethod]
        public void EditorTimelineAddActionTest()
        {
            VisualNovelEngine.Engine.Component.Engine engine = new();
            VisualNovelEngine.Engine.Editor.Component.Editor editor = engine.CreateEditor("Test", @"../../../Engine/Data/PlaceholderEditor.json");
            VisualNovelEngine.Engine.Game.Component.Game game = engine.CreateGame(@"../../../Engine/Data/PlaceholderGameBuild.json");
            Timeline timeline = new(editor, 0, 0, [], []);
            NativeLoadSceneAction action = new(game, 0);
            timeline.AddAction(action);
            Assert.AreEqual(1, timeline.Actions.Count);
            Assert.AreEqual(action, timeline.Actions[0]);
        }
    }
}
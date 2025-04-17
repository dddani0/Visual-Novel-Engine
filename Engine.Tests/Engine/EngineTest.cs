namespace VisualNovelEngine.Engine.Tests.Engine.Component
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Raylib_cs;
    using VisualNovelEngine.Engine.Component;
    using VisualNovelEngine.Engine.Editor.Component.Command;
    using VisualNovelEngine.Engine.Editor.Interface;

    [TestClass]
    public class EngineTest
    {
        /// <summary>
        /// Test method to check if the engine is created correctly.
        /// </summary>
        [TestMethod]
        public void EngineCreationTest()
        {
            Engine engine = new();
            Assert.IsNotNull(engine);
        }
        /// <summary>
        /// Test method to check if the engine's default state is set correctly.
        /// </summary>
        [TestMethod]
        public void EngineDefaultStateTest()
        {
            Engine engine = new();
            EngineState expectedState = EngineState.Default;
            Assert.AreEqual(expectedState, engine.State);
        }
        /// <summary>
        /// Test method to check if the engine's title is set correctly.
        /// </summary>
        [TestMethod]
        public void EngineTitleTest()
        {
            Engine engine = new();
            string expectedTitle = "Vizuális Novella Motor";
            Assert.AreEqual(expectedTitle, engine.Title);
        }
        /// <summary>
        /// Test method to check if the engine's exit state is set correctly.
        /// </summary>
        [TestMethod]
        public void EngineChangeStateTest()
        {
            Engine engine = new();
            engine.CreateEditor("Test", @"../../../Engine/Data/PlaceholderEditor.json");
            EngineState expectedState = EngineState.Editor;
            Assert.AreEqual(expectedState, engine.State);
        }
        /// <summary>
        /// Test method to check if the engine's state changes correctly when creating an editor.
        /// </summary>
        [TestMethod]
        public void EngineEditorStateTransition()
        {
            Engine engine = new();
            engine.CreateEditor("Test", @"../../../Engine/Data/PlaceholderEditor.json");
            EngineState expectedState = EngineState.Editor;
            Assert.AreEqual(expectedState, engine.State);
        }
        /// <summary>
        /// Test method to check if the engine's state changes correctly when creating a game.
        /// </summary>
        [TestMethod]
        public void EngineGameStateTransition()
        {
            Engine engine = new();
            engine.CreateGame(@"../../../Engine/Data/PlaceholderGameBuild.json");
            EngineState expectedState = EngineState.Game;
            Assert.AreEqual(expectedState, engine.State);
        }
        /// <summary>
        /// Test method to check if the engine's exit state is set correctly.
        /// </summary>
        [TestMethod]
        public void EngineExitTest()
        {
            Engine engine = new();
            bool expectedExit = false;
            Assert.AreEqual(expectedExit, engine.Exit);
        }
        /// <summary>
        /// Test method to check if the engine's exit state changes correctly when set to true.
        /// </summary>
        [TestMethod]
        public void EngineTitleChangeTest()
        {
            Engine engine = new();
            string expectedTitle = "New Title";
            engine.SetWindowTitle(expectedTitle);
            Assert.AreEqual(expectedTitle, engine.Title);
        }
    }
}
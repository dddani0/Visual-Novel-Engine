using VisualNovelEngine.Engine.Game.Interface;

namespace VisualNovelEngine.Engine.Game.Component.Action
{
    /// <summary>
    /// Loads a new scene natively (without trigger).
    /// </summary>
    class NativeLoadSceneAction : IAction, ISettingsEvent, IButtonEvent
    {
        /// <summary>
        /// Active game.
        /// </summary>
        private readonly Game Game;
        /// <summary>
        /// The ID of the scene, which'll be loaded.
        /// </summary>
        internal int sceneID;
        /// <summary>
        /// Constructor for the LoadSceneAction.
        /// </summary>
        /// <param name="game">Active Game</param>
        /// <param name="sceneId"></param>
        public NativeLoadSceneAction(Game game, int sceneId)
        {
            Game = game;
            sceneID = sceneId;
        }
        /// <summary>
        /// Load scene with the correct ID.
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void PerformAction()
        {
            if (!Game.Scenes.Any(scene => scene.id == sceneID))
            {
                throw new Exception($"Scene with {sceneID} ID not found.");
            }
            else
            {
                var nextScene = Game.Scenes.First(scene => scene.id == sceneID);
                Game.LoadScene(nextScene);
                return;
            }
        }
    }
}
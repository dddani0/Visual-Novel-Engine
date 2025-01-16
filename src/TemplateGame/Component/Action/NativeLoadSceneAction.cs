using TemplateGame.Interface;

namespace TemplateGame.Component.Action
{
    /// <summary>
    /// Loads a new scene natively (without trigger).
    /// </summary>
    class NativeLoadSceneAction : IEvent, ISettingsEvent, IButtonEvent
    {
        /// <summary>
        /// Active game.
        /// </summary>
        private readonly Game Game;
        /// <summary>
        /// The ID of the scene, which'll be loaded.
        /// </summary>
        private readonly long sceneID;
        /// <summary>
        /// Constructor for the LoadSceneAction.
        /// </summary>
        /// <param name="game">Active Game</param>
        /// <param name="sceneId"></param>
        public NativeLoadSceneAction(Game game, long sceneId)
        {
            Game = game;
            sceneID = sceneId;
        }
        /// <summary>
        /// Load scene with the correct ID.
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void PerformEvent()
        {
            if (!Game.Scenes.Any(scene => scene.Id == sceneID))
            {
                throw new Exception($"Scene with {sceneID} ID not found.");
            }
            else
            {
                var nextScene = Game.Scenes.First(scene => scene.Id == sceneID);
                Game.LoadScene(nextScene);
                return;
            }
        }
    }
}
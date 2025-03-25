using VisualNovelEngine.Engine.Game.Interface;

namespace VisualNovelEngine.Engine.Game.Component.Action
{
    /// <summary>
    /// Loads a new scene with trigger.
    /// </summary>
    class LoadSceneAction : IAction, ISettingsEvent, IButtonEvent
    {
        /// <summary>
        /// Active game.
        /// </summary>
        private readonly Game Game;
        /// <summary>
        /// The name of the trigger variable.
        /// </summary>
        internal string TriggerVariableName { get; set; }
        /// <summary>
        /// The ID of the scene, which'll be loaded.
        /// </summary>
        internal int sceneID;
        /// <summary>
        /// Constructor for the LoadSceneAction.
        /// </summary>
        /// <param name="game">Active Game.</param>
        /// <param name="sceneId">The ID of the scene</param>
        /// <param name="triggerVariableName">The name of the triggering variable.</param>
        public LoadSceneAction(Game game, int sceneId, string triggerVariableName)
        {
            Game = game;
            sceneID = sceneId;
            TriggerVariableName = triggerVariableName;
        }
        /// <summary>
        /// Load scene with the correct ID if the condition is met.
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void PerformAction()
        {
            var variable = Game.VariableList.First(s => s.Name.Equals(TriggerVariableName));
            if (variable.Value.ToString() == "False") return;
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
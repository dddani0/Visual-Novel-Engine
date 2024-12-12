namespace EngineComponents.Actions
{
    /// <summary>
    /// Loads a new scene with trigger.
    /// </summary>
    class LoadSceneAction : IEvent
    {
        readonly Game Game;
        private bool triggered = false;
        readonly long sceneID;
        public LoadSceneAction(Game game, long sceneId)
        {
            Game = game;
            sceneID = sceneId;
        }
        public void TriggerEvent() => triggered = true;
        public void PerformEvent()
        {
            if (triggered is false) return;
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
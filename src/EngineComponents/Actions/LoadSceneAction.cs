namespace EngineComponents.Actions
{
    /// <summary>
    /// Loads a new scene with trigger.
    /// </summary>
    class LoadSceneAction : IEvent
    {
        readonly Game Game;
        readonly Scene nextScene;
        private bool triggered = false;
        int SceneIndex = -1;
        public LoadSceneAction(Game game, int sceneIndex)
        {
            Game = game;
            SceneIndex = sceneIndex;
        }
        public LoadSceneAction(Game game, Scene scene)
        {
            Game = game;
            nextScene = scene;
        }
        public void TriggerEvent() => triggered = true;
        public void PerformEvent()
        {
            if (triggered is false) return;
            if (SceneIndex == -1) SceneIndex = Game.Scenes.IndexOf(nextScene);
            Game.LoadScene(SceneIndex);
        }
    }
}
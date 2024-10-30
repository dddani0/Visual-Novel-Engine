namespace EngineComponents.Actions
{
    /// <summary>
    /// Loads a new scene natively (without trigger).
    /// </summary>
    class NativeLoadSceneAction : IEvent
    {
        readonly Game Game;
        readonly Scene nextScene;
        int SceneIndex = -1;
        public NativeLoadSceneAction(Game game, int sceneIndex)
        {
            Game = game;
            SceneIndex = sceneIndex;
        }
        public NativeLoadSceneAction(Game game, Scene scene)
        {
            Game = game;
            nextScene = scene;
        }
        public void PerformEvent()
        {
            if (SceneIndex == -1) SceneIndex = Game.Scenes.IndexOf(nextScene);
            Game.LoadScene(SceneIndex);
        }
    }
}
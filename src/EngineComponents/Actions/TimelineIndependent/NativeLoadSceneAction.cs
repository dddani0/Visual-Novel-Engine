using EngineComponents.Interfaces;

namespace EngineComponents.Actions.TimelineIndependent
{
    /// <summary>
    /// Loads a new scene natively (without trigger).
    /// </summary>
    class NativeLoadSceneAction : IEvent, IButtonEvent
    {
        readonly Game Game;
        readonly long sceneID;
        public NativeLoadSceneAction(Game game, long sceneId)
        {
            Game = game;
            sceneID = sceneId;
        }
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
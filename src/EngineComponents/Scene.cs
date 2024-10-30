using Raylib_cs;

namespace EngineComponents
{
    public class Scene
    {
        /// <summary>
        /// A level, which a player can create, modify and add depth to it.
        /// </summary>
        internal long Id { get; }
        internal enum BackgroundOption
        {
            SolidColor,
            GradientVertical, GradientHorizontal,
            Image
        }
        internal string Name { get; set; }
        internal Timeline Timeline { get; set; }
        internal BackgroundOption Background { get; set; } = BackgroundOption.SolidColor;
        internal Color solidColor = Color.Gray;
        internal Texture2D imageTexture;
        internal Color[] gradientColor;
        internal Game ConcurrentGame { get; }
        internal bool HasActiveTextbox { get; private set; } = false;
        public Scene(string name, Game game)
        {
            Name = name;
            ConcurrentGame = game;
            Timeline = new();
        }
        internal void AddActionsToTimeline(List<IEvent> actions)
        {
            Timeline.ActionList.AddRange(actions);
            Timeline.UpdateTimelineFields();
        }
        internal void AddActionsToTimeline(IEvent action)
        {
            Timeline.ActionList.Add(action);
            Timeline.UpdateTimelineFields();
        }
        internal void ToggleTextBoxActivity() => HasActiveTextbox = !HasActiveTextbox;
    }
}
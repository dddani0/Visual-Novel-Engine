using Raylib_cs;
using TemplateGame.Interface;

namespace TemplateGame.Component
{
    /// <summary>
    /// A level, which a player can create, modify and add depth to it.
    /// </summary>
    public class Scene
    {
        /// <summary>
        /// The unique identifier of the scene.
        /// </summary>
        internal long Id { get; set; }
        /// <summary>
        /// The background type of the scene.
        /// </summary>
        internal enum BackgroundOption
        {
            SolidColor,
            GradientVertical, GradientHorizontal,
            Image
        }
        /// <summary>
        /// The name of the scene.
        /// </summary>
        internal string Name { get; set; }
        /// <summary>
        /// The timeline of the scene.
        /// </summary>
        internal Timeline Timeline { get; set; }
        /// <summary>
        /// The background of the scene.
        /// </summary>
        internal BackgroundOption Background { get; set; } = BackgroundOption.SolidColor;
        /// <summary>
        /// The color of the scene.
        /// </summary>
        internal Color solidColor;
        /// <summary>
        /// The image texture of the scene.
        /// </summary>
        internal Texture2D imageTexture;
        /// <summary>
        /// The gradient color of the scene.
        /// </summary>
        internal Color[] gradientColor;
        internal Game Game { get; }
        public Scene(string name, Game game)
        {
            Name = name;
            Game = game;
            Timeline = new();
        }
        /// <summary>
        /// Adds a list of actions to the timeline.
        /// </summary>
        /// <param name="actions">List of actions</param>
        internal void AddActionsToTimeline(List<IAction> actions)
        {
            Timeline.ActionList.AddRange(actions);
            Timeline.UpdateTimelineFields();
        }
        /// <summary>
        /// Adds an action to the timeline.
        /// </summary>
        /// <param name="action">List of action</param>
        internal void AddActionToTimeline(IAction action)
        {
            Timeline.ActionList.Add(action);
            Timeline.UpdateTimelineFields();
        }
    }
}
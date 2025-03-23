using TemplateGame.Interface;

namespace TemplateGame.Component
{
    /// <summary>
    /// Timeline holds the list of user assigned actions (e.g. Sprite change, textbox placement, etc.), which plays out during a scene.
    /// Timeline also holds the list of sprites that are currently active in the scene.
    /// Timeline aligns the sprites according to the screen.
    /// </summary>
    public class Timeline
    {
        /// <summary>
        /// The index of the current step in the timeline.
        /// </summary>
        int StepIndex { get; set; }
        /// <summary>
        /// The total number of steps in the timeline.
        /// </summary>
        int StepCount { get; set; }
        /// <summary>
        /// The list of actions in the timeline.
        /// </summary>
        internal List<IAction> ActionList { get; set; }
        /// <summary>
        /// The list of sprites that are currently active in the scene.
        /// They are rendered in the scene.
        /// </summary>
        internal List<Sprite> SpriteRenderList { get; set; }
        /// <summary>
        /// Creates a timeline.
        /// </summary>
        public Timeline()
        {
            ActionList = [];
            SpriteRenderList = [];
        }
        /// <summary>
        /// Executes the action in the timeline.
        /// </summary>
        public void ExecuteAction()
        {
            if (StepIndex == StepCount) return; //Load next scene
            ActionList.ElementAt(StepIndex).PerformAction();
        }
        /// <summary>
        /// Renders the active sprites in the timeline.
        /// </summary>
        public void RenderSprites()
        {
            AlignActiveSpritesAccordingToScreen();
            foreach (var sprite in SpriteRenderList)
            {
                sprite.Render();
            }
        }
        /// <summary>
        /// Moves to the next step in the timeline.
        /// </summary>
        public void NextStep()
        {
            StepIndex++;
        }
        /// <summary>
        /// Starts the timeline.
        /// </summary>
        public void StartTimeline()
        {
            StepIndex = 0;
            StepCount = ActionList.Count;
        }
        /// <summary>
        /// Updates the timeline fields.
        /// </summary>
        public void UpdateTimelineFields() => StepCount = ActionList.Count;

        /// <summary>
        /// Aligns the active sprites according to the screen.
        /// </summary>
        internal void AlignActiveSpritesAccordingToScreen()
        {
            int spriteCount = SpriteRenderList.Count;
            for (int i = 0; i < spriteCount; i++)
            {
                Sprite currentSprite = SpriteRenderList[i];
                currentSprite.AlignItems(spriteCount, i);
            }
        }
    }
}
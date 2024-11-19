namespace EngineComponents
{
    public class Timeline
    {
        /// <summary>
        /// Timeline holds the list of user assigned actions (e.g. Sprite change, textbox placement, etc.), which plays out during a scene.
        /// </summary>

        int StepIndex { get; set; }
        int StepCount { get; set; }
        internal List<IEvent> ActionList { get; set; }
        internal List<Sprite> RenderList { get; set; }
        public Timeline()
        {
            ActionList = [];
            RenderList = [];
        }

        /// <summary>
        /// Executes the action in the timeline.
        /// </summary>
        public void ExecuteAction()
        {
            if (StepIndex == StepCount) return; //Load next scene
            ActionList.ElementAt(StepIndex).PerformEvent();
        }

        /// <summary>
        /// Renders the active sprites in the timeline.
        /// </summary>
        public void RenderSprites()
        {
            AlignActiveSpritesAccordingToScreen();
            foreach (var sprite in RenderList)
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
            int spriteCount = RenderList.Count();
            for (int i = 0; i < RenderList.Count; i++)
            {
                Sprite currentSprite = RenderList[i];
                currentSprite.AlignItems(spriteCount, i);
            }
        }
    }
}
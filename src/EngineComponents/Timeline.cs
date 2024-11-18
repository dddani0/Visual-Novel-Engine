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
        }


        /// <summary>
        /// Executes the action in the timeline.
        /// </summary>
        public void ExecuteAction()
        {
            if (StepIndex == StepCount) return; //Load next scene
            RenderAlignedObjectsFromList(); //render the graphics.
            ActionList.ElementAt(StepIndex).PerformEvent();
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
        /// Renders the objects in the list.
        /// </summary>
        private void RenderAlignedObjectsFromList()
        {
            int numberOfEnabledObjects = RenderList.Count(x => x.Enabled);
            int count = 0;
            foreach (var obj in RenderList)
            {
                if (count == numberOfEnabledObjects) break;
                if (obj.Enabled)
                {
                    obj.AlignItems(RenderList, count);
                    count++;
                }
            }
        }
    }
}
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
        internal bool InProgress { get; set; } = false;

        public Timeline()
        {
            ActionList = [];
        }

        public void ExecuteAction()
        {
            if (StepIndex == StepCount) return; //Load next scene
            ActionList.ElementAt(StepIndex).PerformEvent();
            ToggleProgress();
        }
        public void NextStep()
        {
            StepIndex++;
        }
        public void StartTimeline()
        {
            StepIndex = 0;
            StepCount = ActionList.Count;
        }
        public void UpdateTimelineFields() => StepCount = ActionList.Count;
        public void ToggleProgress() => InProgress = !InProgress;
    }
}
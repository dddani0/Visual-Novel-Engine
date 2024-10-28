namespace EngineComponents
{
    class Timeline
    {
        /// <summary>
        /// Timeline holds the list of user assigned actions (e.g. Sprite change, textbox placement, etc.), which plays out during a scene.
        /// </summary>
        int StepIndex { get; set; }
        int StepCount { get; set; }
        List<IEvent> ActionList { get; set; }

        public void ExecuteAction() => ActionList.ElementAt(StepIndex).PerformEvent();
        public void NextStep() => StepIndex++;
        public void StartTimeline()
        {
            StepIndex = 0;
            StepCount = ActionList.Count;
        }
    }
}
namespace EngineComponents
{
    class Timeline
    {
        /// <summary>
        /// Timeline holds the list of user assigned actions (e.g. Sprite change, textbox placement, etc.), which plays out during a scene.
        /// </summary>
        int StepIndex { get; set; }
        int StepCount { get; set; }

        public void NextStep()
        {
            StepIndex++;
        }
    }
}
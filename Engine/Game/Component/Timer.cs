using Raylib_cs;

namespace VisualNovelEngine.Engine.Game.Component
{
    public class Timer
    {
        /// <summary>
        /// Creates a timer.
        /// </summary>
        /// <param name="timerValue">Number of seconds</param>
        public Timer(float timerValue) => Seconds = timerValue;
        /// <summary>
        /// The number of seconds.
        /// </summary>
        private float Seconds { get; }
        /// <summary>
        /// The current value of the timer.
        /// </summary>
        public float CurrentTimerValue { get; set; }
        /// <summary>
        /// Checks if the timer is on cooldown.
        /// </summary>
        public bool OnCooldown() => CurrentTimerValue > 0;
        /// <summary>
        /// Decreases the timer.
        /// </summary>
        public void DecreaseTimer() => CurrentTimerValue -= (float)Raylib.GetFrameTime();
        /// <summary>
        /// Resets the timer.
        /// </summary>
        public void ResetTimer() => CurrentTimerValue = Seconds;
    }
}
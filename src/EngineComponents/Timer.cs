using Raylib_cs;

namespace EngineComponents
{
    public class Timer
    {
        public Timer(float timerValue) => this.timerValue = timerValue;

        private float timerValue { get; }

        public float currentTimerValue { get; set; }

        public bool OnCooldown() => currentTimerValue > 0;

        public void DecreaseTimer() => currentTimerValue -= (float)Raylib.GetFrameTime();

        public void ResetTimer() => currentTimerValue = timerValue;
    }
}
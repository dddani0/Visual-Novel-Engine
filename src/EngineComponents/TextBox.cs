using Raylib_cs;

namespace EngineComponents
{
    public class TextBox
    {
        /// <summary>
        /// Store, Write, Edit and Delete text according to need.
        /// </summary>
        /// <param name="data">text data, which is to be written out.</param>
        /// <param name="cps">characters per second</param>
        private TextBox(string data, double cps, int xpos, int ypos)
        {
            content = data;
            cpsTextSpeed = cps;
            secondTimer = new Timer(1 / (float)cpsTextSpeed);
            currentIdx = 0;
            maxIdx = content.Length;
            output = "";
            isEnabled = true;
        }

        internal bool isFinished => currentIdx == maxIdx;
        internal void WriteToScreen()
        {
            if (isEnabled is false) return;
            Raylib.DrawText(output, 12, 12, 20, Color.Black);
            if (isFinished is true) return;
            if (secondTimer.OnCooldown() is true)
            {
                secondTimer.DecreaseTimer();
                return;
            }
            output += content[currentIdx];
            incrementIndex();
            secondTimer.ResetTimer();

        }

        internal void ToggleData()
        {
            isEnabled = !isEnabled;
        }
        internal Timer secondTimer { get; private set; }
        internal string content { get; private set; }
        internal string output { get; private set; }
        private double cpsTextSpeed { get; }
        private int currentIdx { get; set; }
        private int incrementIndex() => currentIdx++;
        private int maxIdx { get; }
        private bool isEnabled { get; set; }
        public static TextBox createNewTextBox(double characterPerSecond, int xpos, int ypos, string textBoxContent) => new TextBox(textBoxContent, characterPerSecond, 1, 2);
        //public static TextBox createNewTextBox(double characterPerSecond, string[] textBoxContent) => new TextBox(textBoxContent, characterPerSecond);
    }
}
using System.Numerics;
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
        readonly int[] textMargin = [5, 5];
        private TextBox(List<String> data, double cps, int xpos, int ypos, int xSize, int ySize)
        {
            content = data;
            cpsTextSpeed = cps;
            secondTimer = new Timer(1 / (float)cpsTextSpeed);
            currentIdx = 0;
            maxIdx = content[0].Length;
            maxTextDataindex = content.Count;
            output = "";
            isEnabled = true;
            position = [xpos, ypos];
            scale = [xSize, ySize];
            box = new Rectangle(position[0], position[1], scale[0], scale[1]);
            textBatchDone = false;
        }
        internal void ToggleNextTextBatch()
        {
            currentIdx = 0;
            maxIdx = content[currentTextDataIndex].Length;
            textBatchDone = false;
            secondTimer.ResetTimer();
        }
        internal void WriteToScreen()
        {
            if (isEnabled is false) return;
            Raylib.DrawRectangle((int)box.Position.X, (int)box.Position.Y, (int)box.Width, (int)box.Height, Color.Black);
            Raylib.DrawText(sanatizedOutput, position[0] + textMargin[0], position[1] + textMargin[1], 20, Color.White);
            if (isFinished is true) return;
            if (Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                textBatchDone = true;
                output = content[currentTextDataIndex];
                currentIdx = maxIdx;
                secondTimer.ResetTimer();
            }
            if (secondTimer.OnCooldown() is true)
            {
                secondTimer.DecreaseTimer();
                return;
            }
            output += content[currentTextDataIndex][currentIdx];
            incrementIndex();
            secondTimer.ResetTimer();

        }
        internal bool isFinished => currentIdx == maxIdx;
        internal void ToggleData() => isEnabled = !isEnabled;
        internal Timer secondTimer { get; private set; }
        internal List<String> content { get; private set; }
        private string output { get; set; }
        private string sanatizedOutput => output.Replace("\n", "").Replace("\t", "");
        private double cpsTextSpeed { get; }
        private int currentIdx { get; set; }
        private int maxIdx { get; set; }
        private int currentTextDataIndex { get; set; }
        private int maxTextDataindex { get; set; }
        private int incrementTextDataIndex() => currentTextDataIndex++;
        private int incrementIndex() => currentIdx++;

        private int[] position { get; set; }
        private bool isEnabled { get; set; }
        private bool wordWrap { get; set; }
        private bool textBatchDone { get; set; }
        internal int xPosition => position[0];
        internal int yPosition => position[1];
        internal int xScale => position[0];
        internal int yScale => position[1];
        internal Rectangle box { get; set; }
        private int[] scale { get; set; }
        public static TextBox createNewTextBox(
            double characterPerSecond,
            int xPos,
            int yPos,
            int xSize,
            int ySize,
            List<String> textBoxContent)
            =>
            new(
                textBoxContent,
                characterPerSecond,
                xPos,
                yPos,
                xSize,
                ySize);
    }
}
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
            Content = data;
            CPSTextSpeed = cps;
            SecondTimer = new Timer(1 / (float)CPSTextSpeed);
            CurrentIdx = 0;
            MaxIdx = Content[0].Length;
            MaxTextDataindex = Content.Count;
            Output = "";
            IsEnabled = true;
            Position = [xpos, ypos];
            Scale = [xSize, ySize];
            Box = new Rectangle(Position[0], Position[1], Scale[0], Scale[1]);
            TextBatchDone = false;
        }
        internal void ToggleNextTextBatch()
        {
            CurrentIdx = 0;
            MaxIdx = Content[CurrentTextDataIndex].Length;
            TextBatchDone = false;
            SecondTimer.ResetTimer();
        }
        internal void WriteToScreen()
        {
            if (IsEnabled is false) return;
            Raylib.DrawRectangle((int)Box.Position.X, (int)Box.Position.Y, (int)Box.Width, (int)Box.Height, Color.Black);
            Raylib.DrawText(SanatizedOutput, Position[0] + textMargin[0], Position[1] + textMargin[1], 20, Color.White);
            if (IsFinished is true) return;
            if (Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                TextBatchDone = true;
                Output = Content[CurrentTextDataIndex];
                CurrentIdx = MaxIdx;
                SecondTimer.ResetTimer();
            }
            if (SecondTimer.OnCooldown() is true)
            {
                SecondTimer.DecreaseTimer();
                return;
            }
            Output += Content[CurrentTextDataIndex][CurrentIdx];
            IncrementIndex();
            SecondTimer.ResetTimer();

        }
        internal bool IsFinished => CurrentIdx == MaxIdx;
        internal void ToggleData() => IsEnabled = !IsEnabled;
        internal Timer SecondTimer { get; private set; }
        internal List<String> Content { get; private set; }
        private string Output { get; set; }
        private string SanatizedOutput => Output.Replace("\n", "").Replace("\t", "");
        private double CPSTextSpeed { get; }
        private int CurrentIdx { get; set; }
        private int MaxIdx { get; set; }
        private int CurrentTextDataIndex { get; set; }
        private int MaxTextDataindex { get; set; }
        private int IncrementTextDataIndex() => CurrentTextDataIndex++;
        private int IncrementIndex() => CurrentIdx++;

        private int[] Position { get; set; }
        private bool IsEnabled { get; set; }
        private bool WordWrap { get; set; }
        private bool TextBatchDone { get; set; }
        internal int XPosition => Position[0];
        internal int YPosition => Position[1];
        internal int XScale => Position[0];
        internal int YScale => Position[1];
        internal Rectangle Box { get; set; }
        private int[] Scale { get; set; }
        public static TextBox CreateNewTextBox(
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
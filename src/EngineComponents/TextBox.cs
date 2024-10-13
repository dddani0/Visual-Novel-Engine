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
        readonly int[] textMargin = [10, 10];
        private TextBox(List<String> data, double cps, Font theFont, int xpos, int ypos, int xSize, int ySize)
        {
            Content = data;
            CPSTextSpeed = cps;
            SecondTimer = new Timer(1 / (float)CPSTextSpeed);
            TextIndex = 0;
            TextCount = Content[0].Length;
            TextCollectionCount = Content.Count;
            Output = String.Empty;
            IsEnabled = true;
            Position = [xpos, ypos];
            Scale = [xSize, ySize];
            Box = new Rectangle(Position[0], Position[1], Scale[0], Scale[1]);
            TextBatchDone = false;
            CurrentFont = theFont;
            CharacterWidth = (CurrentFont.BaseSize + CurrentFont.GlyphPadding) / 2;
            CharacterHeigth = CurrentFont.BaseSize + CurrentFont.GlyphPadding;
            MaximumCharacterCount = (int)((Box.Width - textMargin[0]) / CharacterWidth) / 2 ; //1/8-ad része írható karakterekkel
            MaximumRowCount = (int)(Box.Height - textMargin[1] / CharacterHeigth);
            Content[TextCollectionIndex] = FitLoadedStringToTextBox(Content[TextCollectionIndex]);
            CurrentLoadedData = Content[TextCollectionIndex];
        }
        private string FitLoadedStringToTextBox(string data)
        {
            if (data.Length < MaximumCharacterCount) return data;
            //
            bool IsFinished = false;
            string splittingText = data;
            string nextString = String.Empty;
            //
            int usedRows = 1;
            //
            while (IsFinished is false)
            {
                nextString = splittingText.Remove(0, (MaximumCharacterCount - 1) * usedRows);
                splittingText = splittingText.Insert((MaximumCharacterCount - 1) * usedRows, "\n\n");
                if (nextString.Length <= MaximumCharacterCount || usedRows >= MaximumRowCount)
                {
                    IsFinished = true;
                }
                else
                {
                    usedRows++;
                }
            }
            return splittingText;
            //bool isLineFittable = false;
            //string splittingText = CurrentLoadedData;
            //string nextString = splittingText;
            //string followingString = string.Empty;
            //while (isLineFittable is false)
            //{

            //need to consider word wrap
            // if (StringWidthToCoordinates(nextString) > LineDeadZone)
            // {
            //     int charIndex = 0;
            //     int rowCount = 1;
            //     bool foundIdx = false;
            //     string splittingTextTemporary = String.Empty;
            //     int splitIdx = 0;
            //     if (rowCount == RowDeadZone)
            //     {
            //         isLineFittable = true;
            //         goto endLoop;
            //     }
            //     nextString = splittingText.Remove(0, charIndex++);
            //     splittingText = splittingText.Insert(charIndex, splittingTextTemporary + "\n");
            // }
            // else isLineFittable = true;
            // endLoop:;
            //}
            //return splittingText;
        }


        private void ToggleNextTextBatch()
        {
            TextIndex = 0;
            TextCount = Content[TextCollectionIndex].Length;
            TextBatchDone = false;
            SecondTimer.ResetTimer();
        }
        internal void WriteToScreen()
        {
            if (IsEnabled is false) return;
            Raylib.DrawRectangle((int)Box.Position.X, (int)Box.Position.Y, (int)Box.Width, (int)Box.Height, Color.Black);
            Raylib.DrawRectangleLines((int)Box.Position.X, (int)Box.Position.Y, (int)Box.Width, (int)Box.Height, Color.Black);
            Raylib.DrawRectangle(Position[0] + textMargin[0] + CharacterWidth, Position[1] + textMargin[1], CharacterWidth, CurrentFont.BaseSize, Color.Red);

            Raylib.DrawTextEx(
                CurrentFont,
                SanatizedOutput,
                new Vector2(Position[0] + textMargin[0], Position[1] + textMargin[1]),
                CurrentFont.BaseSize,
                CurrentFont.GlyphPadding,
                Color.White);
            if (IsFinished is true) return;
            if (Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                TextBatchDone = true;
                Output = Content[TextCollectionIndex];
                TextIndex = TextCount;
                SecondTimer.ResetTimer();
            }
            if (SecondTimer.OnCooldown() is true)
            {
                SecondTimer.DecreaseTimer();
                return;
            }
            Output += Content[TextCollectionIndex][TextIndex];
            IncrementIndex();
            SecondTimer.ResetTimer();

        }
        private int StringWidthToCoordinates(string text) => Raylib.GetScreenWidth() / 2 - XScale / 2 + textMargin[0] + (text.Length * CharacterWidth);
        internal bool IsFinished => TextIndex == TextCount;
        internal void ToggleData() => IsEnabled = !IsEnabled;
        internal Timer SecondTimer { get; private set; }
        internal List<String> Content { get; private set; }
        private string CurrentLoadedData { get; set; }
        private string Output { get; set; }
        private string SanatizedOutput => Output.Replace("\t", String.Empty); //new line is acceptable
        private double CPSTextSpeed { get; }
        private int LineHeight { get; set; } //When to 
        private int StringWidth { get; }
        private int MaximumCharacterCount { get; }
        private int MaximumRowCount { get; }
        private int CharacterWidth { get; set; }
        private int CharacterHeigth { get; set; }
        private int TextIndex { get; set; }
        private int TextCount { get; set; }
        private int TextCollectionIndex { get; set; }
        private int TextCollectionCount { get; set; }
        private int IncrementTextDataIndex() => TextCollectionIndex++;
        private int IncrementIndex() => TextIndex++;
        private int[] Position { get; set; }
        private bool IsEnabled { get; set; }
        private bool WordWrap { get; set; }
        private bool TextBatchDone { get; set; }
        internal int XPosition => Position[0];
        internal int YPosition => Position[1];
        internal int XScale => Position[0];
        internal int YScale => Position[1];
        internal int CharacterCount => Output.Length;
        internal Rectangle Box { get; set; }
        internal Font CurrentFont { get; set; }
        private int[] Scale { get; set; }
        public static TextBox CreateNewTextBox(
            double characterPerSecond,
            Font activeFont,
            int xPos,
            int yPos,
            int xSize,
            int ySize,
            List<String> textBoxContent)
            =>
            new(
                textBoxContent,
                characterPerSecond,
                activeFont,
                xPos,
                yPos,
                xSize,
                ySize);
    }
}
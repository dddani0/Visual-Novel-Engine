using System.Numerics;
using System.Security.Cryptography;
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
        private TextBox(List<String> data, double cps, Font theFont, int xpos, int ypos, int xSize, int ySize, bool wordWrapEnabled)
        {
            Content = data;
            CPSTextSpeed = cps;
            //  
            WordWrap = wordWrapEnabled;
            //
            SecondTimer = new Timer(1 / (float)CPSTextSpeed);
            //
            TextIndex = 0;
            TextCollectionCount = Content.Count;
            //
            Output = String.Empty;
            //
            IsEnabled = true;
            TextBatchDone = false;
            //
            Position = [xpos, ypos];
            Scale = [xSize, ySize];
            Box = new Rectangle(Position[0], Position[1], Scale[0], Scale[1]);
            //
            CurrentFont = theFont;
            //
            CharacterWidth = (CurrentFont.BaseSize + CurrentFont.GlyphPadding) / 2;
            CharacterHeigth = CurrentFont.BaseSize + CurrentFont.GlyphPadding;
            MaximumCharacterCount = (int)(Box.Width - 2 * textMargin[0] - CharacterWidth) / CharacterWidth;
            MaximumRowCount = (int)((Box.Height - textMargin[1]) / CharacterHeigth);
            //
            Content[TextCollectionIndex] = FitLoadedStringToTextBox(Content[TextCollectionIndex]);
            CurrentLoadedData = Content[TextCollectionIndex];
            //
            TextCount = CurrentLoadedData.Length;
        }
        private TextBox(List<String> data, string title, double cps, Font theFont, int xpos, int ypos, int xSize, int ySize, bool wordWrapEnabled)
        {
            Content = data;
            CPSTextSpeed = cps;
            TextBoxTitle = title;
            //  
            WordWrap = wordWrapEnabled;
            //
            SecondTimer = new Timer(1 / (float)CPSTextSpeed);
            //
            TextIndex = 0;
            TextCollectionCount = Content.Count;
            //
            Output = String.Empty;
            //
            IsEnabled = true;
            TextBatchDone = false;
            //
            Position = [xpos, ypos];
            Scale = [xSize, ySize];
            Box = new Rectangle(Position[0], Position[1], Scale[0], Scale[1]);
            //
            CurrentFont = theFont;
            //
            CharacterWidth = (CurrentFont.BaseSize + CurrentFont.GlyphPadding) / 2;
            CharacterHeigth = CurrentFont.BaseSize + CurrentFont.GlyphPadding;
            MaximumCharacterCount = (int)(Box.Width - 2 * textMargin[0] - CharacterWidth) / CharacterWidth;
            MaximumRowCount = (int)((Box.Height - textMargin[1]) / CharacterHeigth);
            //
            Content[TextCollectionIndex] = FitLoadedStringToTextBox(Content[TextCollectionIndex]);
            CurrentLoadedData = Content[TextCollectionIndex];
            //
            TextCount = CurrentLoadedData.Length;
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
                int nextSplitIndex = (MaximumCharacterCount - 1) * usedRows;
                nextString = splittingText.Remove(0, nextSplitIndex); //
                //
                if (WordWrap) splittingText = WrapLine(splittingText, usedRows);
                else splittingText = splittingText.Insert(nextSplitIndex, "\n\n");
                if (nextString.Length <= MaximumCharacterCount || usedRows >= MaximumRowCount) IsFinished = true;
                else usedRows++;
                //
                if (usedRows == MaximumRowCount)
                {
                    string endString = nextString;
                    if (endString.Length > MaximumCharacterCount)
                    {
                        if (WordWrap)
                        {
                            endString = WrapLine(endString, 1);
                            int removeIdx = endString.IndexOf('\n');
                            endString = endString[..(removeIdx)];
                        }
                        else endString = endString[..(MaximumCharacterCount - 1)];
                        //
                        nextString = nextString.Replace(endString, String.Empty);
                    }
                    splittingText = splittingText.Replace(nextString, String.Empty);
                    Content.Insert(TextCollectionIndex + 1, nextString.Trim());
                    IsFinished = true;
                }
            }
            return splittingText;
        }
        string WrapLine(string wrappingLine, int rowNumber)
        {
            int newLineIndex = (MaximumCharacterCount - 1) * rowNumber;
            for (int i = newLineIndex; i >= 0; i--)
            {
                if (Char.IsWhiteSpace(wrappingLine[i]))
                {
                    wrappingLine = wrappingLine.Remove(i, 1);
                    wrappingLine = wrappingLine.Insert(i, "\n\n");
                    break;
                }
                if (i == 0 || wrappingLine[i] == '\n')
                    wrappingLine = wrappingLine.Insert(newLineIndex, "\n\n");
            }
            return wrappingLine;
        }

        private void ToggleNextTextBatch()
        {
            if (IncrementIndex() >= TextCollectionCount) return;
            IncrementTextDataIndex();
            //
            Content[TextCollectionIndex] = FitLoadedStringToTextBox(Content[TextCollectionIndex]);
            CurrentLoadedData = Content[TextCollectionIndex];
            //
            TextIndex = 0;
            TextCount = CurrentLoadedData.Length;
            //
            TextBatchDone = false;
            //
            SecondTimer.ResetTimer();
            //
            Output = String.Empty;
        }
        internal void WriteToScreen()
        {
            if (IsEnabled is false) return;
            Raylib.DrawRectangle((int)Box.Position.X, (int)Box.Position.Y, (int)Box.Width, (int)Box.Height, Color.Black);
            if (string.IsNullOrEmpty(TextBoxTitle) is false)
            {
                Raylib.DrawRectangle(XPosition, YPosition - CharacterHeigth, TextBoxTitle.Length * CharacterWidth + 2 * textMargin[0], CharacterHeigth, Color.Black);
                Raylib.DrawTextEx(CurrentFont, TextBoxTitle, new Vector2(XPosition + textMargin[0], YPosition - CharacterHeigth), CurrentFont.BaseSize, CurrentFont.GlyphPadding, Color.White);
            }
            Raylib.DrawRectangleLines((int)Box.Position.X, (int)Box.Position.Y, (int)Box.Width, (int)Box.Height, Color.Black);
            Raylib.DrawTextEx(CurrentFont, SanatizedOutput, new Vector2(Position[0] + textMargin[0], Position[1] + textMargin[1]),
                CurrentFont.BaseSize,
                CurrentFont.GlyphPadding,
                Color.White);
            if (TextCollectionIndex < TextCollectionCount && IsFinished && Raylib.IsMouseButtonPressed(MouseButton.Right))
                ToggleNextTextBatch();
            if (IsFinished is true) return;
            if (Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                TextBatchDone = true;
                Output = CurrentLoadedData;
                TextIndex = TextCount;
            }
            if (SecondTimer.OnCooldown() is true)
            {
                SecondTimer.DecreaseTimer();
                return;
            }
            if (TextIndex >= TextCount) return;
            Output += CurrentLoadedData[TextIndex];
            IncrementIndex();
            SecondTimer.ResetTimer();
        }
        internal bool IsFinished => TextIndex == TextCount;
        internal void ToggleData() => IsEnabled = !IsEnabled;
        internal Timer SecondTimer { get; private set; }
        internal List<String> Content { get; private set; }
        private string CurrentLoadedData { get; set; }
        private string Output { get; set; }
        private string SanatizedOutput => Output.Replace("\t", String.Empty);
        private string TextBoxTitle { get; set; }
        private double CPSTextSpeed { get; }
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
        internal int XScale => Scale[0];
        internal int YScale => Scale[1];
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
            bool wordWrap,
            List<String> textBoxContent)
            =>
            new(
                textBoxContent,
                characterPerSecond,
                activeFont,
                xPos,
                yPos,
                xSize,
                ySize, wordWrap);
        public static TextBox CreateNewTextBox(
            double characterPerSecond,
            Font activeFont,
            int xPos,
            int yPos,
            int xSize,
            int ySize,
            bool wordWrap,
            string textBoxTitle,
            List<String> textBoxContent)
            =>
            new(
                textBoxContent,
                textBoxTitle,
                characterPerSecond,
                activeFont,
                xPos,
                yPos,
                xSize,
                ySize, wordWrap);
    }
}
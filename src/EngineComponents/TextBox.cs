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

        readonly int[] textMargin = [10, 10];
        //ctors
        private TextBox(
            List<String> data, double cps, Font theFont, Color textBoxBackground, Color textBoxBorder, int xpos,
            int ypos, int xSize, int ySize, bool wordWrapEnabled)
        {
            Content = data;
            CPSTextSpeed = cps;
            //  
            WordWrap = wordWrapEnabled;
            //
            SecondTimer = new Timer(1 / (float)CPSTextSpeed);
            BlinkingCursorTimer = new Timer(0.5f);
            //
            TextIndex = 0;
            TextCollectionCount = Content.Count;
            //
            Output = String.Empty;
            //
            IsBlinking = true;
            IsEnabled = true;
            TextBatchDone = false;
            //
            Position = [xpos, ypos];
            Scale = [xSize, ySize];
            Box = new Rectangle(Position[0], Position[1], Scale[0], Scale[1]);
            //
            TextBoxBackground = textBoxBackground;
            TextBoxBorder = textBoxBorder;
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
            //
            Raylib.SetTextLineSpacing(CharacterHeigth);
        }
        private TextBox(List<String> data, string title, double cps, Font theFont, Color textBoxBackground, Color textBoxBorder, int xpos, int ypos, int xSize, int ySize, bool wordWrapEnabled)
        {
            Content = data;
            CPSTextSpeed = cps;
            TextBoxTitle = title;
            //  
            WordWrap = wordWrapEnabled;
            //
            SecondTimer = new Timer(1 / (float)CPSTextSpeed);
            BlinkingCursorTimer = new Timer(0.25f);
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
            TextBoxBackground = textBoxBackground;
            TextBoxBorder = textBoxBorder;
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
            //
            Raylib.SetTextLineSpacing(CharacterHeigth);
        }
        //members
        internal bool IsFinished => TextIndex == TextCount;
        internal void ToggleEnability() => IsEnabled = !IsEnabled;
        internal Timer SecondTimer { get; private set; }
        internal Timer BlinkingCursorTimer { get; private set; }
        internal List<String> Content { get; private set; }
        private string CurrentLoadedData { get; set; }
        private string Output { get; set; }
        private string SanatizedOutput => Output.Replace("\t", String.Empty);
        private string HeaderSanatization => TextBoxTitle.Replace("\t", String.Empty).Replace("\n", String.Empty);
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
        private bool IsBlinking { get; set; }
        private bool ToggleBlinking() => IsBlinking = !IsBlinking;
        internal int XPosition => Position[0];
        internal int YPosition => Position[1];
        internal int XScale => Scale[0];
        internal int YScale => Scale[1];
        internal Rectangle Box { get; set; }
        internal Font CurrentFont { get; set; }
        internal Color TextBoxBackground { get; set; }
        internal Color TextBoxBorder { get; set; }
        private int[] Scale { get; set; }

        /// <summary>
        /// Splits the string data, to fit into the textbox framework.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
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
                else splittingText = splittingText.Insert(nextSplitIndex, "\n");
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
                    TextCollectionCount = Content.Count; //update the collection size.
                    IsFinished = true;
                }
            }
            return splittingText;
        }

        /// <summary>
        /// Wraps a singular instance of textoverflow.
        /// </summary>
        /// <param name="wrappingLine">The line which'll proceed to be wrapped.</param>
        /// <param name="rowNumber">The number of row, that line is in.</param>
        /// <returns></returns>
        string WrapLine(string wrappingLine, int rowNumber)
        {
            int newLineIndex = (MaximumCharacterCount - 1) * rowNumber;
            for (int i = newLineIndex; i >= 0; i--)
            {
                if (Char.IsWhiteSpace(wrappingLine[i]))
                {
                    wrappingLine = wrappingLine.Remove(i, 1);
                    wrappingLine = wrappingLine.Insert(i, "\n");
                    break;
                }
                if (i == 0 || wrappingLine[i] == '\n')
                    wrappingLine = wrappingLine.Insert(newLineIndex, "\n");
            }
            return wrappingLine;
        }
        /// <summary>
        /// Cycle to the next unprocessed content data, and process it.
        /// </summary>
        private void ToggleNextTextBatch()
        {
            if (TextCollectionIndex >= TextCollectionCount - 1) { ToggleEnability(); return; };
            //
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
        /// <summary>
        /// Write currently loaded textdata to screen (within the textbox framework).
        /// </summary>
        internal void WriteToScreen()
        {
            //Return if disabled or finished. (render nothing)
            if (isTurnedOff() || isDone()) return;
            if (headerExists())
            {
                //header textbox frame
                Raylib.DrawRectangle(XPosition, YPosition - CharacterHeigth - textMargin[1], TextBoxTitle.Length * CharacterWidth + 2 * textMargin[0], CharacterHeigth, TextBoxBackground);
                Raylib.DrawRectangleLines((int)Box.Position.X, YPosition - CharacterHeigth, TextBoxTitle.Length * CharacterWidth + 2 * textMargin[0], CharacterHeigth, TextBoxBorder);
                //Headertext
                Raylib.DrawTextEx(CurrentFont, HeaderSanatization, new Vector2(XPosition + textMargin[0], YPosition - CharacterHeigth - textMargin[1]), CurrentFont.BaseSize, CurrentFont.GlyphPadding, Color.Black);
            }
            //Textbox and Border
            Raylib.DrawRectangle((int)Box.Position.X, (int)Box.Position.Y, (int)Box.Width, (int)Box.Height, TextBoxBackground);
            Raylib.DrawRectangleLines((int)Box.Position.X, (int)Box.Position.Y, (int)Box.Width, (int)Box.Height, TextBoxBorder);
            //draw current string data to screen.
            Raylib.DrawTextEx(CurrentFont, SanatizedOutput, new Vector2(Position[0] + textMargin[0], Position[1] + textMargin[1]),
                CurrentFont.BaseSize,
                CurrentFont.GlyphPadding,
                Color.White);
            if (shouldProceedNextBatch()) { ToggleNextTextBatch(); return; }
            if (IsFinished is true)
            {
                //blinking cursor
                if (BlinkingCursorTimer.OnCooldown()) BlinkingCursorTimer.DecreaseTimer();
                else
                {
                    ToggleBlinking();
                    BlinkingCursorTimer.ResetTimer();
                }
                if (IsBlinking)
                {
                    Raylib.DrawTextEx(CurrentFont, "I", new Vector2(XPosition + Box.Width - CharacterWidth - textMargin[0], YPosition + Box.Height - CharacterHeigth - textMargin[1]),
                    CurrentFont.BaseSize,
                    CurrentFont.GlyphPadding,
                    Color.White);
                }
                return;
            }
            if (isSkipBatch())
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
            return;
            //Local functions for readability
            bool isTurnedOff() => IsEnabled is false;
            bool isDone() => TextCollectionIndex >= TextCollectionCount;
            bool headerExists() => string.IsNullOrEmpty(TextBoxTitle) is false;
            bool isSkipBatch() => Raylib.IsMouseButtonPressed(MouseButton.Left);
            bool shouldProceedNextBatch() =>
                TextCollectionIndex < TextCollectionCount && IsFinished is true && Raylib.IsMouseButtonPressed(MouseButton.Left);

        }

        /// <summary>
        /// Create textbox for string data, without a header.
        /// Default color (Black and white) for textbox background and border colors.
        /// </summary>
        /// <param name="characterPerSecond">Characters per second</param>
        /// <param name="activeFont">The font which the textbox will use</param>
        /// <param name="xPos">X position of the textbox</param>
        /// <param name="yPos">Y position of the textbox</param>
        /// <param name="xSize">Textbox width</param>
        /// <param name="ySize">Textbox height</param>
        /// <param name="wordWrap">Should wrap the entire word when initiating a new line?</param>
        /// <param name="textBoxContent">text data</param>
        /// <returns></returns>
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
                activeFont, Color.Black, Color.White,
                xPos,
                yPos,
                xSize,
                ySize, wordWrap);
        /// <summary>
        /// Create textbox for string data, without a header.
        /// Custom color for textbox background and border color.
        /// </summary>
        /// <param name="characterPerSecond">Characters per second</param>
        /// <param name="activeFont">The font which the textbox will use</param>
        /// <param name="textBoxcolor">Background color of the textbox.</param>
        /// <param name="TextBoxBorder">Border color of the textbox.</param>
        /// <param name="xPos">X position of the textbox</param>
        /// <param name="yPos">Y position of the textbox</param>
        /// <param name="xSize">Textbox width</param>
        /// <param name="ySize">Textbox height</param>
        /// <param name="wordWrap">Should wrap the entire word when initiating a new line?</param>
        /// <param name="textBoxContent">text data</param>
        /// <returns></returns>
        public static TextBox CreateNewTextBox(
        double characterPerSecond,
        Font activeFont,
        Color textBoxcolor,
        Color textBoxBorderColor,
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
        textBoxcolor,
        textBoxBorderColor,
        xPos,
        yPos,
        xSize,
        ySize, wordWrap);
        /// <summary>
        /// Create textbox for string data with a header.
        /// Custom color for textbox background and border color.
        /// </summary>
        /// <param name="characterPerSecond">Characters per second</param>
        /// <param name="activeFont">The font which the textbox will use</param>
        /// <param name="xPos">X position of the textbox</param>
        /// <param name="yPos">Y position of the textbox</param>
        /// <param name="xSize">Textbox width</param>
        /// <param name="ySize">Textbox height</param>
        /// <param name="wordWrap">Should wrap the entire word when initiating a new line?</param>
        /// <param name="textBoxTitle">The header of the textbox.</param>
        /// <param name="textBoxContent">text data</param>
        /// <returns></returns>
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
                Color.Black,
                Color.White,
                xPos,
                yPos,
                xSize,
                ySize, wordWrap);
        /// <summary>
        /// Create textbox for string data with a header.
        /// Custom color for textbox background and border.
        /// </summary>
        /// <param name="characterPerSecond">Characters per second</param>
        /// <param name="activeFont">The font which the textbox will use</param>
        /// <param name="TextBoxcolor">Background color of the textbox.</param>
        /// <param name="TextBoxBorder">Border color of the textbox.</param>
        /// <param name="xPos">X position of the textbox</param>
        /// <param name="yPos">Y position of the textbox</param>
        /// <param name="xSize">Textbox width</param>
        /// <param name="ySize">Textbox height</param>
        /// <param name="wordWrap">Should wrap the entire word when initiating a new line?</param>
        /// <param name="textBoxTitle">The header of the textbox.</param>
        /// <param name="textBoxContent">text data</param>
        /// <returns></returns>
        public static TextBox CreateNewTextBox(
            double characterPerSecond,
            Font activeFont,
            Color TextBoxcolor,
            Color TextBoxBorder,
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
            TextBoxcolor, TextBoxBorder,
            xPos,
            yPos,
            xSize,
            ySize, wordWrap);
    }
}
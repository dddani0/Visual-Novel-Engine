using System.Numerics;
using System.Text.RegularExpressions;
using Raylib_cs;
using VisualNovelEngine.Engine.Game.Interface;

namespace VisualNovelEngine.Engine.Game.Component
{
    /// <summary>
    /// Store, Write, Edit and Delete text according to need.
    /// </summary>
    public class TextBox : IPermanentRenderingObject
    {
        /// <summary>
        /// The position type of the textbox.
        /// </summary>
        public enum PositionType
        {
            defaultPosition,
            upperPosition
        }
        internal PositionType TextBoxPositionType { get; set; }
        /// <summary>
        /// The horizontal margin of the text.
        /// </summary>
        internal int HorizontalTextMargin { get; set; }
        /// <summary>
        /// The vertical margin of the text.
        /// </summary>
        internal int VerticalTextMargin { get; set; }
        /// <summary>
        /// A timer for the textbox.
        /// </summary>
        internal Timer SecondTimer { get; private set; }
        /// <summary>
        /// A timer for the blinking cursor.
        /// </summary>
        internal Timer BlinkingCursorTimer { get; private set; }
        /// <summary>
        /// The content of the textbox.
        /// </summary>
        internal List<String> Content { get; set; } = [];
        /// <summary>
        /// The current loaded data of the textbox.
        /// </summary>
        private string CurrentLoadedData { get; set; }
        /// <summary>
        /// The output of the textbox.
        /// </summary>
        private string Output { get; set; }
        /// <summary>
        /// The output of the textbox without any illegal escape sequences.
        /// Legal escape sequence is a newline.
        /// </summary>
        internal string SanatizedOutput => Regex.Replace(Output, @"\\(?!n)", "");
        /// <summary>
        /// The header of the textbox.
        /// </summary>
        internal string SanatizedHeader => Regex.Unescape(Title);
        /// <summary>
        /// The title of the textbox.
        /// </summary>
        internal string Title { get; set; }
        /// <summary>
        /// The speed of the textbox (Characters per second).
        /// </summary>
        internal double CPSTextSpeed { get; set; }
        private const int TextBoxPositionYOffset = 5;
        /// <summary>
        /// The maximum character count of the textbox.
        /// </summary>
        private int MaximumCharacterCount { get; }
        /// <summary>
        /// The maximum number of rows enabled in a textbox.
        /// </summary>
        private int MaximumRowCount { get; }
        /// <summary>
        /// The width of the character.
        /// </summary>
        private int CharacterWidth { get; set; }
        /// <summary>
        /// The height of the character.
        /// </summary>
        private int CharacterHeigth { get; set; }
        /// <summary>
        /// The current character index of the text.
        /// </summary>
        private int TextIndex { get; set; }
        /// <summary>
        /// The current text count of the text.
        /// </summary>
        private int TextCount { get; set; }
        /// <summary>
        /// The current index of the text collection.
        /// </summary>
        private int TextCollectionIndex { get; set; }
        /// <summary>
        /// The count of the text collection.
        /// </summary>
        private int TextCollectionCount { get; set; }
        private int IncrementTextDataIndex() => TextCollectionIndex++;
        private int IncrementIndex() => TextIndex++;
        /// <summary>
        /// The position of the textbox.
        /// </summary>
        private int[] Position { get; set; }
        /// <summary>
        /// Check if the textbox is enabled.
        /// </summary>
        private bool IsEnabled { get; set; }
        internal bool IsDisabled() => IsEnabled is false;
        /// <summary>
        /// Should the text wrap when initiating a new line?
        /// </summary>
        internal bool WordWrap { get; set; }
        /// <summary>
        /// Check if the current batch of the textbox is done.
        /// </summary>
        private bool TextBatchDone { get; set; }
        /// <summary>
        /// Check if the textbox is blinking.
        /// </summary>
        private bool IsBlinking { get; set; }
        private bool ToggleBlinking() => IsBlinking = !IsBlinking;
        internal int XPosition => Position[0];
        internal int YPosition => Position[1];
        internal int XScale => Scale[0];
        internal int YScale => Scale[1];
        /// <summary>
        /// The box which abstractly represents the textbox.
        /// </summary>
        internal Rectangle Box { get; set; }
        /// <summary>
        /// The font of the textbox.
        /// </summary>
        internal Font CurrentFont { get; set; }
        /// <summary>
        /// The background and border color of the textbox.
        /// </summary>
        internal Color Color { get; set; }
        /// <summary>
        /// The border color of the textbox.
        /// </summary>
        internal Color BorderColor { get; set; }
        internal readonly Game Game;
        private int[] Scale { get; set; }
        /// <summary>
        /// Create a new textbox.
        /// </summary>
        /// <param name="data">List of string data</param>
        /// <param name="title">Title of the textbox. Nullable parameter.</param>
        /// <param name="cps">Characters per second</param>
        /// <param name="theFont"></param>
        /// <param name="textBoxBackground"></param>
        /// <param name="textBoxBorder"></param>
        /// <param name="textBoxPosition"></param>
        /// <param name="horizontalTextMargin"></param>
        /// <param name="verticalTextMargin"></param>
        /// <param name="wordWrapEnabled">Should wrap lines with consideration of word progress.</param>
        /// <param name="game">Active Game object</param>
        private TextBox(List<String> data, string title, double cps, Font theFont, Color textBoxBackground, Color textBoxBorder, PositionType textBoxPosition, int horizontalTextMargin, int verticalTextMargin, bool wordWrapEnabled, Game game)
        {
            Content = data;
            CPSTextSpeed = cps;
            Title = title;
            HorizontalTextMargin = horizontalTextMargin;
            VerticalTextMargin = verticalTextMargin;
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
            TextBoxPositionType = textBoxPosition;
            Position = TextBoxPositionType switch
            {
                PositionType.upperPosition => [Raylib.GetScreenWidth() / 2 - Convert.ToInt32(Raylib.GetScreenWidth() / 1.6f) / 2,
                                    Raylib.GetScreenHeight() - Convert.ToInt32(Raylib.GetScreenHeight() / 1.5f) - TextBoxPositionYOffset],
                _ => [Raylib.GetScreenWidth() / 2 - Convert.ToInt32(Raylib.GetScreenWidth() / 1.6f) / 2,
                                    Raylib.GetScreenHeight() - Convert.ToInt32(Raylib.GetScreenHeight() / 5.3f) - TextBoxPositionYOffset],
            };
            Scale = [Convert.ToInt32(Raylib.GetScreenWidth() / 1.6f), Convert.ToInt32(Raylib.GetScreenWidth() / 5.3f)];
            Box = new Rectangle(XPosition, YPosition, XScale, YScale);
            //
            Color = textBoxBackground;
            BorderColor = textBoxBorder;
            //
            CurrentFont = theFont;
            //
            CharacterWidth = (CurrentFont.BaseSize + CurrentFont.GlyphPadding) / 2;
            CharacterHeigth = CurrentFont.BaseSize + CurrentFont.GlyphPadding;
            MaximumCharacterCount = (int)(Box.Width - 2 * HorizontalTextMargin - CharacterWidth) / CharacterWidth;
            MaximumRowCount = (int)((Box.Height - VerticalTextMargin) / CharacterHeigth);
            //Reference variables and fit the string to the textbox
            if (TextCollectionIndex < TextCollectionCount)
            {
                Content[TextCollectionIndex] = ReferenceVariables(Content[TextCollectionIndex]);
                Content[TextCollectionIndex] = FitLoadedStringToTextBox(Content[TextCollectionIndex]);
                CurrentLoadedData = Content[TextCollectionIndex];
            }
            else
            {
                CurrentLoadedData = String.Empty;
            }
            //
            TextCount = CurrentLoadedData.Length;
            //
            Raylib.SetTextLineSpacing(CharacterHeigth);
            //
            Game = game;
            //
            ToggleEnability(); //Disabled by default
        }

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
            //
            int usedRows = 1;
            //
            while (IsFinished is false)
            {
                int nextSplitIndex = (MaximumCharacterCount - 1) * usedRows;
                string nextString = splittingText.Remove(0, nextSplitIndex);
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
        /// Reference variables in the string data.
        /// </summary>
        /// <param name="data">Concurrent line</param>
        /// <returns></returns>
        string ReferenceVariables(string data)
        {
            var variables = Game.VariableList;
            if (variables.Count == 0) return data;
            foreach (var variable in variables)
            {
                if (data.Contains($"[{variable.Name}]")) data = data.Replace($"[{variable.Name}]", variable.Value);
            }
            return data;
        }

        /// <summary>
        /// Wraps a singular instance of textoverflow.
        /// </summary>
        /// <param name="wrappingLine">The line which'll proceed to be wrapped.</param>
        /// <param name="rowNumber">The number of row, that line is in.</param>
        /// <returns></returns>
        private string WrapLine(string wrappingLine, int rowNumber)
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
        /// Reset the textbox to its initial state.
        /// </summary>
        private void ResetTextBox()
        {
            //reset collection index
            TextCollectionIndex = 0;
            //update the current loaded data
            CurrentLoadedData = Content[TextCollectionIndex];
            //reset text index
            TextIndex = 0;
            //update the text count
            TextCount = CurrentLoadedData.Length;
            //nullify the output
            Output = String.Empty;
            //Reseting second timer and blinking cursor timer
            SecondTimer.ResetTimer();
            BlinkingCursorTimer.ResetTimer();
            // disable textbatch
            TextBatchDone = false;
        }

        /// <summary>
        /// Cycle to the next unprocessed content data, and process it.
        /// </summary>
        private void ToggleNextTextBatch()
        {
            IncrementTextDataIndex();
            //
            if (TextCollectionIndex >= TextCollectionCount)
            {
                Game.ActiveScene.Timeline.NextStep();
                ToggleEnability();
                ResetTextBox();
                return;
            }
            ;
            //Reference variables and fit the string to the textbox
            if (Content[TextCollectionIndex].Contains("$[") is false) Content[TextCollectionIndex] = ReferenceVariables(Content[TextCollectionIndex]);
            //Only wrap if its not already wrapped
            if (Content[TextCollectionIndex].Contains('\n') is false) Content[TextCollectionIndex] = FitLoadedStringToTextBox(Content[TextCollectionIndex]);
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
        /// Toggle the enability of the textbox.
        /// </summary>
        internal void ToggleEnability() => IsEnabled = !IsEnabled;
        /// <summary>
        /// Check if the textbox is finished.
        /// </summary>
        /// <returns></returns>
        internal bool IsFinished() => TextIndex == TextCount;
        /// <summary>
        /// Create textbox for string data with a header.
        /// Custom color for textbox background and border.
        /// </summary>
        /// <param name="game">The game instance</param>
        /// <param name="characterPerSecond">Characters per second</param>
        /// <param name="activeFont">The font which the textbox will use</param>
        /// <param name="TextBoxcolor">Background color of the textbox.</param>
        /// <param name="TextBoxBorder">Border color of the textbox.</param>
        /// <param name="textBoxPosition">The position of the textbox</param>
        /// <param name="textMarginHorizontal">The horizontal margin of the text.</param>
        /// <param name="textMarginVertical">The vertical margin of the text.</param>
        /// <param name="wordWrap">Should wrap the entire word when initiating a new line?</param>
        /// <param name="textBoxTitle">The header of the textbox.</param>
        /// <param name="textBoxContent">text data</param>
        /// <returns></returns>
        public static TextBox CreateNewTextBox(
            Game game,
            double characterPerSecond,
            Font activeFont,
            Color TextBoxcolor,
            Color TextBoxBorder,
            PositionType textBoxPosition,
            int textMarginHorizontal,
            int textMarginVertical,
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
            textBoxPosition,
            textMarginHorizontal,
            textMarginVertical,
            wordWrap,
            game);

        public void Render()
        {
            //Return if disabled or finished. (render nothing)
            if (isTurnedOff() || isDone()) return;
            if (headerExists())
            {
                //header textbox frame
                Raylib.DrawRectangle(XPosition, YPosition - CharacterHeigth - VerticalTextMargin, Title.Length * CharacterWidth + 2 * HorizontalTextMargin + CurrentFont.GlyphPadding, CharacterHeigth, Color);
                Raylib.DrawRectangleLines((int)Box.Position.X, YPosition - CharacterHeigth - VerticalTextMargin, Title.Length * CharacterWidth + 2 * HorizontalTextMargin + CurrentFont.GlyphPadding, CharacterHeigth, BorderColor);
                //Headertext
                Raylib.DrawTextEx(CurrentFont, SanatizedHeader, new Vector2(XPosition + HorizontalTextMargin, YPosition - CharacterHeigth - VerticalTextMargin), CurrentFont.BaseSize, CurrentFont.GlyphPadding, Color.White);
            }
            //Textbox and Border
            Raylib.DrawRectangle((int)Box.Position.X, (int)Box.Position.Y, (int)Box.Width, (int)Box.Height, Color);
            Raylib.DrawRectangleLines((int)Box.Position.X, (int)Box.Position.Y, (int)Box.Width, (int)Box.Height, BorderColor);
            //draw current string data to screen.
            Raylib.DrawTextEx(CurrentFont, SanatizedOutput, new Vector2(XPosition + HorizontalTextMargin, YPosition + VerticalTextMargin),
                CurrentFont.BaseSize,
                CurrentFont.GlyphPadding,
                Color.White);
            if (shouldProceedNextBatch()) { ToggleNextTextBatch(); return; }
            if (IsFinished() is true)
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
                    Raylib.DrawTextEx(CurrentFont, "I", new Vector2(XPosition + Box.Width - CharacterWidth - HorizontalTextMargin, YPosition + Box.Height - CharacterHeigth - VerticalTextMargin),
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
            bool isTurnedOff() => IsEnabled is false;
            bool isDone() => TextCollectionIndex >= TextCollectionCount;
            bool headerExists() => Title.Length > 0;
            bool isSkipBatch() => Game.IsLeftMouseButtonPressed();
            bool shouldProceedNextBatch() =>
                TextCollectionIndex < TextCollectionCount && IsFinished() is true && Game.IsLeftMouseButtonPressed();
        }

        public bool Enabled()
        {
            throw new NotImplementedException();
        }
    }
}
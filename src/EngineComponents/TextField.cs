using System.Numerics;
using System.Text.RegularExpressions;
using EngineComponents.Interfaces;
using Raylib_cs;

namespace EngineComponents
{
    /// <summary>
    /// Represents a text field that can be rendered on the screen.
    /// A text field can display a single instance of text.
    /// No limit to the row count.
    /// </summary>
    public class TextField : IPermanentRenderingObject
    {
        /// <summary>
        /// The position of the text field according to the parenting block.
        /// </summary>
        internal int XPosition { get; set; }
        /// <summary>
        /// The position of the text field according to the parenting block.
        /// </summary>
        internal int YPosition { get; set; }
        /// <summary>
        /// The width of the text field.
        /// </summary>
        internal int Width { get; set; }
        /// <summary>
        /// The height of the text field.
        /// </summary>
        internal int Height { get; set; }
        /// <summary>
        /// The horizontal margin of the text.
        /// </summary>
        internal int HorizontalTextMargin { get; set; }
        /// <summary>
        /// The vertical margin of the text.
        /// </summary>
        internal int VerticalTextMargin { get; set; }
        /// <summary>
        /// The width of a single character.
        /// </summary>
        internal int CharacterWidth { get; set; }
        /// <summary>
        /// The height of a single character.
        /// </summary>
        internal int CharacterHeight { get; set; }
        /// <summary>
        /// The text data.
        /// Can be nullable.
        /// </summary>
        internal string? Text { get; set; }
        /// <summary>
        /// The visibility of the text field.
        /// </summary>
        internal bool IsVisible { get; set; }
        /// <summary>
        /// Should wrap the text if it exceeds the width of the text field.
        /// </summary>
        internal bool WordWrap { get; set; }
        /// <summary>
        /// The font of the text field.
        /// </summary>
        internal Font Font { get; set; }
        /// <summary>
        /// The color of the text.
        /// </summary>
        private Color TextColor { get; set; }
        /// <summary>
        /// The maximum character count that can be displayed in a single row.
        /// </summary>
        private int MaximumCharacterCount { get; set; }
        /// <summary>
        /// Creates a new instance of the TextField class.
        /// </summary>
        /// <param name="block">Parent block</param>
        /// <param name="width">Width of the Text field</param>
        /// <param name="height">Height of the Text field</param>
        /// <param name="horizontalTextMargin">The horizontal margin of the text.</param>
        /// <param name="verticalTextMargin">The vertical margin of the text.</param>
        /// <param name="text">The string data.</param>
        /// <param name="font">Font of the textdata</param>
        /// <param name="textColor">The color of the text.</param>
        public TextField(Block block, int width, int height, int horizontalTextMargin, int verticalTextMargin, string text, Font font, Color textColor)
        {
            XPosition = block.XPosition;
            YPosition = block.YPosition;
            Width = width;
            Height = height;
            HorizontalTextMargin = horizontalTextMargin;
            VerticalTextMargin = verticalTextMargin;
            Text = text;
            Font = font;
            CharacterWidth = (Font.BaseSize + Font.GlyphPadding) / 2;
            CharacterHeight = Font.BaseSize;
            TextColor = textColor;
            IsVisible = true;
            MaximumCharacterCount = (int)(Width - 2 * HorizontalTextMargin - CharacterWidth) / CharacterWidth;
            FormatText(Text);
        }
        /// <summary>
        /// Formats the text to fit the TextField width wise.
        /// </summary>
        /// <param name="text"></param>
        private void FormatText(string text)
        {
            if (Text is null) return;
            if (Text.Length < MaximumCharacterCount) return;
            //
            bool IsFinished = false;
            string splittingText = Text;
            // Due to string linearity, multiply the splitting count
            int splitCount = 1;
            //
            while (IsFinished is false)
            {
                int nextSplitIndex = (MaximumCharacterCount - 1) * splitCount;
                //
                while (IsFinished is false)
                {
                    int nextStringSplitIndex = (MaximumCharacterCount - 1) * splitCount;
                    string nextString = splittingText.Remove(0, nextSplitIndex);
                    //
                    if (WordWrap) splittingText = WrapLine(nextString);
                    else splittingText = nextString;
                    if (nextString.Length <= MaximumCharacterCount) IsFinished = true;
                    else splitCount++;
                }
            }
            string WrapLine(string wrappingLine)
            {
                int newLineIndex = (MaximumCharacterCount - 1) * splitCount;
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
        }
        /// <summary>
        /// Renders the text field on the screen.
        /// </summary>
        public void Render()
        {
            if (IsVisible is false) return;
            if (Text is null) return;
            Raylib.DrawTextEx(Font, SanatizedText(), new Vector2(XPosition + HorizontalTextMargin, YPosition + VerticalTextMargin), Font.BaseSize, 1, TextColor);
        }
        /// <summary>
        /// if visibile, returns true, otherwise false
        /// </summary>
        public bool Enabled() => IsVisible;
        /// <summary>
        /// Filters escape any escape characters except for the newline.
        /// </summary>
        /// <returns></returns>
        internal string SanatizedText() => Regex.Replace(Text, @"\\(?!n)", "");
    }
}
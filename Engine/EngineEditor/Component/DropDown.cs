using Raylib_cs;
using VisualNovelEngine.Engine.EngineEditor.Interface;

namespace VisualNovelEngine.Engine.EngineEditor.Component
{
    public class DropDown : IComponent
    {
        private Editor Editor { get; set; }
        public int XPosition { get; set; }
        public int YPosition { get; set; }
        internal int Width { get; set; }
        internal int Height { get; set; }
        internal int BorderWidth { get; set; }
        internal string? Text { get; set; }
        internal bool IsHover { get; set; }
        internal bool IsSelected { get; set; } = false;
        internal Button? Button { get; set; }
        internal List<Button>? ButtonList { get; set; } = [];
        private Color Color { get; set; }
        private Color BorderColor { get; set; }
        private Color HoverColor { get; set; }

        public DropDown(Editor editor, int xPosition, int yPosition, int width, int height, int borderWidth, List<Button> buttonList)
        {
            Editor = editor;
            XPosition = xPosition;
            YPosition = yPosition;
            Width = width;
            Height = height;
            BorderWidth = borderWidth;
            Color = Editor.BaseColor;
            BorderColor = Editor.BorderColor;
            HoverColor = Editor.HoverColor;
            ButtonList = buttonList;
            Button = ButtonList[0];
            Text = Button.Text;
            UpdateComponent();
        }

        internal void AddButton(Button button)
        {
            ButtonList.Add(button);
        }
        private void UpdateComponent()
        {
            for (int i = 0; i < ButtonList.Count; i++)
            {
                ButtonList[i].XPosition = XPosition;
                ButtonList[i].YPosition = YPosition + ((i + 1) * Height);
            }
        }
        public void Update()
        {
            IsHover = Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), new Rectangle(XPosition, YPosition, Width, Height));
            if (IsSelected && Raylib.IsMouseButtonPressed(MouseButton.Left)) IsSelected = false;
            else if (IsSelected is false && Raylib.IsMouseButtonPressed(MouseButton.Left)) IsSelected = true;
            if (IsSelected)
            {
                foreach (Button button in ButtonList)
                {
                    button.Update();
                }
            }
        }
        public void Render()
        {
            Update();
            if (IsHover)
            {
                Raylib.DrawRectangle(XPosition, YPosition, Width, Height, HoverColor);
            }
            else
            {
                Raylib.DrawRectangle(XPosition, YPosition, Width, Height, Color);
            }
            Raylib.DrawRectangleLinesEx(new Rectangle(XPosition, YPosition, Width, Height), BorderWidth, BorderColor);
            Raylib.DrawText(Text, XPosition + 5, YPosition + 5, 20, Raylib_cs.Color.White);
        }
    }
}
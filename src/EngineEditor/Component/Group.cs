using EngineEditor.Interface;
using Raylib_cs;

namespace EngineEditor.Component
{
    public enum GroupType
    {
        SolidColor,
        ClearBackground
    }

    public class Group : IWindow
    {
        internal int XPosition { get; set; }
        internal int YPosition { get; set; }
        internal int Width { get; set; }
        internal int Height { get; set; }
        internal int BorderWidth { get; set; }
        List<IComponent> ComponentList { get; set; }
        GroupType BackgroundType { get; set; }
        private Color Color { get; set; }
        private Color BorderColor { get; set; }
        private Color HoverColor { get; set; }
        private const int Padding = 10;

        public Group(int xPosition, int yPosition, int width, int height, int borderWidth, Color color, Color borderColor, Color hoverColor, GroupType groupType, ITool[] components)
        {
            XPosition = xPosition;
            YPosition = yPosition;
            if (components.Length > 0) ComponentList = [.. (IEnumerable<IComponent>)components]; else ComponentList = [];
            Width = width;
            Height = height;
            BorderWidth = borderWidth;
            Color = color;
            BorderColor = borderColor;
            HoverColor = hoverColor;
            BackgroundType = groupType;
            UpdateComponentPosition();
        }

        public Group(int xPosition, int yPosition, int width, int height, int borderWidth, Color color, Color borderColor, Color hoverColor, IDynamicComponent[] components)
        {
            XPosition = xPosition;
            YPosition = yPosition;
            ComponentList = [.. (IEnumerable<IComponent>)components];
            Width = width;
            Height = height;
            BorderWidth = borderWidth;
            Color = color;
            BorderColor = borderColor;
            HoverColor = hoverColor;
            BackgroundType = GroupType.SolidColor;
            UpdateComponentPosition();
        }

        public void Update()
        {

        }

        private void UpdateComponentPosition()
        {
            for (int i = 0; i < ComponentList.Count; i++)
            {
                ComponentList[i].XPosition = XPosition + Padding;
                ComponentList[i].YPosition = YPosition;
            }
        }
        internal void AddComponent(IDynamicComponent component)
        {
            ComponentList.Add((IComponent)component);
            UpdateComponentPosition();
        }

        public void Show()
        {
            Update();
            Raylib.DrawRectangle(XPosition, YPosition, Width, Height, Color);
            Raylib.DrawRectangleLines(XPosition, YPosition, Width, Height, BorderColor);
            ComponentList.ForEach(component => component.Render());
        }

        public void Hide()
        {

        }
    }
}
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
        const int Padding = 10;
        const int MaximumHorizontalComponentCount = 4;
        public int ComponentCount => ComponentList.Count;

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
        /// <summary>
        /// Updates the position of the components inside the group.
        /// Dynamically changes the position of the components based on the amount of components in the group.
        /// </summary>
        private void UpdateComponentPosition()
        {
            int rowcount = 0;
            for (int i = 0; i < ComponentList.Count; i++)
            {
                if (i % MaximumHorizontalComponentCount == 0) rowcount++;
                ComponentList[i].XPosition = rowcount > 1 ? ComponentList[i % MaximumHorizontalComponentCount].XPosition : (i + 1) * (Padding + XPosition);
                ComponentList[i].YPosition = rowcount * (YPosition + Padding);
            }
            if (ComponentList.Count < 1) return;
            Width = rowcount > 1 ? Width : (XPosition * ComponentList.Count) + 2 * Padding;
            Height = YPosition * rowcount + Padding;
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
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
        private Color SelectedColor { get; set; }
        internal bool IsActive { get; set; } = true;
        private bool IsSelected { get; set; } = false;
        private bool IsHover { get; set; } = false;
        private bool IsStatic { get; set; } = false;
        const int Padding = 10;
        internal int MaximumHorizontalComponentCount = 4;
        public int ComponentCount => ComponentList.Count;
        private Button ButtonDependency { get; set; }

        public Group(int xPosition, int yPosition, int width, int height, int borderWidth, Color color, Color borderColor, Color hoverColor, GroupType groupType, int maximumHorizontalElements, ITool[] components)
        {
            XPosition = xPosition;
            YPosition = yPosition;
            if (components.Length > 0)
            {
                List<IComponent> componentList = [];
                for (int i = 0; i < components.Length; i++)
                {
                    componentList.Add((IComponent)components[i]);
                }
                ComponentList = componentList;
            }
            else ComponentList = [];
            Width = width;
            Height = height;
            BorderWidth = borderWidth;
            Color = color;
            BorderColor = borderColor;
            SelectedColor = hoverColor;
            BackgroundType = groupType;
            MaximumHorizontalComponentCount = maximumHorizontalElements;
            IsStatic = true;
            UpdateComponentPosition();
        }

        public Group(int xPosition, int yPosition, int width, int height, int borderWidth, Color color, Color borderColor, Color hoverColor, IDinamicComponent[] components)
        {
            XPosition = xPosition;
            YPosition = yPosition;
            ComponentList = [.. (IEnumerable<IComponent>)components];
            Width = width;
            Height = height;
            BorderWidth = borderWidth;
            Color = color;
            BorderColor = borderColor;
            SelectedColor = hoverColor;
            BackgroundType = GroupType.SolidColor;
            UpdateComponentPosition();
        }
        /// <summary>
        /// Updates the position of the components inside the group.
        /// Dynamically changes the position of the components based on the amount of components in the group.
        /// </summary>
        internal void UpdateComponentPosition()
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

        internal void SelectButtonDependency(Button button)
        {
            ButtonDependency = button;
        }
        /// <summary>
        /// Adds a component to the group.
        /// Only accepts dinamic components.
        /// </summary>
        /// <param name="component"></param>
        internal void AddComponent(IDinamicComponent component)
        {
            ComponentList.Add((IComponent)component);
            UpdateComponentPosition();
        }
        /// <summary>
        /// Adds multiple components to the group.
        /// </summary>
        /// <param name="components"></param>
        internal void AddComponents(IDinamicComponent[] components)
        {
            ComponentList.AddRange((IEnumerable<IComponent>)components);
            UpdateComponentPosition();
        }
        /// <summary>
        /// Removes a component from the group.
        /// </summary>
        /// <param name="component"></param>
        internal void RemoveComponent(IDinamicComponent component)
        {
            ComponentList.Remove((IComponent)component);
            UpdateComponentPosition();
        }
        internal void Update()
        {
            switch (IsActive)
            {
                case true:
                    Show();
                    if (IsStatic is false) Move();
                    break;
                case false:
                    Hide();
                    break;
            }
            if (ButtonDependency is null) return;
            IsActive = ButtonDependency.Selected;
        }
        public void Move()
        {
            IsHover = Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), new Rectangle(XPosition, YPosition, Width, Height));
            if (Raylib.IsMouseButtonPressed(MouseButton.Left) && IsHover)
            {
                IsSelected = true;
            }
            else if (IsSelected && IsHover is false && Raylib.IsMouseButtonReleased(MouseButton.Left))
            {
                IsSelected = false;
                UpdateComponentPosition();
            }
            if (IsSelected)
            {
                XPosition = Raylib.GetMouseX();
                YPosition = Raylib.GetMouseY();
            }
        }
        public void Show()
        {
            Raylib.DrawRectangle(XPosition, YPosition, Width, Height, IsSelected ? SelectedColor : Color);
            Raylib.DrawRectangleLines(XPosition, YPosition, Width, Height, BorderColor);
            if (ComponentList.Count < 1) return;
            for (int i = 0; i < ComponentList.Count; i++)
            {
                IComponent? component = ComponentList[i];
                component.Render();
            }
        }
        public void Hide()
        {

        }
    }
}
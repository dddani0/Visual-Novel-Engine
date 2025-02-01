using VisualNovelEngine.Engine.EngineEditor.Interface;
using Raylib_cs;

namespace VisualNovelEngine.Engine.EngineEditor.Component
{
    public enum GroupType
    {
        SolidColor,
        ClearBackground
    }

    public class Group : IWindow
    {
        Editor Editor { get; set; }
        internal int XPosition { get; set; }
        internal int YPosition { get; set; }
        internal int Width { get; set; }
        internal int Height { get; set; }
        internal int BorderWidth { get; set; }
        internal List<IComponent> ComponentList { get; set; }
        GroupType BackgroundType { get; set; }
        private Color Color { get; set; }
        private Color BorderColor { get; set; }
        private Color HoverColor { get; set; }
        internal bool IsActive { get; set; } = true;
        private bool IsSelected { get; set; } = false;
        private bool IsHover { get; set; } = false;
        private bool IsStatic { get; set; } = false;
        const int Padding = 5;
        internal int MaximumHorizontalComponentCount = 4;
        public int ComponentCount => ComponentList.Count;
        private Button ButtonDependency { get; set; }

        public Group(Editor editor, int xPosition, int yPosition, int width, int height, int borderWidth, Color color, Color borderColor, Color hoverColor, GroupType groupType, int maximumHorizontalElements, ITool[] components)
        {
            Editor = editor;
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
            HoverColor = hoverColor;
            BackgroundType = groupType;
            MaximumHorizontalComponentCount = maximumHorizontalElements;
            IsStatic = true;
            UpdateComponentPosition();
        }

        public Group(Editor editor, int xPosition, int yPosition, int width, int height, int borderWidth, Color color, Color borderColor, Color hoverColor, IDinamicComponent[] components)
        {
            Editor = editor;
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
                ComponentList[i].XPosition = rowcount > 1 ? Width : Padding + XPosition + (i * (Padding + Editor.ComponentWidth));
                ComponentList[i].YPosition = YPosition + Padding + ((rowcount - 1) * (Editor.ComponentHeight + Padding));
            }
            if (ComponentList.Count < 1) return;
            Width = rowcount > 1 ? Width : (Editor.ComponentWidth * ComponentList.Count) + (ComponentList.Count * 2 * BorderWidth) + (2 * Padding);
            Height = Editor.ComponentHeight * rowcount + (2 * Padding);
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
            if (IsActive is false) return;
            if (IsStatic is false) Move();
            Show();
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
            Raylib.DrawRectangle(XPosition, YPosition, Width, Height, IsSelected ? HoverColor : Color);
            Raylib.DrawRectangleLines(XPosition, YPosition, Width, Height, BorderColor);
            if (ComponentList.Count < 1) return;
            for (int i = 0; i < ComponentList.Count; i++)
            {
                IComponent? component = ComponentList[i];
                component.Render();
            }
        }
    }
}
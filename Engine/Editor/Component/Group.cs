using VisualNovelEngine.Engine.Editor.Interface;
using Raylib_cs;

namespace VisualNovelEngine.Engine.Editor.Component
{
    /// <summary>
    /// Represents the types which the group can have.
    /// </summary>
    public enum GroupType
    {
        SolidColor,
        ClearBackground
    }
    /// <summary>
    /// Represents a group of components.
    /// </summary>
    public class Group : IWindow
    {
        /// <summary>
        /// Represents the editor.
        /// </summary>
        Editor Editor { get; set; }
        /// <summary>
        /// The x position of the group.
        /// </summary>
        internal int XPosition { get; set; }
        /// <summary>
        /// The y position of the group.
        /// </summary>
        internal int YPosition { get; set; }
        /// <summary>
        /// The width of the group.
        /// </summary>
        internal int Width { get; set; }
        /// <summary>
        /// The height of the group.
        /// </summary>
        internal int Height { get; set; }
        /// <summary>
        /// The border width of the group.
        /// </summary>
        internal int BorderWidth { get; set; }
        /// <summary>
        /// The list of components in the group.
        /// </summary>
        internal List<IComponent> ComponentList { get; set; }
        /// <summary>
        /// The type of background which the group has.
        /// </summary>
        GroupType BackgroundType { get; set; }
        /// <summary>
        /// The color of the group.
        /// </summary>
        private Color Color { get; set; }
        /// <summary>
        /// The border color of the group.
        /// </summary>
        private Color BorderColor { get; set; }
        /// <summary>
        /// The hover color of the group.
        /// </summary>
        private Color HoverColor { get; set; }
        /// <summary>
        /// Represents if the group is active.
        /// </summary>
        internal bool IsActive { get; set; } = true;
        /// <summary>
        /// Represents if the group is selected.
        /// </summary>
        internal bool IsSelected { get; set; } = false;
        /// <summary>
        /// Represents if the group is hovered.
        /// </summary>
        private bool IsHover { get; set; } = false;
        /// <summary>
        /// Represents if the group is static.
        /// </summary>
        internal bool IsStatic { get; set; } = false;
        /// <summary>
        /// The padding of the group.
        /// </summary>
        const int Padding = 5;
        /// <summary>
        /// The maximum amount of horizontal components.
        /// </summary>
        internal int MaximumHorizontalComponentCount = 4;
        /// <summary>
        /// The button dependency.
        /// </summary>
        private Button ButtonDependency { get; set; }
        /// <summary>
        /// Creates a new group.
        /// </summary>
        /// <param name="editor"></param>
        /// <param name="xPosition"></param>
        /// <param name="yPosition"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="borderWidth"></param>
        /// <param name="color"></param>
        /// <param name="borderColor"></param>
        /// <param name="hoverColor"></param>
        /// <param name="groupType"></param>
        /// <param name="maximumHorizontalElements"></param>
        /// <param name="components"></param>
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
        /// <summary>
        /// Creates a new group.
        /// </summary>
        /// <param name="editor"></param>
        /// <param name="xPosition"></param>
        /// <param name="yPosition"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="borderWidth"></param>
        /// <param name="color"></param>
        /// <param name="borderColor"></param>
        /// <param name="hoverColor"></param>
        /// <param name="components"></param>
        public Group(Editor editor, int xPosition, int yPosition, int width, int height, int borderWidth, Color color, Color borderColor, Color hoverColor, IDinamicComponent[] components)
        {
            Editor = editor;
            XPosition = xPosition;
            YPosition = yPosition;
            ComponentList = [];
            foreach (IComponent component in components.Cast<IComponent>())
            {
                ComponentList.Add(component);
            }
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
            if (ComponentList.Count < 1) { Editor.ActiveScene.ComponentGroupList.Remove(this); return; }
            UpdateComponentPosition();
        }
        /// <summary>
        /// Removes multiple components from the group.
        /// </summary>
        internal void Update()
        {
            if (IsActive is false) return;
            if (IsStatic is false) Move();
            Show();
            if (ButtonDependency is null) return;
            IsActive = ButtonDependency.Selected;
        }
        /// <summary>
        /// Moves the group.
        /// </summary>
        public void Move()
        {
            if (Editor.Busy) return;
            if (IsStatic) return;
            if (ComponentList == null) return;
            if (ComponentList.Any(c => (c as Component).IsHover)) return;
            if (ComponentList.Any(c => (c as Component).IsSelected)) return;
            IsHover = Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), new Rectangle(XPosition, YPosition, Width, Height));
            if (Raylib.IsMouseButtonDown(MouseButton.Left) && IsHover) IsSelected = true;
            if (IsSelected && Raylib.IsMouseButtonReleased(MouseButton.Left))
            {
                IsSelected = false;
                UpdateComponentPosition();
            }
            if (IsSelected)
            {
                XPosition = Raylib.GetMouseX();
                YPosition = Raylib.GetMouseY() > (Editor.SceneBar.Height + Editor.Toolbar.Height) && Raylib.GetMouseY() < Editor.ActiveScene.Timeline.YPosition - Height ? Raylib.GetMouseY() : YPosition;
            }
        }
        /// <summary>
        /// Shows the group.
        /// </summary>
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
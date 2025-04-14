using VisualNovelEngine.Engine.Editor.Interface;
using Raylib_cs;
using VisualNovelEngine.Engine.Game.Component;
using VisualNovelEngine.Engine.Game.Interface;
using Timer = VisualNovelEngine.Engine.Game.Component.Timer;
using System.Text.RegularExpressions;
using VisualNovelEngine.Engine.Editor.Component.Command;
using System.Linq;

namespace VisualNovelEngine.Engine.Editor.Component
{
    /// <summary>
    /// Represents a component in the editor.
    /// </summary>
    public class Component : IComponent, IDinamicComponent
    {
        /// <summary>
        /// Represents the editor.
        /// </summary>
        private Editor Editor { get; set; }
        /// <summary>
        /// Unique identifier of the component.
        /// </summary>
        internal readonly int ID;
        /// <summary>
        /// The name of the component.
        /// </summary>
        internal string Name { get; set; }
        /// <summary>
        /// The x position of the component.
        /// </summary>
        public int XPosition { get; set; }
        /// <summary>
        /// The y position of the component.
        /// </summary>
        public int YPosition { get; set; }
        /// <summary>
        /// The width of the component.
        /// </summary>
        internal int Width { get; set; }
        /// <summary>
        /// The height of the component.
        /// </summary>
        internal int Height { get; set; }
        /// <summary>
        /// The x position of the close button.
        /// </summary>
        private int CloseButtonXPosition { get; set; }
        /// <summary>
        /// The x position of the inspector button.
        /// </summary>
        private int InspectorButtonXPosition { get; set; }
        /// <summary>
        /// The border width of the component.
        /// </summary>
        internal int BorderWidth { get; set; }
        /// <summary>
        /// The flag that indicates if the component is static.
        /// </summary>
        internal bool IsObjectStatic { get; set; }
        /// <summary>
        /// The flag that indicates if the component is selected.
        /// </summary>
        internal bool IsSelected { get; set; }
        /// <summary>
        /// The flag that indicates if the component is locked.
        /// </summary>
        internal bool IsLocked { get; set; }
        /// <summary>
        /// The flag that indicates if the component is hovered.
        /// </summary>
        internal bool IsHover { get; set; }
        /// <summary>
        /// The flag that indicates if the component is moving.
        /// </summary>
        internal bool IsMoving { get; set; } = false;
        /// <summary>
        /// The flag that indicates if the component is renaming.
        /// </summary>
        internal bool IsRenaming { get; set; } = false;
        /// <summary>
        /// The flag that indicates if the component is dragged outside the group.
        /// </summary>
        internal Button CloseButton { get; set; }
        /// <summary>
        /// The flag that indicates if the component is dragged outside the group.
        /// </summary>
        internal Button InspectorButton { get; set; }
        /// <summary>
        /// The color of the component.
        /// </summary>
        internal Color Color { get; set; }
        /// <summary>
        /// The border color of the component.
        /// </summary>
        internal Color BorderColor { get; set; }
        /// <summary>
        /// The selected color of the component.
        /// </summary>
        internal Color SelectedColor { get; set; }
        /// <summary>
        /// The hover color of the component.
        /// </summary>
        internal Color HoverColor { get; set; }
        /// <summary>
        /// The timer that controls the movement of the component.
        /// </summary>
        private Timer MoveTimer { get; set; }
        /// <summary>
        /// The group of the component.
        /// </summary>
        internal Group? Group { get; set; }
        /// <summary>
        /// The rendering object associated with the component.
        /// </summary>
        internal IRenderingObject? RenderingObject { get; set; }
        /// <summary>
        /// Creates a new instance of the component.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="editor"></param>
        /// <param name="group"></param>
        /// <param name="name"></param>
        /// <param name="xPosition"></param>
        /// <param name="yPosition"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="borderWidth"></param>
        /// <param name="color"></param>
        /// <param name="borderColor"></param>
        /// <param name="selectedColor"></param>
        /// <param name="hoverColor"></param>
        /// <param name="component"></param>
        public Component(int id, Editor editor, Group group, string name, int xPosition, int yPosition, int width, int height, int borderWidth, Color color, Color borderColor, Color selectedColor, Color hoverColor, IRenderingObject component)
        {
            Editor = editor;
            ID = id;
            Name = name;
            XPosition = xPosition;
            YPosition = yPosition;
            Width = width;
            Height = height;
            CloseButtonXPosition = XPosition + Width - Editor.SmallButtonWidth;
            InspectorButtonXPosition = XPosition + Width - 2 * Editor.SmallButtonWidth;
            BorderWidth = borderWidth;
            Color = color;
            BorderColor = borderColor;
            SelectedColor = selectedColor;
            HoverColor = hoverColor;
            Color closeButtonBaseColor = Editor.CloseButtonBaseColor;
            Color closeButtonBorderColor = Editor.CloseButtonBorderColor;
            Color closeButtonHoverColor = Editor.CloseButtonHoverColor;
            CloseButton = new Button(editor, CloseButtonXPosition, YPosition, "X", true, Editor.SmallButtonWidth, Editor.SmallButtonHeight, Editor.SmallButtonBorderWidth, closeButtonBaseColor, closeButtonBorderColor, closeButtonHoverColor, new DeleteComponentCommand(Editor, this), Button.ButtonType.Trigger);
            Color inspectorButtonBaseColor = Editor.InspectorButtonBaseColor;
            Color inspectorButtonBorderColor = Editor.InspectorButtonBorderColor;
            Color inspectorButtonHoverColor = Editor.InspectorButtonHoverColor;
            InspectorButton = new Button(editor, InspectorButtonXPosition, YPosition, "I", true, Editor.SmallButtonWidth, Editor.SmallButtonHeight, Editor.SmallButtonBorderWidth, inspectorButtonBaseColor, inspectorButtonBorderColor, inspectorButtonHoverColor, new ShowInspectorCommand(Editor, 1), Button.ButtonType.Trigger);
            MoveTimer = new Timer(0.1f);
            Group = group;
            RenderingObject = component;
        }
        /// <summary>
        /// Renders the component.
        /// </summary>
        public void Render()
        {
            Raylib.DrawRectangle(XPosition, YPosition, Width, Height, IsSelected ? SelectedColor : IsHover ? HoverColor : Color);
            Raylib.DrawRectangleLinesEx(new Rectangle(XPosition, YPosition, Width, Height), BorderWidth, BorderColor);
            Raylib.DrawText(Name, XPosition + 5, YPosition + 5, 12, Color.Black);
            Update();
        }
        /// <summary>
        /// Updates the component.
        /// </summary>
        public void Update()
        {
            if (IsLocked) return;
            CloseButtonXPosition = XPosition + Width - Editor.SmallButtonWidth;
            CloseButton.XPosition = CloseButtonXPosition;
            CloseButton.YPosition = YPosition;
            InspectorButtonXPosition = XPosition + Width - 2 * Editor.SmallButtonWidth;
            InspectorButton.XPosition = InspectorButtonXPosition;
            InspectorButton.YPosition = YPosition;
            IsHover = Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), new Rectangle(XPosition, YPosition, Width, Height));
            if (IsHover && Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                //Convert IComponents to Components and if any other components are selected, then return
                if (!Editor.ActiveScene.ComponentList.Cast<Component>().Any(component => component.IsSelected)) IsSelected = true;
            }
            else if (IsSelected && IsHover is false && Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                IsSelected = false;
                IsRenaming = false;
                MoveTimer.Reset();
            }
            if (IsSelected) //Selected shows buttons
            {
                if (Raylib.IsMouseButtonPressed(MouseButton.Right)) IsRenaming = true;
                Move();
                CloseButton.Render();
                InspectorButton.Render();
            }
            if (IsRenaming)
            {
                if (VisualNovelEngine.Engine.Game.Component.Game.IsKeyPressed(KeyboardKey.Backspace))
                {
                    if (Name.Length > 0)
                    {
                        Name = Name.Remove(Name.Length - 1);
                    }
                }
                else if (VisualNovelEngine.Engine.Game.Component.Game.IsKeyPressed(KeyboardKey.Enter))
                {
                    IsSelected = false;
                }
                else if (Raylib.GetKeyPressed() > 0)
                {
                    Name = Regex.Unescape(Name + ((char)Raylib.GetCharPressed()).ToString()).Replace('\0', ' ');
                }
            }
            if (IsInGroup()) return;
            //check if overlaps any other components
            foreach (Component component in Editor.ActiveScene.ComponentList.Cast<Component>())
            {
                if (component == this) continue;
                if (component.IsInGroup() && component.Group.IsSelected) continue;
                if (Raylib.CheckCollisionRecs(new Rectangle(XPosition, YPosition, Width, Height), new Rectangle(component.XPosition, component.YPosition, component.Width, component.Height)))
                {
                    switch (component.IsInGroup())
                    {
                        case true:
                            component.Group.AddComponent(this);
                            Group = component.Group;
                            break;
                        case false:
                            //create a group
                            Group = new Group(Editor, component.XPosition, component.YPosition, component.Width, component.Height, Editor.ComponentBorderWidth, Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, [this as IDinamicComponent, component as IDinamicComponent]);
                            //
                            Editor.ActiveScene.ComponentGroupList.Add(Group);
                            break;
                    }
                }
            }
        }
        /// <summary>
        /// Moves the component.
        /// </summary>
        public void Move()
        {
            if (IsRenaming is true) return;
            //If holding down the left mouse button, move the component.
            if (MoveTimer.OnCooldown())
            {
                MoveTimer.DecreaseTimer();
                return;
            }
            if (Raylib.IsMouseButtonDown(MouseButton.Left))
            {
                if (IsHover is false)
                {
                    IsMoving = true;
                    XPosition = Raylib.GetMouseX();
                    YPosition = Raylib.GetMouseY() > (Editor.SceneBar.Height + Editor.Toolbar.Height) && Raylib.GetMouseY() < Editor.ActiveScene.Timeline.YPosition - Editor.ComponentHeight ? Raylib.GetMouseY() : YPosition;
                }
            }
            //Create a group for objects.
            if (IsInGroup() is false)
            {
                if (IsMoving is false) return;
                foreach (var group in Editor.ActiveScene.ComponentGroupList)
                {
                    if (Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), new Rectangle(group.XPosition, group.YPosition, group.Width, group.Height)) && Raylib.IsMouseButtonReleased(MouseButton.Left))
                    {
                        Group = group;
                        Group.AddComponent(this);
                        Editor.ActiveScene.ComponentList.Remove(this);
                        IsMoving = false;
                        break;
                    }
                }
            }
            else
            {
                if (Raylib.IsMouseButtonReleased(MouseButton.Left) && IsDraggedOutsideGroup())
                {
                    Group.RemoveComponent(this);
                    if (Group.ComponentList.Count == 0) Editor.ActiveScene.ComponentGroupList.Remove(Group);
                    Group = null;
                    Editor.ActiveScene.ComponentList.Add(this);
                    IsMoving = false;
                }
                else if (Raylib.IsMouseButtonReleased(MouseButton.Left) && IsDraggedOutsideGroup() is false)
                {
                    Group.UpdateComponentPosition();
                    IsMoving = false;
                }
            }
            return;
            bool IsDraggedOutsideGroup() => Group is not null && (XPosition < Group.XPosition || XPosition > Group.XPosition + Group.Width || YPosition < Group.YPosition || YPosition > Group.YPosition + Group.Height);
        }
        /// <summary>
        /// Checks if the component is in a group.
        /// </summary>
        /// <returns></returns>
        public bool IsInGroup() => Group != null;
    }
}
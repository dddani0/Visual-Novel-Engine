using EngineEditor.Interface;
using Raylib_cs;
using TemplateGame.Component;
using TemplateGame.Interface;
using Timer = TemplateGame.Component.Timer;

namespace EngineEditor.Component
{
    /// <summary>
    /// Represents a component.
    /// </summary>
    public class Component : IComponent, IDynamicComponent
    {
        internal string Name { get; set; }
        public int XPosition { get; set; }
        public int YPosition { get; set; }
        internal int Width { get; set; }
        internal int Height { get; set; }
        internal int BorderWidth { get; set; }
        internal IPermanentRenderingObject? RenderingObject { get; set; }
        //Icon
        internal bool IsSelected { get; set; }
        internal bool IsLocked { get; set; }
        internal bool IsHover { get; set; }
        internal bool IsMoving { get; set; } = false;
        internal Button CloseButton { get; set; }
        internal Button InspectorButton { get; set; }
        internal Color Color { get; set; }
        internal Color BorderColor { get; set; }
        internal Color SelectedColor { get; set; }
        internal Color HoverColor { get; set; }
        private Editor Editor { get; set; }
        private Timer MoveTimer { get; set; }
        private Group? Group { get; set; }

        public Component(Editor editor, Group group, string name, int xPosition, int yPosition, int width, int height, int borderWidth, Color color, Color borderColor, Color selectedColor, Color hoverColor)
        {
            Name = name;
            XPosition = xPosition;
            YPosition = yPosition;
            Width = width;
            Height = height;
            BorderWidth = borderWidth;
            Color = color;
            BorderColor = borderColor;
            SelectedColor = selectedColor;
            HoverColor = hoverColor;
            CloseButton = new Button(editor, XPosition + Width - BorderWidth, YPosition + 10, "X", 20, 20, 1, Color.Red, Color.Black, Color.Red, null);
            InspectorButton = new Button(editor, XPosition + Width - BorderWidth - CloseButton.Width - CloseButton.BorderWidth, YPosition + 10, "I", 20, 20, 1, Color.Blue, Color.Black, Color.DarkBlue, null);
            MoveTimer = new Timer(0.1f);
            Group = group;
            Editor = editor;
        }

        public void Render()
        {
            Raylib.DrawRectangle(XPosition, YPosition, Width, Height, IsSelected ? SelectedColor : IsHover ? HoverColor : Color);
            Raylib.DrawRectangleLinesEx(new Rectangle(XPosition, YPosition, Width, Height), BorderWidth, BorderColor);
            Raylib.DrawText(Name, XPosition + 5, YPosition + 5, 12, Color.Black);
            Update();
        }

        public void Update()
        {
            IsHover = Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), new Rectangle(XPosition, YPosition, Width, Height));
            if (IsHover && Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                IsSelected = true;
            }
            else if (IsSelected && IsHover is false && Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                IsSelected = false;
                MoveTimer.ResetTimer();
            }
            if (IsSelected) //Selected shows buttons
            {
                Move();
                CloseButton.Render();
                InspectorButton.Render();
            }
        }

        public void Move()
        {
            //If holding down the left mouse button, move the component.
            if (MoveTimer.OnCooldown())
            {
                MoveTimer.DecreaseTimer();
                return;
            }
            if (Raylib.IsMouseButtonDown(MouseButton.Left))
            {
                IsMoving = true;
                XPosition = Raylib.GetMouseX();
                YPosition = Raylib.GetMouseY();
                CloseButton.XPosition = XPosition + Width - BorderWidth;
                CloseButton.YPosition = YPosition;
                InspectorButton.XPosition = XPosition + Width - BorderWidth - CloseButton.Width - CloseButton.BorderWidth;
                InspectorButton.YPosition = YPosition;
            }
            //Detach this component if it's is dragged outside the group.
            if (Group is null)
            {
                if (IsMoving is false) return;
                foreach (var group in Editor.ComponentGroupList)
                {
                    if (Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), new Rectangle(group.XPosition, group.YPosition, group.Width, group.Height)) && Raylib.IsMouseButtonReleased(MouseButton.Left))
                    {
                        Group = group;
                        Group.AddComponent(this);
                        Editor.ComponentList.Remove(this);
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
                    Group = null;
                    Editor.ComponentList.Add(this);
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

        public bool IsInGroup() => Group != null;
    }
}
using VisualNovelEngine.Engine.EngineEditor.Interface;
using Raylib_cs;
using TemplateGame.Component;
using TemplateGame.Interface;
using Timer = TemplateGame.Component.Timer;
using System.Text.RegularExpressions;
using VisualNovelEngine.Engine.EngineEditor.Component.Command;

namespace VisualNovelEngine.Engine.EngineEditor.Component
{
    /// <summary>
    /// Represents a component.
    /// </summary>
    public class Component : IComponent, IDinamicComponent
    {
        internal readonly int ID;
        internal string Name { get; set; }
        public int XPosition { get; set; }
        public int YPosition { get; set; }
        internal int Width { get; set; }
        internal int Height { get; set; }
        private int CloseButtonXPosition { get; set; }
        private int InspectorButtonXPosition { get; set; }
        internal int BorderWidth { get; set; }
        internal IPermanentRenderingObject? RenderingObject { get; set; }
        internal bool IsSelected { get; set; }
        internal bool IsLocked { get; set; }
        internal bool IsHover { get; set; }
        internal bool IsMoving { get; set; } = false;
        internal bool IsRenaming { get; set; } = false;
        internal Button CloseButton { get; set; }
        internal Button InspectorButton { get; set; }
        internal Color Color { get; set; }
        internal Color BorderColor { get; set; }
        internal Color SelectedColor { get; set; }
        internal Color HoverColor { get; set; }
        private Editor Editor { get; set; }
        private Timer MoveTimer { get; set; }
        private Group? Group { get; set; }

        public Component(int id, Editor editor, Group group, string name, int xPosition, int yPosition, int width, int height, int borderWidth, Color color, Color borderColor, Color selectedColor, Color hoverColor, IPermanentRenderingObject component)
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
            CloseButton = new Button(editor, CloseButtonXPosition, YPosition, "X", Editor.SmallButtonWidth, Editor.SmallButtonHeight, Editor.SmallButtonBorderWidth, closeButtonBaseColor, closeButtonBorderColor, closeButtonHoverColor, new DeleteComponentCommand(Editor, this), Button.ButtonType.Trigger);
            Color inspectorButtonBaseColor = Editor.InspectorButtonBaseColor;
            Color inspectorButtonBorderColor = Editor.InspectorButtonBorderColor;
            Color inspectorButtonHoverColor = Editor.InspectorButtonHoverColor;
            InspectorButton = new Button(editor, InspectorButtonXPosition, YPosition, "I", Editor.SmallButtonWidth, Editor.SmallButtonHeight, Editor.SmallButtonBorderWidth, inspectorButtonBaseColor, inspectorButtonBorderColor, inspectorButtonHoverColor, new ShowInspectorCommand(Editor, 1, 200, 200), Button.ButtonType.Trigger);
            MoveTimer = new Timer(0.1f);
            Group = group;
            RenderingObject = component;
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
                IsRenaming = false;
                MoveTimer.ResetTimer();
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
                if (Game.IsKeyPressed(KeyboardKey.Backspace))
                {
                    if (Name.Length > 0)
                    {
                        Name = Name.Remove(Name.Length - 1);
                    }
                }
                else if (Game.IsKeyPressed(KeyboardKey.Enter))
                {
                    IsSelected = false;
                }
                else if (Raylib.GetKeyPressed() > 0)
                {
                    Name = Regex.Unescape(Name + ((char)Raylib.GetCharPressed()).ToString()).Replace('\0', ' ');
                }
            }
        }
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
                    YPosition = Raylib.GetMouseY();
                    CloseButtonXPosition = XPosition + Width - Editor.SmallButtonWidth;
                    CloseButton.XPosition = CloseButtonXPosition;
                    CloseButton.YPosition = YPosition;
                    InspectorButtonXPosition = XPosition + Width - 2 * Editor.SmallButtonWidth;
                    InspectorButton.XPosition = InspectorButtonXPosition;
                    InspectorButton.YPosition = YPosition;
                }
            }
            //Detach this component if it's is dragged outside the group.
            if (Group is null)
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

        public bool IsInGroup() => Group != null;
    }
}
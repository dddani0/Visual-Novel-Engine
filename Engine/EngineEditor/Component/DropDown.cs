using Raylib_cs;
using TemplateGame.Component;
using VisualNovelEngine.Engine.EngineEditor.Component.Command;
using VisualNovelEngine.Engine.EngineEditor.Interface;

namespace VisualNovelEngine.Engine.EngineEditor.Component
{
    public class DropDown : IComponent
    {
        public enum FilterType
        {
            None,
            Sprite,
            Menu,
            Button,
            TextBox,
            Block
        }
        internal FilterType Filter { get; set; }
        private Editor Editor { get; set; }
        public int XPosition { get; set; }
        public int YPosition { get; set; }
        internal int Width { get; set; }
        internal int Height { get; set; }
        internal int BorderWidth { get; set; }
        internal string? Text { get; set; }
        internal bool IsHover { get; set; }
        internal bool IsSelected { get; set; } = false;
        internal Button Button { get; set; }
        internal List<Button> ButtonList { get; set; } = [];
        internal List<Button> FilteredButtonList { get; set; } = [];

        public DropDown(Editor editor, int xPosition, int yPosition, int width, int height, int borderWidth, FilterType filter)
        {
            Editor = editor;
            XPosition = xPosition;
            YPosition = yPosition;
            Width = width;
            Height = height;
            BorderWidth = borderWidth;
            Button = new Button(Editor, XPosition, YPosition, "New DropDown", true, Editor.ButtonWidth, Editor.ButtonHeight, Editor.ButtonBorderWidth, Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, new OpenDropDownCommand(Editor, this), Button.ButtonType.Hold);
            Filter = filter;
        }

        internal void UpdateComponentList()
        {
            //drops the existing list
            ButtonList.Clear();
            FilteredButtonList.Clear();
            //update whole list
            ButtonList.AddRange([.. Editor.ActiveScene.ComponentList.Cast<Component>().Select(component => new Button(Editor, this, $"ID:{component.ID}, Name:{component.Name}", Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, component))]);
            switch (Filter is FilterType.None)
            {
                case true:
                    FilteredButtonList = ButtonList;
                    break;
                case false:
                    foreach (Button item in ButtonList)
                    {
                        var component = item.Component as Component;
                        FilterType filter = component.RenderingObject switch
                        {
                            Sprite => FilterType.Sprite,
                            Menu => FilterType.Menu,
                            TemplateGame.Component.Button => FilterType.Button,
                            TextBox => FilterType.TextBox,
                            Block => FilterType.Block,
                            _ => throw new NotImplementedException()
                        };
                        if (filter == Filter) FilteredButtonList.Add(item);
                    }
                    break;
            }
            UpdateComponentPosition();
        }

        internal void UpdateComponentPosition()
        {
            for (int i = 0; i < FilteredButtonList.Count; i++)
            {
                FilteredButtonList[i].Width = Editor.SideButtonWidth;
                FilteredButtonList[i].Height = Editor.SideButtonHeight;
                FilteredButtonList[i].BorderWidth = Editor.SideButtonBorderWidth;
                FilteredButtonList[i].XPosition = XPosition;
                FilteredButtonList[i].YPosition = YPosition + (i * Editor.SideButtonHeight);
            }
        }
        public void Update()
        {
            Button.XPosition = XPosition;
            Button.YPosition = YPosition;
            IsSelected = Button.Selected;
        }

        public void Render()
        {
            Update();
            Button.Render();
        }
    }
}
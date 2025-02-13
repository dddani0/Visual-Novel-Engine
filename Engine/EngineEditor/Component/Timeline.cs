using Raylib_cs;
using TemplateGame.Interface;
using VisualNovelEngine.Engine.EngineEditor.Component.Command;
using VisualNovelEngine.Engine.EngineEditor.Interface;

namespace VisualNovelEngine.Engine.EngineEditor.Component
{
    public class Timeline : IWindow
    {
        internal Editor Editor { get; set; }
        internal int XPosition { get; set; }
        internal int YPosition { get; set; }
        internal int Width { get; set; }
        internal int Height { get; set; }
        internal int BorderWidth { get; set; }
        internal List<IEvent> Events { get; set; } = [];
        internal List<Button> EventButtons { get; set; } = [];
        internal Button AddGeneralEventButton { get; set; }
        internal Button AddTimelineIndependentEventButton { get; set; }
        internal Button RemoveEventButton { get; set; }
        internal Button EditEventButton { get; set; }

        public Timeline(Editor editor, int xPos, int yPos)
        {
            Editor = editor;
            XPosition = xPos;
            YPosition = yPos;
            Width = Raylib.GetScreenWidth() - XPosition;
            Height = Raylib.GetScreenHeight() - YPosition;
            BorderWidth = Editor.InspectorWindowBorderWidth;
            AddGeneralEventButton = new(
                Editor,
                XPosition + BorderWidth,
                YPosition,
                "Event",
                Editor.ButtonWidth,
                Editor.ButtonHeight,
                Editor.ButtonBorderWidth,
                Editor.BaseColor,
                Editor.BorderColor,
                Editor.HoverColor,
                null,
                0);
            AddGeneralEventButton.AddCommand(new ShowSideWindowCommand(Editor, AddGeneralEventButton, [
                new Button(
                    Editor,
                    0,
                    0,
                    "New 'MenuAction'",
                    Editor.ButtonWidth,
                    Editor.ButtonHeight,
                    Editor.ButtonBorderWidth,
                    Editor.BaseColor,
                    Editor.BorderColor,
                    Editor.HoverColor,
                new CreateGeneralEventCommand(
                    Editor,
                    CreateGeneralEventCommand.ActionType.CreateMenuAction),
                    0
                ),
                new Button(
                    Editor,
                    0,
                    0,
                    "new 'LoadSceneAction'",
                    Editor.ButtonWidth,
                    Editor.ButtonHeight,
                    Editor.ButtonBorderWidth,
                    Editor.BaseColor,
                    Editor.BorderColor,
                    Editor.HoverColor,
                    new CreateGeneralEventCommand(Editor, CreateGeneralEventCommand.ActionType.LoadSceneAction),
                    0
                    ),
            ]));
        }

        public void Show()
        {
            Raylib.DrawRectangle(XPosition, YPosition, Width, Height, Editor.BaseColor);
            Raylib.DrawRectangleLines(XPosition, YPosition, Width, Height, Editor.BorderColor);
            AddGeneralEventButton.Render();
            for (int i = 0; i < Events.Count; i++) EventButtons[i].Render();
            if (Events.Count <= 0) return;
            //RemoveEventButton.Render();
        }

        internal void AddEvent(IEvent action)
        {
            Events.Add(action);
            //add a slider
            EventButtons.Add(new(
                Editor,
                XPosition + BorderWidth + (Events.Count - 1) * (Editor.ButtonWidth + Editor.ButtonBorderWidth),
                YPosition + Height / 2,
                $"{Events.Count}. action",
                Editor.ButtonWidth,
                Editor.ButtonHeight,
                Editor.ButtonBorderWidth,
                Editor.BaseColor,
                Editor.BorderColor,
                Editor.HoverColor,
                null,
                Button.ButtonType.Trigger));
        }
    }
}
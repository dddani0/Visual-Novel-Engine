using EngineEditor.Component;
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
        internal Button ConfigureTimelineButton { get; set; }
        internal Button RemoveEventsButton { get; set; }
        internal Button EditEventButton { get; set; }
        internal Scrollbar Scrollbar { get; set; }

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
                    true,
                Editor.ButtonWidth,
                Editor.ButtonHeight,
                Editor.ButtonBorderWidth,
                Editor.BaseColor,
                Editor.BorderColor,
                Editor.HoverColor,
                null,
                (Button.ButtonType)2);
            AddGeneralEventButton.AddCommand(new ShowSideWindowCommand(Editor, AddGeneralEventButton, [
                new Button(
                    Editor,
                    0,
                    0,
                    "New 'MenuAction' action",
                    true,
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
                    "new 'LoadSceneAction' action",
                    true,
                    Editor.ButtonWidth,
                    Editor.ButtonHeight,
                    Editor.ButtonBorderWidth,
                    Editor.BaseColor,
                    Editor.BorderColor,
                    Editor.HoverColor,
                    new CreateGeneralEventCommand(Editor, CreateGeneralEventCommand.ActionType.LoadSceneAction),
                    0
                    ),
                new Button(
                    Editor,
                    0,
                    0,
                    "new 'NativeLoadSceneAction' action",
                    true,
                    Editor.ButtonWidth,
                    Editor.ButtonHeight,
                    Editor.ButtonBorderWidth,
                    Editor.BaseColor,
                    Editor.BorderColor,
                    Editor.HoverColor,
                    new CreateGeneralEventCommand(Editor, CreateGeneralEventCommand.ActionType.NativeLoadSceneAction),
                    0
                    ),
                new Button(
                    Editor,
                    0,
                    0,
                    "new 'AddSpriteAction' action",
                    true,
                    Editor.ButtonWidth,
                    Editor.ButtonHeight,
                    Editor.ButtonBorderWidth,
                    Editor.BaseColor,
                    Editor.BorderColor,
                    Editor.HoverColor,
                    new CreateGeneralEventCommand(Editor, CreateGeneralEventCommand.ActionType.AddSpriteAction),
                    0
                    ),
                new Button(
                    Editor,
                    0,
                    0,
                    "new 'ChangeSpriteAction' action",
                    true,
                    Editor.ButtonWidth,
                    Editor.ButtonHeight,
                    Editor.ButtonBorderWidth,
                    Editor.BaseColor,
                    Editor.BorderColor,
                    Editor.HoverColor,
                    new CreateGeneralEventCommand(Editor, CreateGeneralEventCommand.ActionType.ChangeSpriteAction),
                    0
                    ),
                new Button(
                    Editor,
                    0,
                    0,
                    "new 'CreateVariableAction' action",
                    true,
                    Editor.ButtonWidth,
                    Editor.ButtonHeight,
                    Editor.ButtonBorderWidth,
                    Editor.BaseColor,
                    Editor.BorderColor,
                    Editor.HoverColor,
                    new CreateGeneralEventCommand(Editor, CreateGeneralEventCommand.ActionType.CreateVariableAction),
                    0
                    ),
                new Button(
                    Editor,
                    0,
                    0,
                    "new 'DecrementVariableAction' action",
                    true,
                    Editor.ButtonWidth,
                    Editor.ButtonHeight,
                    Editor.ButtonBorderWidth,
                    Editor.BaseColor,
                    Editor.BorderColor,
                    Editor.HoverColor,
                    new CreateGeneralEventCommand(Editor, CreateGeneralEventCommand.ActionType.DecrementVariableAction),
                    0
                    ),
                new Button(
                    Editor,
                    0,
                    0,
                    "new 'EmptyAction' action",
                    true,
                    Editor.ButtonWidth,
                    Editor.ButtonHeight,
                    Editor.ButtonBorderWidth,
                    Editor.BaseColor,
                    Editor.BorderColor,
                    Editor.HoverColor,
                    new CreateGeneralEventCommand(Editor, CreateGeneralEventCommand.ActionType.EmptyAction),
                    0
                    ),
                new Button(
                    Editor,
                    0,
                    0,
                    "new 'IncrementVariableAction' action",
                    true,
                    Editor.ButtonWidth,
                    Editor.ButtonHeight,
                    Editor.ButtonBorderWidth,
                    Editor.BaseColor,
                    Editor.BorderColor,
                    Editor.HoverColor,
                    new CreateGeneralEventCommand(Editor, CreateGeneralEventCommand.ActionType.IncrementVariableAction),
                    0
                    ),
                new Button(
                    Editor,
                    0,
                    0,
                    "new 'RemoveSpriteAction' action",
                    true,
                    Editor.ButtonWidth,
                    Editor.ButtonHeight,
                    Editor.ButtonBorderWidth,
                    Editor.BaseColor,
                    Editor.BorderColor,
                    Editor.HoverColor,
                    new CreateGeneralEventCommand(Editor, CreateGeneralEventCommand.ActionType.RemoveSpriteAction),
                    0
                    ),
                new Button(
                    Editor,
                    0,
                    0,
                    "new 'SetBoolVariableAction' action",
                    true,
                    Editor.ButtonWidth,
                    Editor.ButtonHeight,
                    Editor.ButtonBorderWidth,
                    Editor.BaseColor,
                    Editor.BorderColor,
                    Editor.HoverColor,
                    new CreateGeneralEventCommand(Editor, CreateGeneralEventCommand.ActionType.SetBoolVariableAction),
                    0
                    ),
                new Button(
                    Editor,
                    0,
                    0,
                    "new 'SetVariableFalseAction' action",
                    true,
                    Editor.ButtonWidth,
                    Editor.ButtonHeight,
                    Editor.ButtonBorderWidth,
                    Editor.BaseColor,
                    Editor.BorderColor,
                    Editor.HoverColor,
                    new CreateGeneralEventCommand(Editor, CreateGeneralEventCommand.ActionType.SetVariableFalseAction),
                    0
                    ),
                new Button(
                    Editor,
                    0,
                    0,
                    "new 'SetVariableTrueAction' action",
                    true,
                    Editor.ButtonWidth,
                    Editor.ButtonHeight,
                    Editor.ButtonBorderWidth,
                    Editor.BaseColor,
                    Editor.BorderColor,
                    Editor.HoverColor,
                    new CreateGeneralEventCommand(Editor, CreateGeneralEventCommand.ActionType.SetVariableTrueAction),
                    0
                    ),
                new Button(
                    Editor,
                    0,
                    0,
                    "new 'TextBoxCreateAction' action",
                    true,
                    Editor.ButtonWidth,
                    Editor.ButtonHeight,
                    Editor.ButtonBorderWidth,
                    Editor.BaseColor,
                    Editor.BorderColor,
                    Editor.HoverColor,
                    new CreateGeneralEventCommand(Editor, CreateGeneralEventCommand.ActionType.TextBoxCreateAction),
                    0
                    ),
                new Button(
                    Editor,
                    0,
                    0,
                    "new 'TintSpriteAction' action",
                    true,
                    Editor.ButtonWidth,
                    Editor.ButtonHeight,
                    Editor.ButtonBorderWidth,
                    Editor.BaseColor,
                    Editor.BorderColor,
                    Editor.HoverColor,
                    new CreateGeneralEventCommand(Editor, CreateGeneralEventCommand.ActionType.TintSpriteAction),
                    0
                    ),
                new Button(
                    Editor,
                    0,
                    0,
                    "new 'ToggleVariableAction' action",
                    true,
                    Editor.ButtonWidth,
                    Editor.ButtonHeight,
                    Editor.ButtonBorderWidth,
                    Editor.BaseColor,
                    Editor.BorderColor,
                    Editor.HoverColor,
                    new CreateGeneralEventCommand(Editor, CreateGeneralEventCommand.ActionType.ToggleVariableAction),
                    0
                    )
            ]));
            ConfigureTimelineButton = new(Editor, XPosition + Editor.ButtonWidth, YPosition, "Configure", true, Editor.ButtonWidth, Editor.ButtonHeight, Editor.ButtonBorderWidth, Editor.BaseColor, Editor.BorderColor, Editor.HoverColor, new EmptyCommand(), Button.ButtonType.Trigger);
            RemoveEventsButton = new(
                Editor,
                XPosition + Width - Editor.ButtonWidth,
                YPosition,
                "Remove",
                    true,
                Editor.ButtonWidth,
                Editor.ButtonHeight,
                Editor.ButtonBorderWidth,
                Editor.BaseColor,
                Editor.BorderColor,
                Editor.HoverColor,
                new ShowErrorCommand(Editor, "Delete all events from timeline?", [
                    new Button(
                        Editor,
                        0,
                        0,
                        "Yes",
                        true,
                        Editor.ButtonWidth,
                        Editor.ButtonHeight,
                        Editor.ButtonBorderWidth,
                        Editor.BaseColor,
                        Editor.BorderColor,
                        Editor.HoverColor,
                        new DeleteAllEventsCommand(Editor),
                        0
                    )
                ]),
                Button.ButtonType.Trigger);
            Scrollbar = new(
                Editor,
                XPosition,
                YPosition + Height - Editor.SmallButtonHeight,
                Editor.SmallButtonHeight,
                Raylib.GetScreenWidth(),
                Scrollbar.ScrollbarType.Horizontal,
                false,
                [.. EventButtons]);
        }

        public void Show()
        {
            Raylib.DrawRectangle(XPosition, YPosition, Width, Height, Editor.BaseColor);
            Raylib.DrawRectangleLines(XPosition, YPosition, Width, Height, Editor.BorderColor);
            AddGeneralEventButton.Render();
            ConfigureTimelineButton.Render();
            for (int i = 0; i < Events.Count; i++)
            {
                EventButtons[i].Render();
            }
            if ((EventButtons.Count * Editor.ButtonWidth) > (XPosition + Width)) ScrollBar();
            if (Events.Count <= 0) return;
            RemoveEventsButton.Render();
        }

        private void ScrollBar()
        {
            Scrollbar.Render();
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
                    true,
                Editor.ButtonWidth,
                Editor.ButtonHeight,
                Editor.ButtonBorderWidth,
                Editor.BaseColor,
                Editor.BorderColor,
                Editor.HoverColor,
                new ShowInspectorCommand(Editor, action, 1),
                Button.ButtonType.Trigger));
            Scrollbar.AddComponent(EventButtons[^1]);
        }
    }
}
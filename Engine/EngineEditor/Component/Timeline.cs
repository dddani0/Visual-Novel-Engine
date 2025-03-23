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
        internal List<IAction> Actions { get; set; } = [];
        internal List<ISettingsEvent> TimelineIndepententActions { get; set; } = [];
        internal List<Button> ActionButtons { get; set; } = [];
        internal List<Button> TimelineIndepententActionButtons { get; set; } = [];
        internal Button AddGeneralAction { get; set; }
        internal Button RemoveActionsButton { get; set; }
        internal Scrollbar Scrollbar { get; set; }
        internal Scrollbar TimelineIndepententScrollbar { get; set; }

        public Timeline(Editor editor, int xPos, int yPos)
        {
            Editor = editor;
            XPosition = xPos;
            YPosition = yPos;
            Width = Raylib.GetScreenWidth() - XPosition;
            Height = Raylib.GetScreenHeight() - YPosition;
            BorderWidth = Editor.InspectorWindowBorderWidth;
            AddGeneralAction = new(
                Editor,
                5 + XPosition + BorderWidth,
                YPosition + 5,
                "Action",
                    true,
                Editor.ButtonWidth,
                Editor.ButtonHeight,
                Editor.ButtonBorderWidth,
                Editor.BaseColor,
                Editor.BorderColor,
                Editor.HoverColor,
                null,
                (Button.ButtonType)2);
            AddGeneralAction.AddCommand(new ShowSideWindowCommand(Editor, AddGeneralAction, [
                new Button(
                    Editor,
                    0,
                    0,
                    "New 'MenuAction' action",
                    true,
                    Editor.SideButtonWidth,
                    Editor.SideButtonHeight,
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
                    Editor.SideButtonWidth,
                    Editor.SideButtonHeight,
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
                    Editor.SideButtonWidth,
                    Editor.SideButtonHeight,
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
                    Editor.SideButtonWidth,
                    Editor.SideButtonHeight,
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
                    Editor.SideButtonWidth,
                    Editor.SideButtonHeight,
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
                    "new 'DecrementVariableAction' action",
                    true,
                    Editor.SideButtonWidth,
                    Editor.SideButtonHeight,
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
                    Editor.SideButtonWidth,
                    Editor.SideButtonHeight,
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
                    Editor.SideButtonWidth,
                    Editor.SideButtonHeight,
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
                    Editor.SideButtonWidth,
                    Editor.SideButtonHeight,
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
                    Editor.SideButtonWidth,
                    Editor.SideButtonHeight,
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
                    Editor.SideButtonWidth,
                    Editor.SideButtonHeight,
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
                    Editor.SideButtonWidth,
                    Editor.SideButtonHeight,
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
                    Editor.SideButtonWidth,
                    Editor.SideButtonHeight,
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
                    Editor.SideButtonWidth,
                    Editor.SideButtonHeight,
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
                    Editor.SideButtonWidth,
                    Editor.SideButtonHeight,
                    Editor.ButtonBorderWidth,
                    Editor.BaseColor,
                    Editor.BorderColor,
                    Editor.HoverColor,
                    new CreateGeneralEventCommand(Editor, CreateGeneralEventCommand.ActionType.ToggleVariableAction),
                    0
                    )
            ]));
            RemoveActionsButton = new(
                Editor,
                XPosition + Width - Editor.ButtonWidth - 5,
                YPosition + 5,
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
                XPosition - 5,
                YPosition + Height - Editor.SmallButtonHeight,
                Editor.SmallButtonHeight,
                Raylib.GetScreenWidth(),
                Scrollbar.ScrollbarType.Horizontal,
                false,
                [.. ActionButtons]);
            TimelineIndepententScrollbar = new(
                Editor,
                XPosition - 5,
                YPosition + Height - 2 * Editor.SmallButtonHeight,
                Editor.SmallButtonHeight,
                Raylib.GetScreenWidth(),
                Scrollbar.ScrollbarType.Horizontal,
                false,
                [.. TimelineIndepententActionButtons]);
        }

        public void Show()
        {
            Raylib.DrawRectangle(XPosition, YPosition, Width, Height, Editor.BaseColor);
            Raylib.DrawRectangleLines(XPosition, YPosition, Width, Height, Editor.BorderColor);
            AddGeneralAction.Render();
            for (int i = 0; i < Actions.Count; i++)
            {
                ActionButtons[i].Render();
            }
            for (int i = 0; i < TimelineIndepententActions.Count; i++)
            {
                TimelineIndepententActionButtons[i].Render();
            }
            if (TimelineIndepententActionButtons.Count * Editor.ButtonWidth > XPosition + Width) TimelineIndepententScrollbar.Render();
            if ((ActionButtons.Count * Editor.ButtonWidth) > (XPosition + Width)) Scrollbar.Render();
            if (Actions.Count <= 0 && TimelineIndepententActions.Count <= 0) return;
            RemoveActionsButton.Render();
        }
        internal void AddAction(IAction action)
        {
            Actions.Add(action);
            //add a slider
            ActionButtons.Add(new(
                Editor,
                XPosition + Editor.ComponentWidth + BorderWidth + 5 + (Actions.Count - 1) * (Editor.ButtonWidth + Editor.ButtonBorderWidth),
                YPosition + Height / 2 - 2 * Editor.SmallButtonWidth,
                $"{Actions.Count}. action",
                    true,
                Editor.ButtonWidth,
                Editor.ButtonHeight,
                Editor.ButtonBorderWidth,
                Editor.BaseColor,
                Editor.BorderColor,
                Editor.HoverColor,
                new ShowInspectorCommand(Editor, action, 1),
                Button.ButtonType.Trigger));
            Scrollbar.AddComponent(ActionButtons[^1]);
        }

        internal void AddTimelineIndependentAction(ISettingsEvent action)
        {
            TimelineIndepententActions.Add(action);
            TimelineIndepententActionButtons.Add(new(
                Editor,
                XPosition + Editor.ComponentWidth + BorderWidth + 5 + (TimelineIndepententActions.Count - 1) * (Editor.ButtonWidth + Editor.ButtonBorderWidth),
                YPosition + Editor.ComponentHeight + Height / 2 - Editor.SmallButtonWidth,
                $"{TimelineIndepententActions.Count}. action",
                    true,
                Editor.ButtonWidth,
                Editor.ButtonHeight,
                Editor.ButtonBorderWidth,
                Editor.BaseColor,
                Editor.BorderColor,
                Editor.HoverColor,
                new ShowInspectorCommand(Editor, (IAction)action, 1),
                Button.ButtonType.Trigger));
            TimelineIndepententScrollbar.AddComponent(TimelineIndepententActionButtons[^1]);
        }
    }
}
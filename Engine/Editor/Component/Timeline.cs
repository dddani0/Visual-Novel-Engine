using EngineEditor.Component;
using Raylib_cs;
using VisualNovelEngine.Engine.Game.Interface;
using VisualNovelEngine.Engine.Editor.Component.Command;
using VisualNovelEngine.Engine.Editor.Interface;

namespace VisualNovelEngine.Engine.Editor.Component
{
    /// <summary>
    /// Represents a timeline, which is a collection of actions that the Game will execute.
    /// </summary>
    public class Timeline : IWindow
    {
        /// <summary>
        /// The editor that the timeline is associated with.
        /// </summary>
        internal Editor Editor { get; set; }
        /// <summary>
        /// The x position of the timeline.
        /// </summary>
        internal int XPosition { get; set; }
        /// <summary>
        /// The y position of the timeline.
        /// </summary>
        internal int YPosition { get; set; }
        /// <summary>
        /// The width of the timeline.
        /// </summary>
        internal int Width { get; set; }
        /// <summary>
        /// The height of the timeline.
        /// </summary>
        internal int Height { get; set; }
        /// <summary>
        /// The border width of the timeline.
        /// </summary>
        internal int BorderWidth { get; set; }
        /// <summary>
        /// The actions that the timeline will execute.
        /// </summary>
        internal List<IAction> Actions { get; set; } = [];
        internal List<IAction> ComponentActions { get; set; } = [];
        /// <summary>
        /// The timeline independent actions
        /// </summary>
        internal List<ISettingsAction> TimelineIndepententActions { get; set; } = [];
        /// <summary>
        /// The button representation of the actions.
        /// </summary>
        internal List<Button> ActionButtons { get; set; } = [];
        /// <summary>
        /// The button representation of the timeline independent actions.
        /// </summary>
        internal List<Button> TimelineIndepententActionButtons { get; set; } = [];
        /// <summary>
        /// The button that adds a general action to the timeline.
        /// </summary>
        internal Button AddGeneralAction { get; set; }
        /// <summary>
        /// The button that removes all actions from the timeline.
        /// </summary>
        internal Button RemoveActionsButton { get; set; }
        /// <summary>
        /// The scrollbar that controls the timeline.
        /// </summary>
        internal Scrollbar Scrollbar { get; set; }
        /// <summary>
        /// The scrollbar that controls the timeline independent actions.
        /// </summary>
        internal Scrollbar TimelineIndepententScrollbar { get; set; }
        /// <summary>
        /// Creates a new instance of the timeline.
        /// </summary>
        /// <param name="editor"></param>
        /// <param name="xPos"></param>
        /// <param name="yPos"></param>
        public Timeline(Editor editor, int xPos, int yPos, IAction[] actions, ISettingsAction[] timelineIndependentActions)
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
                        Editor.ButtonWidth,
                        Editor.ButtonHeight,
                        Editor.ButtonBorderWidth,
                        Editor.BaseColor,
                        Editor.BorderColor,
                        Editor.HoverColor,
                        new DeleteAllActionsCommand(Editor),
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
            if (actions.Length == 0 && timelineIndependentActions.Length == 0) return;
            foreach (var action in actions)
            {
                AddAction(action);
            }
            foreach (var action in timelineIndependentActions)
            {
                AddTimelineIndependentAction(action);
            }
        }
        /// <summary>
        /// Renders the timeline on the screen.
        /// </summary>
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
        /// <summary>
        /// Updates the timeline.
        /// </summary>
        /// <param name="action"></param>
        internal void AddAction(IAction action)
        {
            Actions.Add(action);
            //add a slider
            ActionButtons.Add(new(
                Editor,
                XPosition + Editor.ComponentWidth + BorderWidth + 5 + (Actions.Count - 1) * (Editor.ButtonWidth + Editor.ButtonBorderWidth),
                YPosition + Height / 2 - 2 * Editor.SmallButtonWidth,
                $"{Actions.Count}. {action.GetType().Name}",
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
        /// <summary>
        /// Adds a component action to the timeline.
        /// </summary>
        /// <param name="action"></param>
        internal void AddComponentAction(IAction action)
        {
            ComponentActions.Add(action);
            ActionButtons.Add(new(
                Editor,
                XPosition + Editor.ComponentWidth + BorderWidth + 5 + (Actions.Count - 1) * (Editor.ButtonWidth + Editor.ButtonBorderWidth),
                YPosition + Height / 2 - 2 * Editor.SmallButtonWidth,
                $"{Actions.Count}. {action.GetType().Name}",
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
        /// <summary>
        /// Adds a timeline independent action.
        /// </summary>
        /// <param name="action"></param>
        internal void AddTimelineIndependentAction(ISettingsAction action)
        {
            TimelineIndepententActions.Add(action);
            TimelineIndepententActionButtons.Add(new(
                Editor,
                XPosition + Editor.ComponentWidth + BorderWidth + 5 + (TimelineIndepententActions.Count - 1) * (Editor.ButtonWidth + Editor.ButtonBorderWidth),
                YPosition + Editor.ComponentHeight + Height / 2 - Editor.SmallButtonWidth,
                $"{TimelineIndepententActions.Count}. action",
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
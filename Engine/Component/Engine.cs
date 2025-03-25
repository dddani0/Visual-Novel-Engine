using Raylib_cs;
using VisualNovelEngine.Engine.Component.Command;
using VisualNovelEngine.Engine.Editor.Component;
using VisualNovelEngine.Engine.Editor.Component.Command;

namespace VisualNovelEngine.Engine.Component
{
    public enum EngineState
    {
        PreState,
        EditorState,
        GameState
    }
    /// <summary>
    /// Engine is the base of the Visual Novel Engine.
    /// It contains the Editor and the Game and connects them together.
    /// Create a new project, import a project or open a game project.
    /// </summary>
    public class Engine
    {
        /// <summary>
        /// The Editor of the Engine.
        /// </summary>
        internal Editor.Component.Editor Editor { get; set; }
        /// <summary>
        /// The Game of the Engine.
        /// </summary>
        internal Game.Component.Game Game { get; set; }
        /// <summary>
        /// The current state of the Engine.
        /// </summary>
        internal EngineState State { get; private set; }
        /// <summary>
        /// The title of the Engine.
        /// </summary>
        internal string Title { get; set; } = "Vizuális Novella Motor";
        /// <summary>
        /// The width of the Engine.
        /// </summary>
        internal int Width { get; set; } = 800;
        /// <summary>
        /// The height of the Engine.
        /// </summary>
        internal int Height { get; set; } = 300;
        /// <summary>
        /// The exit flag of the Engine.
        /// </summary>
        internal bool Exit { get; set; } = false;
        /// <summary>
        /// The button to import a project.
        /// </summary>
        internal Button ImportProjectButton { get; set; }
        /// <summary>
        /// The button to create a new project.
        /// </summary>
        internal Button NewProjectButton { get; set; }
        /// <summary>
        /// The button to open a game project.
        /// </summary>
        internal Button OpenGameProject { get; set; }
        /// <summary>
        /// The button to open the repository.
        /// </summary>
        private Button RepositoryButton { get; set; }
        /// <summary>
        /// The path of the project.
        /// </summary>
        internal string ProjectPath { get; private set; } = "../../../Engine/Data/";
        /// <summary>
        /// The path of the project data.
        /// </summary>
        internal string ProjectDataPath { get; set; }
        /// <summary>
        /// The path of the project build.
        /// </summary>
        internal string ProjectBuildPath { get; set; }
        internal Window? Window { get; set; } = null;

        /// <summary>
        /// Create a new Engine.
        /// </summary>
        public Engine()
        {
            //Set base window attributes.
            Raylib.InitWindow(Width, Height, Title);
            //Set the state to PreState.
            ChangeState(EngineState.PreState);
            //Prestate buttons
            NewProjectButton = new Button("Új projekt", Width / 2 - 125, 80, 210, 50, Color.RayWhite, Color.Gray, new OpenWindowCommand(this, WindowType.NewProject));
            ImportProjectButton = new Button("Projekt importálás", Width / 2 - 125, 140, 210, 50, Color.RayWhite, Color.Gray, new OpenWindowCommand(this, WindowType.ImportProject));
            OpenGameProject = new Button("Játék megnyitása", Width / 2 - 125, 200, 210, 50, Color.RayWhite, Color.Gray, new EmptyCommand());
            RepositoryButton = new Button("GitHub", 10, Height - 50 - 10, 100, 50, Color.RayWhite, Color.Gray, new OpenLinkCommand("https://github.com/dddani0/Visual-Novel-Engine"));
        }
        /// <summary>
        /// The main loop of the engine.
        /// </summary>
        public void Process()
        {
            while (Exit is false)
            {
                Raylib.BeginDrawing();
                switch (State)
                {
                    case EngineState.PreState:
                        Exit = Raylib.WindowShouldClose();
                        Raylib.ClearBackground(Color.Black);
                        Raylib.DrawText(Title, 125, 0, 50, Color.RayWhite);
                        NewProjectButton.Render();
                        ImportProjectButton.Render();
                        OpenGameProject.Render();
                        RepositoryButton.Render();
                        Window?.Show();
                        break;
                    case EngineState.EditorState:
                        Editor.Update();
                        break;
                    case EngineState.GameState:
                        Game.Update();
                        break;
                }
                Raylib.EndDrawing();
            }
            Raylib.CloseWindow();
        }

        public void ChangeState(EngineState state)
        {
            //Enter code
            State = state;
            //Exit code
        }
        public void ChangeTitle(string title)
        {
            Title = title;
            Raylib.SetWindowTitle(title);
        }
    }
}
#include <raylib.h>
#include <string>

#include "scene.cpp"
#include "textdata.cpp"

int main()
{
    int theWindowLenghtPx = 1650;
    int theWindowHeightPx = 980;
    // string windowTitle = "Visual Novel Játékmotor";

    InitWindow(theWindowLenghtPx, theWindowHeightPx, "Visual Novel Játékmotor");
    SetTargetFPS(60);
    while (WindowShouldClose() == false)
    {
        BeginDrawing();
        ClearBackground(ALABASTER);
        TextData test("ez egy kibaszott teszt.", 1);
        test.writeToScreen();
        //DrawText(d.c_str(), theWindowLenghtPx / 2, theWindowHeightPx / 2, 24, BLACK);
        DrawFPS(0, 0);
        EndDrawing();
    }
    CloseWindow();
    return 0;
}
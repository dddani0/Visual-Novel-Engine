#include <string>
#include <raylib.h>
#include <cmath>

using namespace std;

class TextData
{
    string unloadedText = "";

public:
    string content;
    /// @brief Characters per seconds
    int cpsTextSpeed;
    /// @brief
    /// @param data text data.
    /// @param cps characters per seconds
    TextData(string data, int cps)
    {
        content = data;
        cpsTextSpeed = cps;
    }
    string getContent()
    {
        return content.c_str();
    }
    int getSpeed()
    {
        return cpsTextSpeed;
    }
    string loadText()
    {
        return unloadedText.c_str();
    }

    /// @brief Write content to screen with cps speed..
    void writeToScreen()
    {
        float timer = GetTime() + 1; // one second
        bool isFinished = false;
        int currentIdx = 0;
        int maxIdx = getContent().length();
        if (currentIdx == maxIdx) // No content.
            return;
        while (!isFinished)
        {
            bool isOnCooldown = timer > 0;
            if (isOnCooldown)
            {
                timer = (int)floor(timer - GetTime());
            }
            else
            {
                for (int i = 0; i < cpsTextSpeed; i++)
                {
                    unloadedText += getContent()[currentIdx];
                    currentIdx++;
                }
                timer = GetTime() + 1; // reset the timer.
            }
            isFinished = currentIdx == maxIdx; // finish only when the index is matching
        }
        return;
    }
};
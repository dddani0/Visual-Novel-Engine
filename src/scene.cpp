#include <bits/stdc++.h>

using namespace std;

class Scene
{
public:
    string sceneName;
    Scene(string name) {
        sceneName = name;
    }

    string getName()
    {
        return sceneName;
    }

private:
    int id;
};
#include <bits/stdc++.h>

using namespace std;

class Timeline
{
public:
    string timelineName;
    Timeline(string name) {
        timelineName = name;
    }

    string getName()
    {
        return timelineName;
    }

private:
    int id;
};
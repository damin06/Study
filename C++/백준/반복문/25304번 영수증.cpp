#include <iostream>
using namespace std;

int main()
{
    int cost, count;
    int a = 0, b = 0, allcost = 0;

    cin >> cost >> count;

    for (int i = 0; i < count; i++)
    {
        cin >> a >> b;
        allcost += a * b;
    }

    if (cost == allcost)
    {
        cout << "Yes";
        return 0;
    }
    cout << "No";
}
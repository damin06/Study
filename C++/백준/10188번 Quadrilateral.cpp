#include <iostream>
using namespace std;

int main()
{
    int t, x, y;
    cin >> t;

    for (int i = 0; i < t; i++)
    {
        cin >> x >> y;
        for(int j = 0; j < y; j++)
        {
            for(int k = 0; k < x; k++)
            {
                cout << "X";
            }
            cout << "\n";
        }
        cout << "\n";
    }
}
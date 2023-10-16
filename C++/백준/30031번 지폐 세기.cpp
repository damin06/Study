#include<iostream>
using namespace std;

int main()
{
    int n, total = 0, inputX, inputY;
    cin >> n;
    for (int i = 0; i < n; i++)
    {
        cin >> inputX >> inputY;
        switch (inputX)
        {
        case 136:
            total += 1000;
            break;
        case 142:
            total += 5000;
            break;
        case 148:
            total += 10000;
            break;
        case 154:
            total += 50000;
            break;
        }
    }

    cout << total;
}
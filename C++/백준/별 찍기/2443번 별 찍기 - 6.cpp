#include <iostream>
using namespace std;

int main()
{
    int count;
    cin >> count;

    for (int i = count; i>=0; i--)
    {
        for (int j = 1; j <= count - i; j++)
        {
            cout << " ";
        }
        for (int j = 1; j <= 2 * i - 1; j++)
        {
            cout << "*";
        }

        cout << endl;
    }
}
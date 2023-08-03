#include <iostream>
using namespace std;

int main()
{
    int a;
    cin >> a;

    for (int i = 0; i < a; i += 4)
    {
        cout << "long ";
    }
    cout << "int";
}
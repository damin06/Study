#include <iostream>
using namespace std;

int main()
{
    int number, answer = 0;
    cin >> number;
    for (int i = 1; i <= number; i++)
    {
        answer += i;
    }
    cout << answer;
}
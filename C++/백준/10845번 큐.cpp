#include <iostream>
#include<queue>
#include <string>
using namespace std;

int main()
{
    ios_base::sync_with_stdio(false);
    cin.tie(NULL);
    cout.tie(NULL);
    queue<int>myqueue;
    int n;
    string str;
    cin >> n;
    cin.ignore();
    while (n--)
    {
        getline(cin, str);
        if (str.substr(0, 4) == "push")
        {
            myqueue.push(stoi(str.substr(5)));
        }
        else if (str == "front")
        {
            if (myqueue.empty()) cout << -1 << '\n';
            else    cout << myqueue.front() << '\n';

        }
        else if (str == "back")
        {
            if (myqueue.empty()) cout << -1 << '\n';
            else    cout << myqueue.back() << '\n';
        }
        else if (str == "size")
        {
            cout << myqueue.size() << '\n';
        }
        else if (str == "empty")
        {
            cout << myqueue.empty() << '\n';
        }
        else if (str == "pop")
        {
            if (myqueue.empty()) cout << -1 << '\n';
            else
            {
                cout << myqueue.front() << '\n';
                myqueue.pop();
            }
        }

    }
}
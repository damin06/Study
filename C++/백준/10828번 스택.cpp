#include <iostream>
#include<stack>
#include <string>
using namespace std;

int main()
{
    stack<int>mystack;
    int n;
    string str;
    cin >> n;

    for (int i = 0; i <= n; i++)
    {
        getline(cin, str);
        if (str.substr(0, 4) == "push")
        {
            mystack.push(stoi(str.substr(5)));
        }
        else if (str == "top")
        {
            if (mystack.empty()) cout << -1 << '\n';
            else    cout << mystack.top() << '\n';
        }
        else if (str == "size")
        {
            cout << mystack.size() << '\n';
        }
        else if (str == "empty")
        {
            cout << mystack.empty() << '\n';
        }
        else if (str == "pop")
        {
            if (mystack.empty()) cout << -1 << '\n';
            else
            {
                cout << mystack.top() << '\n';
                mystack.pop();
            }
        }

    }
}
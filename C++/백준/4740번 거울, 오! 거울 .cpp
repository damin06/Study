#include <iostream>
#include <stack>
#include <string>
using namespace std;

int main()
{
		stack<char> s1;
		string _input;
		while (true)
		{
			getline(cin, _input);

			if(_input == "***")
			{
				return 0;
			}

			for (int i = 0; i < _input.length(); i++)
			{
				s1.push(_input[i]);
			}

			while (!s1.empty())
			{
				cout << s1.top();
				s1.pop();
			}

			cout << endl;
		}
}
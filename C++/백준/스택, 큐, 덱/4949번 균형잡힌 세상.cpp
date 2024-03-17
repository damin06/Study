#include <iostream>
#include <string>
#include <stack>
using namespace std;

int main()
{
	ios_base::sync_with_stdio(false);
	cin.tie(nullptr);

	stack<char> st;
	string input;
	bool result = true;

	while (true)
	{
		getline(cin, input);

		if (input[0] == '.' && input.length() == 1)
			break;

		for (int i = 0; i < input.length(); i++)
		{
			switch (input[i])
			{
			case '(':
			case '[':
				st.push(input[i]);
				break;
			case ']':
				if (!st.empty() && st.top() == '[')
					st.pop();
				else
					result = false;
				break;
			case ')':
				if (!st.empty() && st.top() == '(')
					st.pop();
				else
					result = false;
				break;
			}
		}

		if (st.empty() && result)
			cout << "yes" << "\n";
		else
			cout << "no" << "\n";

		input.clear();
		st = {};
		result = true;
	}
}
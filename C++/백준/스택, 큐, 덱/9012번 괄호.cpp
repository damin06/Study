#include <iostream>
#include <stack>
using namespace std;

int main()
{
	string input;
	int n;
	cin >> n;
	bool isNo = false;

	for(int i = 0; i < n; i++)
	{
		stack<char> st;
		isNo = false;
		cin >> input;

		for(int i = 0; i < input.size(); i++)
		{
			if (input[i] == '(')
				st.push(input[i]);
			else if(st.empty())
			{
				cout << "NO" << "\n";
				isNo = true;
				break;
			}
			else
				st.pop();
		}
		if (isNo)
			continue;

		if (st.empty())
			cout << "YES" << "\n";
		else
			cout << "NO" << "\n";
	}
}
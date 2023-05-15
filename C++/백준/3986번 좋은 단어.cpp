#include <iostream>
#include <stack>
#include <string>
using namespace std;

int main()
{
	int t, cnt = 0;
	string str;
	cin >> t;
	while (t--)
	{
		stack<char>st;
		cin >> str;
		for (int i = 0; i < str.size(); i++)
		{
			if (!st.empty() && str[i] == st.top())
			{
				st.pop();
			}
			else
			{
				st.push(str[i]);
			}
		}
		if (st.empty()) cnt++;
	}
	cout << cnt;
}
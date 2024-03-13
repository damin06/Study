#include <iostream>
#include <stack>
using namespace std;

int main()
{
	stack<int> st;
	int n, input, answer = 0;

	cin >> n;

	for(int i = 0; i < n; i++)
	{
		cin >> input;

		if (input == 0)
			st.pop();
		else
			st.push(input);
	}

	while (!st.empty())
	{
		answer += st.top();
		st.pop();
	}

	cout << answer;
}
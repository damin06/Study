#include <iostream>
#include<stack>
using namespace std;

int main() 
{
	ios_base::sync_with_stdio(false);
	cin.tie(NULL);

	stack<int> st;
	int n, input = 0, num = 0;
	cin >> n;

	for(int i = 0; i < n; i++)
	{
		cin >> input;
		switch (input)
		{
		case 1:
			cin >> num;
			st.push(num);
			break;
		case 2:
			if (st.empty())
				cout << -1 << "\n";
			else 
			{
				cout << st.top() << "\n";
				st.pop();
			}
				break;
		case 3:
			cout << st.size() << "\n";
			break;
		case 4:
			cout << (st.empty() ? 1 : 0) << "\n";
			break;
		case 5:
			cout << (st.empty() ? -1 : st.top()) << "\n";
		}
	}
}
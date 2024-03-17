#include <iostream>
#include <deque>
using namespace std;

int main()
{
	ios_base::sync_with_stdio(false);
	cin.tie(nullptr);

	int n, input;
	deque<int> q;
	cin >> n;

	while(n--)
	{
		cin >> input;

		switch (input)
		{
		case 1:
			cin >> input;
			q.push_front(input);
			break;
		case 2:
			cin >> input;
			q.push_back(input);
			break;
		case 3:
			if (!q.empty())
			{
				cout << q.front() << "\n";
				q.pop_front();
			}
			else
				cout << -1 << "\n";
			break;
		case 4:
			if (!q.empty())
			{
				cout << q.back() << "\n";
				q.pop_back();
			}
			else
				cout << -1 << "\n";
			break;
		case 5:
			cout << q.size() << "\n";
			break;
		case 6:
			cout << (q.empty() ? 1 : 0) << "\n";
			break;
		case 7:
			if (!q.empty())
				cout << q.front() << "\n";
			else
				cout << -1 << "\n";
			break;
		case 8:
			if (!q.empty())
				cout << q.back() << "\n";
			else
				cout << -1 << "\n";
			break;
		}
	}
}
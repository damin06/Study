#include <iostream>
#include <queue>
using namespace std;

int main()
{
	ios_base::sync_with_stdio(false);
	cin.tie(nullptr);

	int n, k;
	queue<int> q;

	cin >> n >> k;

	for(int i = 1; i <= n; i++)
	{
		q.push(i);
	}

	cout << "<";

	while (!q.empty())
	{
		for(int i = 1; i < k; i++)
		{
			q.push(q.front());
			q.pop();
		}
		cout << q.front();
		if (q.size() != 1)
			cout << ", ";
		q.pop();
	}

	cout << ">";
}
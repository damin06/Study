#include <iostream>
#include <vector>
#include <algorithm>
using namespace std;

int main()
{
	int t, n, m, input, total = 0;
	vector<int> a;
	vector<int> b;

	cin >> t;

	for (int i = 0; i < t; i++)
	{
		a.clear();
		b.clear();
		total = 0;
		cin >> n >> m;

		for (int j = 0; j < n; j++)
		{
			cin >> input;
			a.push_back(input);
		}

		for (int k = 0; k < m; k++)
		{
			cin >> input;
			b.push_back(input);
		}

		sort(a.begin(), a.end());
		sort(b.begin(), b.end());

		for (int j = 0; j < a.size(); j++)
			total += lower_bound(b.begin(), b.end(), a[j]) - b.begin();

		cout << total << "\n";
	}
}
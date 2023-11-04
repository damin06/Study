#include <iostream>
#include <vector>
using namespace std;

int main()
{
	int n, m, total = 0, cnt = 0;
	vector<int> vec;
	cin >> m >> n;

	for(int i = m; i <= n; i++)
	{
		cnt = 0;
		for (int j = 1; j <= i; j++)
			cnt += i % j == 0 ? 1 : 0;

		if (cnt == 2)
			vec.push_back(i);
	}

	if(vec.size() == 0)
	{
		cout << -1;
		return 0;
	}

	for (int i = 0; i < vec.size(); i++)
		total += vec[i];

	cout << total << "\n" << vec[0];
}
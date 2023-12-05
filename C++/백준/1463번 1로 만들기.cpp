#include <iostream>
#include <vector>
#include <algorithm>
using namespace std;

int dp(int n)
{
	vector<int> vec(n + 1);
	for (int i = 2; i <= n; i++)
	{
		vec[i] = vec[i - 1] + 1;
		if(i % 2 == 0)
			vec[i] = min(vec[i], vec[i / 2] + 1);

		if (i % 3 == 0)
			vec[i] = min(vec[i], vec[i / 3] + 1);
	}
		
	return vec[n];
}

int main()
{
	ios::sync_with_stdio(false);
	cin.tie(0);
	cout.tie(0);

	int n;
	cin >> n;
	cout << dp(n);
}
#include <iostream>
#include <vector>
using namespace std;

int dp(int n)
{
	vector<int> vec(n + 1);
	vec[0] = 1;
	vec[1] = 3;

	for (int i = 2; i <= n; i++)
		vec[i] = (vec[i - 1] + vec[i - 2] * 2) % 10007;

	return vec[n - 1];
}

int main()
{
	int n; 
	cin >> n;
	cout << dp(n);
}
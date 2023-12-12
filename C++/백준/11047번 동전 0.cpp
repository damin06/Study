#include <iostream>
#include <vector>
#include <algorithm>
using namespace std;

int main() 
{
	int n, k, input, minTotal, answer = 0;
	vector<int> coins;
	cin >> n >> k;

	for(int i = 0; i < n; i++)
	{
		cin >> input;
		coins.push_back(input);
	}

	sort(coins.begin(), coins.end(), greater<>());

	for (int i = 0; i < n; i++) 
	{
		answer += k / coins[i];
		k %= coins[i];
	}

	cout << answer;
}
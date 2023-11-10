#include <iostream>
#include <vector>
#include <algorithm>
using namespace std;

long long binarySearch(vector<int> cables, int n)
{
	long long left = 1, right = cables[cables.size() - 1], mid = 0, curCount, answer = 0;

	while(left <= right)
	{
		mid = (left + right) / 2;
		curCount = 0;

		for(int i = 0; i < cables.size(); i++)
			curCount += cables[i] / mid;

		if (n <= curCount)
		{
			left = mid + 1;
			answer = max(answer, mid);
		}
		else
			right = mid - 1;
	}

	return answer;
}

int main()
{
	ios_base::sync_with_stdio(0);
	cin.tie(0);
	cout.tie(0);
	int k, n, input;
	vector<int> cables;

	cin >> k >> n;

	for(int i = 0; i < k; i++)
	{
		cin >> input;
		cables.push_back(input);
	}

	sort(cables.begin(), cables.end());

	cout << binarySearch(cables, n);
}
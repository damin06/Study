#include <iostream>
#include <algorithm>
#include <vector>
using namespace std;

int main() 
{
	int n, inputEnd, inputStart, answer = 0;
	vector<pair<int, int>> favor;
	int last = 0;

	cin >> n;

	for (int i = 0; i < n; i++) 
	{
		cin >> inputStart >> inputEnd;
		favor.push_back(make_pair(inputEnd, inputStart));
	}

	sort(favor.begin(), favor.end());

	for(int i = 0; i < n; i++)
	{
		if (favor[i].second < last)
			continue;
		answer++;
		last = favor[i].first;
	}

	cout << answer;
}
#include <algorithm>
#include <iostream>
#include <string>
#include <vector>
#include <map>
using namespace std;

int main() 
{
	ios::sync_with_stdio(false);
	cin.tie(0);
	cout.tie(0);
	int n, m;
	string input;
	map<string, int> names;
	vector<string> answer;
	cin >> n >> m;

	for(int i = 0; i < n + m; i++)
	{
		cin >> input;
		names[input]++;
		if (names[input] > 1)
			answer.push_back(input);
	}
	
	sort(answer.begin(), answer.end());
	cout << answer.size() << "\n";
	for (const auto name : answer)
		cout << name << "\n";
}
#include <unordered_map>
#include <iostream>
#include <string>
#include <vector>
using namespace std;

int main()
{
	unordered_map<string, int> indexMap;
	unordered_map<int, string> nameMap;
	vector<string> answers;
	int n, m;
	string input;

	cin >> n >> m;

	for(int  i = 1; i <= n; i++)
	{
		cin >> input;
		indexMap.insert(make_pair(input, i));
		nameMap.insert(make_pair(i, input));
	}

	for(int i = 0; i < m; i++)
	{
		cin >> input;

		if (input[0] >= 65 && input[0] <= 90)
			answers.push_back(to_string(indexMap[input]));
		else
			answers.push_back(nameMap[stoi(input)]);
	}

	for (int i = 0; i < answers.size(); i++)
		cout << answers[i] << "\n";
}
#include <iostream>
#include <vector>
#include <string>
#include <algorithm>
using namespace std;

bool ischeck(const string str)
{
	vector<char> v;
	for(int i = 0; i < str.length(); i++)
	{
		if(find(v.begin(), v.end(), str[i]) == v.end())
		{
			v.push_back(str[i]);
			continue;
		}

		if (str[i] == str[i - 1])
			continue;
		
		return false;
	}

	return true;
}

int main()
{
	int n, answer = 0;
	string input;

	cin >> n;

	for(int i = 0; i < n; i++)
	{
		cin >> input;
		answer += ischeck(input) ? 1 : 0;
	}

	cout << answer;
}
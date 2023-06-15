#include <iostream>
#include <string>
#include <map>
#include <vector>
using namespace std;

string sort(map<string, int>& map);

int main()
{
	int n;
	string input;
	map<string, int> book;

	cin >> n;

	for(int i = 0; i < n; i++)
	{
		cin >> input;
		
		if(book.find(input) != book.end())
		{
			book[input] ++;
		}
		else
		{
			book.insert({ input, 1 });
		}
	}

	cout << sort(book);
	
}

string sort(map<string, int>& map)
{
	int k = 0;
	string str;

	for (auto s : map)
	{
		if (s.second > k)
		{
			k = s.second;
			str = s.first;
		}
	}
	return str;
}

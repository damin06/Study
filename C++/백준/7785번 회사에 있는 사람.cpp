#include <iostream>
#include <set>
using namespace std;

int main() 
{
	set<string, greater<string>> s;
	int n;
	string name, input;

	cin >> n;

	for (int i = 0; i < n; i++) 
	{
		cin >> name >> input;
		if (input == "enter")
			s.insert(name);
		else
			s.erase(name);
	}

	for (const auto str : s)
		cout << str << "\n";
}
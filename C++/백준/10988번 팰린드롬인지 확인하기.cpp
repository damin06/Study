#include <iostream>
using namespace std;

int main()
{
	string str;
	cin >> str;

	for(int i = 0; i < str.length() / 2; i++)
	{
		if(str[i] != str[str.length()-i-1])
		{
			cout << 0;
			return 0;
		}
	}
	cout << 1;
}
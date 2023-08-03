#include <iostream>
using namespace std;

int f(int n)
{
	if (n <= 1)
	{
		return n;
	}
	else
	{
		return f(n - 1) + f(n - 2);
	}
}

int main()
{
	string s;
	int a;
	cin >> s >> a;

	cout << s[a - 1];

}
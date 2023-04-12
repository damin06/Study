#include <iostream>
using namespace std;

int main()
{
	int count = 0;
	cin >> count;

	for(int i =count; 0<i; i--)
	{
		for(int j=0; j<i; j++)
		{
			cout << "*";
		}
		cout << endl;
	}
}
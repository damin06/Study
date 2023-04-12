#include <iostream>
using namespace std;

int main()
{
	int count;
	cin >> count;

	for(int i=0; i<count; i++)
	{
		for(int k=0; k<i; k++)
		{
			cout << " ";
		}

		for(int j=0; j<count-i; j++)
		{
			cout << "*";
		}

		cout << endl;
	}
}
#include <iostream>
using namespace std;

int main() 
{
	int pos = 0, max = -1000000;
	int arr[9];

	for (int i = 0; i < 9; i++)
	{
		cin >> arr[i];

		if(arr[i] > max)
		{
			max = arr[i];
			pos = i;
		}
	}

	cout << max << " " << pos+1;


	return 0;
}

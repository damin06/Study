#include <iostream>
#include <vector>
using namespace std;
long long int arr[100] = { 1,1,1,2,2 };

int main()
{
	int n, t;
	cin >> t;
	for(int i = 5; i < 101; i++)
	{
		arr[i] = arr[i - 1] + arr[i - 5];
	}

	for(int j = 0; j < t; j++)
	{
		cin >> n;
		cout << arr[n -1] << "\n";
	}
}
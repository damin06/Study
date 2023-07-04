#include <iostream>
#include <algorithm>
using namespace std;

int main()
{
	int n, x, y;
	int arr[3] = { 1, 2, 3 };
	cin >> n;

	for(int i = 0; i < n; i++)
	{
		cin >> x >> y;
		swap(arr[x - 1], arr[y - 1]);
	}

	for (int i = 0; i < 3; i++) 
		if (arr[i] == 1) cout << i + 1;
}

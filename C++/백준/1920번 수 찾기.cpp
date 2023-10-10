#include <iostream>
#include <vector>
#include <algorithm>
using namespace std;
vector<int> arr;
int n, m;

void binarySearch(int num)
{
	int left = 0, right = n - 1, mid;

	while (left <= right)
	{
		mid = (left + right) / 2;
		if (arr[mid] == num)
		{
			cout << '1' << "\n";
			return;
		}
		else if (arr[mid] > num)
		{
			right = mid - 1;
		}
		else
		{
			left = mid + 1;
		}
	}
	cout << '0' << "\n";
}

int main()
{
	ios_base::sync_with_stdio(0); cin.tie(0);
	int input;
	
	cin >> n;
	for(int i = 0; i < n; i++)
	{
		cin >> input;
		arr.push_back(input);
	}
	sort(arr.begin(), arr.end());

	cin >> m;
	for(int i = 0; i < m; i++)
	{
		cin >> input;
		binarySearch(input);
	}

	return 0;
}
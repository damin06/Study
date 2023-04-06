#include <iostream>
#include <vector>
#include<algorithm>
using namespace std;

int main()
{
	vector<int> money;
	int n, j;
	long long int total = 0;
	cin >> n;

	for(int i=0; i<n; i++)
	{
		cin >> j;
		money.push_back(j);
	}

	sort(money.begin(), money.end(), greater<int>());
	for (int i=0; i<n; i++) 
	{
		if (money[i] - i > 0) 
		{

			total += (money[i] - i);
		}
	}

	cout << total;
}
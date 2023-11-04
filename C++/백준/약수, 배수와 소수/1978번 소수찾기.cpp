#include <iostream>
using namespace std;

int main()
{
	int n, input, total = 0, cnt = 0;
	
	cin >> n;

	for(int i = 0; i < n; i++)
	{
		cin >> input;
		cnt = 0;

		for (int i = 1; i <= input; i++)
			cnt += input % i == 0 ? 1 : 0;

		total += cnt == 2 ? 1 : 0;
	}
	cout << total;
}
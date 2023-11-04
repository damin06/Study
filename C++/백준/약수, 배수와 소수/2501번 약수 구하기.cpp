#include <iostream>
#include <vector>
using namespace std;

int main()
{
	int n1, n2;
	vector<int> vec;

	cin >> n1 >> n2;	

	for(int i = 1; i <= n1; i++)
	{
		if (n1 % i == 0)
			vec.push_back(i);
	}

	if (n2 > vec.size())
		cout << 0;
	else
		cout << vec[n2 - 1];
}
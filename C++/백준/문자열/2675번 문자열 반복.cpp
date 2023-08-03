#include <iostream>
#include <vector>
using namespace std;

int main()
{
	int count;
	cin >> count;
	for (int i = 0; i < count; i++)
	{
		int d;
		string g;
		vector <char>k;
		cin >> d >> g;


		for (int i = 0; i < g.length(); i++)
		{
			for (int j = 0; j < d; j++)
			{
				k.push_back(g[i]);
			}
		}

		for (int i = 0; i < k.size(); i++)
		{
			cout << k[i];
		}
		cout << endl;
	}

}
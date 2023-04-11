#include <iostream>
#include <string>
using namespace std;

int main()
{
	int n;
	string note[100];
	
	cin >> n;

	for(int i=0; i<n; i++)
	{
		cin >> note[i];
	}

	for (int i = 1; i < n; i++)
	{
		for(int j=0; j<note[i].length(); j++)
		{
			if(note[0][j] != note[i][j])
			{
				note[0][j] = '?';
			}
		}
	}

	cout << note[0];
}
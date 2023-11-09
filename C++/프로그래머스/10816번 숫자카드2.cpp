#include <iostream>
#include <unordered_map>
using namespace std;

int main()
{
	ios_base::sync_with_stdio(0);
	cin.tie(0);
	int n, input;
	unordered_map<int, int> cards;

	cin >> n;

	for (int i = 0; i < n; i++)
	{
		cin >> input;
		cards[input]++;
	}

	cin >> n;

	for (int i = 0; i < n; i++)
	{
		cin >> input;
		cout << cards[input] << " ";
	}
}
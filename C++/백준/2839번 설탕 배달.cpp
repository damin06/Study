#include <iostream>
using namespace std;

int main() 
{
	int n, answer = 0;
	cin >> n;

	while (n >= 0)
	{
		if(n % 5 == 0)
		{
			answer += n / 5;
			cout << answer;
			return 0;
		}
		answer++;
		n -= 3;
	}

	cout << -1;

}
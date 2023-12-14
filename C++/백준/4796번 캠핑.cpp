#include <iostream>
using namespace std;

int main() 
{
	cout.tie(0);
	cin.tie(0);
	int L, P, V;
	int cnt = 1;

	while (true)
	{
		cin >> L >> P >> V;
		if (L == 0 && P == 0 && V == 0)
			break;
		int use = V / P;
		int remain = V % P;
		if (L < remain) remain = L;
		

		int result = L * use + remain;

		cout << "Case " << cnt << ": " << result << endl;
		cnt++;
	}
}
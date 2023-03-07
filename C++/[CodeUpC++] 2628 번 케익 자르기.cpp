#include <iostream>
using namespace std;

int main() {
	int a = 0;
	int b = 0;
	int c = 0;
	int d = 0;
	int e = 0;

	cin >> a;
	cin >> b;
	cin >> c;
	cin >> d;

	if (a > b) //a가 b보다 크면 a와 b의 값을 바꿔 준다.
	{
		e = a;
		a = b;
		b = e;
	}
	else if (c > d) //c가 d보다 크면 c와 d의 값을 바꿔 준다.
	{
		e = c;
		c = d;
		d = e;
	}


	if (c > a && c < b) //c가 a보다 크고 b보다 작으면(c가 a와 b 사이에 있으면)
	{
		if (d < a || d > b) //d가 a보다 작거나 b보다 크면
		{
			cout << "cross";
		}
		else
		{
			cout << "not cross";
		}
	}
	else if (d > a && d < b) //d가 a보다 크고 b보다 작으면(d가 a와 b 사이에 있으면)
	{
		if (c < a || c > b) //c가 a보다 작거나 b보다 크면
		{
			cout << "cross";
		}
		else
		{
			cout << "not cross";
		}
	}
	else //앞의 모든 조건에 해당이 안된다면
	{
		cout << "not cross";
	}
}
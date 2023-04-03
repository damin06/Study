#include <iostream>
using namespace std;
int d[100] = { 0,1,1 };

int f(int n)
{
	if(d[n] != 0)
	{
		return d[n];
	}
	else
	{
		d[n] = f(n - 2) + f(n - 1);
		return d[n];
	}
}

int main()
{
	int num = 0;
	cout << "팩토리얼 값을 구하고 싶은 숫자를 입력하시오 : ";
	cin >> num;
	cout << f(num);
}
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
	cout << "���丮�� ���� ���ϰ� ���� ���ڸ� �Է��Ͻÿ� : ";
	cin >> num;
	cout << f(num);
}
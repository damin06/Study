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

	if (a > b) //a�� b���� ũ�� a�� b�� ���� �ٲ� �ش�.
	{
		e = a;
		a = b;
		b = e;
	}
	else if (c > d) //c�� d���� ũ�� c�� d�� ���� �ٲ� �ش�.
	{
		e = c;
		c = d;
		d = e;
	}


	if (c > a && c < b) //c�� a���� ũ�� b���� ������(c�� a�� b ���̿� ������)
	{
		if (d < a || d > b) //d�� a���� �۰ų� b���� ũ��
		{
			cout << "cross";
		}
		else
		{
			cout << "not cross";
		}
	}
	else if (d > a && d < b) //d�� a���� ũ�� b���� ������(d�� a�� b ���̿� ������)
	{
		if (c < a || c > b) //c�� a���� �۰ų� b���� ũ��
		{
			cout << "cross";
		}
		else
		{
			cout << "not cross";
		}
	}
	else //���� ��� ���ǿ� �ش��� �ȵȴٸ�
	{
		cout << "not cross";
	}
}
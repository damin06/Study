#include <iostream>
#include <string>
#include<time.h>
using namespace std;



int main()
{
#pragma region ���� ���߱� ����
 	   srand((unsigned int)time(NULL));
	int a = rand() % 101;
	int b = 0;
	int c = 0;

	while (true)
	{
		cout << "������ �����Ͽ� ���ÿ� : ";
		cin >> b;
		c++;
		while (true)
		{
			if (a == b)
			{
				cout << "�����մϴ�. �õ�Ƚ��=" << c;
				break;
			}
			else
			{
				if(b > a)
				{
					cout << "������ ������ �����ϴ�." << endl;
				}
				else
				{
					cout << "������ ������ �����ϴ�." << endl;
				}
				break;
			}
		}

		if (a == b)
		{
			break;
		}
	}
#pragma endregion

#pragma region ������ ���� ���� ����

	char go = ' ';
	int f = 0;
	int g = 0;

	cout << "�����ڸ� �Է��ϰ� ��Ʈ�� 0�� ġ����" << endl;
	while (go != '0')
	{
		cin >> go;


		while (true)
		{
			switch (go)
			{
			case 'a': case 'e': case 'i': case 'o': case 'u':
				f++;
				break;
			default :
				g++;
				break;
			}

			break;
		}
			
		if (go == '0')
		{
			cout << "���� : " << f << endl;
			cout << "���� : " << g << endl;
			break;
		}
	}
	cout << "���� : " << f << endl;
	cout << "���� : " << g << endl;

#pragma endregion


#pragma region ������
	int d = 0;
	cout << "������ �߿��� ����ϰ� ���� ���� �Է��Ͻÿ�: ";
	cin >> d;
	for(int i=1; i<9; i++)
	{
		cout << d << "*" << i << "=" << d * i << endl;
	}
#pragma endregion

#pragma region ��� ���� �ڵ� ����
	srand((unsigned int)time(NULL));
	int randa = rand() % 100 ;
	int randb = rand() % 100 ;
	
	int h=0;
	int k = randa + randb;

	cout << randa << "+" << randb << "=";

	while (true)
	{
		cin >> h;

			if(h == k)
			{
				cout << "�¾ҽ��ϴ�" << endl;
				break;
			}
			else
			{
				cout << "Ʋ�Ƚ��ϴ�" << endl;
				
			}

		
	}
#pragma endregion
}
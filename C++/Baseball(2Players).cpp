#pragma region �߱�
#include <iostream>
#include<Windows.h>
using namespace std;


int GetStrike(int a[3], int c[3]) //��Ʈ����ũ �� ���ϱ�
{
	int strike = 0;
	for(int i = 0; i < 3; i++)
	{
		if(a[i] == c[i])
		{
			strike++;
		}
	}
	return strike;
}

int GetBall(int a[3], int c[3]) //�� �� ���ϱ�
{
	int ball = 0;
	for(int i=0; i<3; i++)
	{
		for(int j =0; j<3; j++)
		{
			if(a[i] == c[j] && a[i] != c[i])
			{
				ball++;
			}
		}
	}
	return ball;
}


int main()
{
	int a[3]; //�÷��̾� 1
	int b[3]; //�÷��̾� 2
	int c[3]; // �Է¹��� ��
	int count = 1; //����

	cout << "�÷��̾�1\n���ڸ� �Է��Ͻÿ� : ";
	for(int i=0; i<3; i++)
	{
		cin >> a[i];
		if(a[i] <= 0 || a[i] > 9)
		{
			cout << "������ �ʰ��Ͽ����ϴ�.";
			return 0;
		}
		else if(a[0] == a[1] == a[2])
		{
			cout << "������";
			return 0;
		}
	}

	system("cls");

	cout << "�÷��̾�2\n���ڸ� �Է��Ͻÿ� : ";
	for (int i = 0; i < 3; i++)
	{
		cin >> b[i];
		if (b[i] <= 0 || b[i] > 9)
		{
			cout << "�ߺ��Ǵ� ���ڰ� �ֽ��ϴ�.";
			return 0;
		}
		else if (b[0] == b[1] == b[2])
		{
			cout << "�ߺ��Ǵ� ���ڰ� �ֽ��ϴ�.";
			return 0;
		}
	}

	system("cls");

	while (true)
	{
		system("cls");
		if(count % 2 == 1)
		{
			cout << "�÷��̾�1 ����" << endl;
		}
		else
		{
			cout << "�÷��̾�2 ����" << endl;
		}

		cin >> c[0] >> c[1] >> c[2];

		if(GetStrike((count % 2 == 1) ? b : a, c) > 0)
		{
			if(GetStrike((count % 2 == 1) ? b : a, c) == 3)
			{
				if(count % 2 == 1)
				{
					cout << "�÷��̾�1 ��!";
					return 0;
				}
				else
				{
					cout << "�÷��̾�2 ��!";
					return 0;
				}
			}
			cout << GetStrike((count % 2 == 1) ? b : a, c) << "STRIKE" << endl;
		}

		if (GetBall((count % 2 == 1) ? b : a, c) > 0)
		{
			cout << GetBall((count % 2 == 1) ? b : a, c) << "BALL" << endl;
		}

		system("pause");
		count++;
	}
	
}
#pragma endregion

#pragma region ���� ���
#include <iostream>
#include <time.h>
#include<Windows.h>
using namespace std;



void GetRanDomNum(int Time,int Count,int *arry)
{
	system("cls");
	srand((unsigned int)time(NULL));
	for(int i = 0; i<Count; i++)
	{
		arry[i] = rand() % 100 + 1;
		cout << arry[i] << endl;
		Sleep(Time);
	}
	system("cls");
}


enum Mode
{
	EASY,
	NORMAL,
	HARD
};

int main()
{
	Mode mod;
	int select = 0;
	int Time = 0;
	int count = 0;
	int arry[15];

	cout << "============================================" << endl;
	cout << "���� ��� �����Դϴ�. ��带 �����ϼ���." << endl;
	cout << "EASY: 1  " << "NORMAL: 2  " << "HARD: 3" << endl;
	cout << "============================================" << endl;


	cin >> select;
	select--;

	switch (select)
	{
	case EASY:
		{
			Time = 1000;
			count = 5;
		}
		break;
	case NORMAL:
		{
			Time = 700;
			count = 10;
		}
		break;
	case HARD:
		{
			Time = 500;
			count = 15;
		}
		break;
	default:
		cout << "�ٽ� �Է��ϼ���";
		return 0;
		break;
	}

	GetRanDomNum(Time, count, arry);
	
	int j = 0;

	for(int i=0; i< count; i++)
	{
		cin >> j;
		if(arry[i] != j)
		{
			cout << "��! Ʋ�Ƚ��ϴ�.";
			return 0;
		}
	}

	cout << "�����մϴ�. ��� ���߼̳׿�";
}
#pragma endregion
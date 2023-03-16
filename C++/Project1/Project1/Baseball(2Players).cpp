#pragma region 야구
#include <iostream>
#include<Windows.h>
using namespace std;


int GetStrike(int a[3], int c[3]) //스트라이크 수 구하기
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

int GetBall(int a[3], int c[3]) //볼 수 구하기
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
	int a[3]; //플레이어 1
	int b[3]; //플레이어 2
	int c[3]; // 입력받을 값
	int count = 1; //라운드

	cout << "플레이어1\n숫자를 입력하시오 : ";
	for(int i=0; i<3; i++)
	{
		cin >> a[i];
		if(a[i] <= 0 || a[i] > 9)
		{
			cout << "범위를 초과하였습니다.";
			return 0;
		}
		else if(a[0] == a[1] == a[2])
		{
			cout << "같은수";
			return 0;
		}
	}

	system("cls");

	cout << "플레이어2\n숫자를 입력하시오 : ";
	for (int i = 0; i < 3; i++)
	{
		cin >> b[i];
		if (b[i] <= 0 || b[i] > 9)
		{
			cout << "중복되는 숫자가 있습니다.";
			return 0;
		}
		else if (b[0] == b[1] == b[2])
		{
			cout << "중복되는 숫자가 있습니다.";
			return 0;
		}
	}

	system("cls");

	while (true)
	{
		system("cls");
		if(count % 2 == 1)
		{
			cout << "플레이어1 차례" << endl;
		}
		else
		{
			cout << "플레이어2 차례" << endl;
		}

		cin >> c[0] >> c[1] >> c[2];

		if(GetStrike((count % 2 == 1) ? b : a, c) > 0)
		{
			if(GetStrike((count % 2 == 1) ? b : a, c) == 3)
			{
				if(count % 2 == 1)
				{
					cout << "플레이어1 승!";
					return 0;
				}
				else
				{
					cout << "플레이어2 승!";
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

#pragma region 숫자 기억
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
	cout << "숫자 기억 게임입니다. 모드를 선택하세요." << endl;
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
		cout << "다시 입력하세요";
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
			cout << "땡! 틀렸습니다.";
			return 0;
		}
	}

	cout << "축하합니다. 모두 맞추셨네요";
}
#pragma endregion
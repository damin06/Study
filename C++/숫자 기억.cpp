#include <iostream>
#include <time.h>
#include<Windows.h>
using namespace std;


void GameManager(int count, int time, int arry[15])
{
	int input[15];
	system("cls");
	for (int i = 0; i < count; i++)
	{
		cout << arry[i] << endl;
		Sleep(time);
	}
	system("cls");
	
	for(int i=0; i< count; i++)
	{
		cin >> input[i];
		if(input[i] != arry[i])
		{
			cout << "땡! 틀렸습니다.";
			return;
		}
;	}
	cout << "축하합니다. 모두 맞추셨네요";
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

	srand((unsigned int)time(NULL));

	for(int i=0; i<count; i++)
	{
		int ran = rand() % 100 + 1;
		arry[i] = ran;
	}

	GameManager(count, Time, arry);
}


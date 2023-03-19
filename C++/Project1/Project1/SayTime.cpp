#include <iostream>
#include <Windows.h>
#include <conio.h>
using namespace std;

void Init(int* num)
{
	srand((unsigned int)time(NULL));
	for(int i =0; i< 24; i++)
	{
		num[i] = i + 1;
	}
	num[25] = INT_MAX;
	//섞는다
	int iTemp, idx1, idx2;
	for(int i=0; i< 100; i++)
	{
		idx1 = rand() % 24;
		idx2 = rand() % 24;
		iTemp = num[idx1];
		num[idx1] = num[idx2];
		num[idx1] = iTemp;
	}
}

void RenderNumber(int num[25])
{
	cout << "=============================================" << endl;
	cout << "\t숫자 퍼즐 게임 입니다.\t" << endl;
	cout << "=============================================" << endl;
	cout << "*를 움직여서 1부터 24까지 순서대로 되도록 퍼즐을 맞춰 보세요." << endl;
	cout << "=============================================" << endl;

	for(int i=0; i<5; i++)
	{
		for (int j = 0; j < 5; j++) 
		{
			if(num[i*5+j] == INT_MAX)
			{
				cout << "*\t";
			}
			else
			{
				cout << num[i * 5 + j] << "\t";

			}
		}
		cout << endl;
	}
}

void Update(int* num)
{
	cout << "w: 위, s: 아래, a: 왼쪽, d: 오른쪽, q:종료" << endl;
}

int main()
{
	int Number[25] = {};
	Init(Number);
	
	while (true)
	{
		system("cls");
		RenderNumber(Number);
	} //배열(1차원 배열)-셔플알고리즘
							//입력
}
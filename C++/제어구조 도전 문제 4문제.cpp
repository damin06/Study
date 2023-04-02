#include <iostream>
#include <string>
#include<time.h>
using namespace std;



int main()
{
#pragma region 숫자 맞추기 게임
 	   srand((unsigned int)time(NULL));
	int a = rand() % 101;
	int b = 0;
	int c = 0;

	while (true)
	{
		cout << "정답을 추측하여 보시오 : ";
		cin >> b;
		c++;
		while (true)
		{
			if (a == b)
			{
				cout << "축하합니다. 시도횟수=" << c;
				break;
			}
			else
			{
				if(b > a)
				{
					cout << "제시한 정수가 높습니다." << endl;
				}
				else
				{
					cout << "제시한 정수가 낮습니다." << endl;
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

#pragma region 자음과 모음 개수 세기

	char go = ' ';
	int f = 0;
	int g = 0;

	cout << "영문자를 입력하고 콘트롤 0를 치세요" << endl;
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
			cout << "모음 : " << f << endl;
			cout << "자음 : " << g << endl;
			break;
		}
	}
	cout << "모음 : " << f << endl;
	cout << "자음 : " << g << endl;

#pragma endregion


#pragma region 구구단
	int d = 0;
	cout << "구구단 중에서 출력하고 싶은 단을 입력하시오: ";
	cin >> d;
	for(int i=1; i<9; i++)
	{
		cout << d << "*" << i << "=" << d * i << endl;
	}
#pragma endregion

#pragma region 산술 문제 자동 출제
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
				cout << "맞았습니다" << endl;
				break;
			}
			else
			{
				cout << "틀렸습니다" << endl;
				
			}

		
	}
#pragma endregion
}
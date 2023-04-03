#include <iostream>
using namespace std;

int main()
{


	int a[9] = { 1,2,3,4,5,6,7,8,9 };
	int b[3];

	srand((unsigned int)time(NULL));
	int idx1, idx2, temp;
	for(int i=0; i < 100; i++)
	{
		idx1 = rand() % 9; // 0 ~ 8
		idx2 = rand() % 9;
		temp = a[idx1];
		a[idx1] = a[idx2];
		a[idx2] = temp;
	}

	for(int i=0; i<3; i++)
	{
		b[i] = a[i];
	}
	
	int strike = 0, ball = 0;
	int IGamecount = 1;
	int iInputnumber[3];

	while (true)
	{
		ball = 0;
		strike = 0;
		cout << IGamecount << "회" << endl;
		cout << "1~9 사이의 숫자 중 숫자 3개를 입력하세요.(0:종료)";
		cin >> iInputnumber[0] >> iInputnumber[1] >> iInputnumber[2];
		while (true)
		{
			for (int i = 0; i < 3; i++)
			{
				if (iInputnumber[i] <= 0 || iInputnumber[i] > 9)
				{
					return 0;
				}

				if(b[i] == iInputnumber[i])
				{
					strike++;
					if(strike == 3)
					{
						cout << "WIN!";
						return 0;
					}
				}

				for(int g=2; g >= 0; g--)
				{
					if(b[i] == iInputnumber[g] && b[i] != iInputnumber[i])
					{
						ball++;
					}
				}
			}
			break;
		}
		if(strike !=0)
		{
			cout << strike << "스트라이크" << endl;
		}

		if(ball !=0)
		{
			cout << ball << "볼" << endl;
		}
		IGamecount++;
	}

}



//#include <iostream>
//#include<Windows.h>
//using namespace std;
//
//void baseball(int a[3], int b[3])
//{
//	int count = 1;
//	int c[3];
//	int d[3];
//
//	int strike = 0;
//	int ball = 0;
//	char ch = ' ';
//
//	while (true)
//	{
//		system("cls");
//
//		strike = 0;
//		ball = 0;
//		if (count % 2 == 1)
//		{
//			cout << "이재명 차례" << endl;
//		}
//		else
//		{
//			cout << "윤석열 차례" << endl;
//		}
//
//		cin >> c[0] >> c[1] >> c[2];
//
//		while (true)
//		{
//			for (int i = 0; i < 3; i++)
//			{
//				d[i] = (count % 2 == 1) ? b[i] : a[i];
//
//			}
//
//			for (int i = 0; i < 3; i++)
//			{
//				if (c[i] == d[i])
//				{
//					strike++;
//				}
//
//				if (strike == 3)
//				{
//
//					if (count % 2 == 1)
//					{
//						cout << "이재명 승!" << endl;
//					}
//					else
//					{
//						cout << "윤석열 승!" << endl;
//					}
//					return;
//				}
//
//				for (int j = 2; j > 0; j--)
//				{
//					if (c[j] == d[i] && c[i] != d[i])
//					{
//						ball++;
//					}
//				}
//			}
//			break;
//		}
//		count++;
//		if (strike > 0)
//		{
//			cout << strike << "STRIKE" << endl;
//		}
//
//		if (ball > 0)
//		{
//			cout << ball << "BALL" << endl;
//		}
//
//		system("pause");
//		//cout << (strike > 0) ? strike : NULL << (ball > 0) ? ball : NULL;
//	}
//}
//
//int main()
//{
//	int a[3];
//	int b[3];
//
//	cout << "이재명 : 숫자를 입력하시오 : ";
//	for (int i = 0; i < 3; i++)
//	{
//		cin >> a[i];
//		if (a[i] <= 0 || a[i] > 9)
//		{
//			cout << "범위를 초과하였습니다.";
//			return 0;
//		}
//	}
//
//	system("cls");
//
//	cout << "윤석열 : 숫자를 입력하시오 : ";
//	for (int i = 0; i < 3; i++)
//	{
//		cin >> b[i];
//		if (b[i] <= 0 || b[i] > 9)
//		{
//			cout << "범위를 초과하였습니다.";
//			return 0;
//		}
//	}
//
//	system("cls");
//
//	baseball(a, b);
//}
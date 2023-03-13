#include <iostream>
#include <array>
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
			cout << strike << "strike" << endl;
		}

		if(ball !=0)
		{
			cout << ball << "ball" << endl;
		}
		IGamecount++;
	}

}
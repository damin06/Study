#include <iostream>
#include <vector>
using namespace std;

int main() 
{

//#pragma region 최대값
//	int a[10];
//	int b = 0;
//	srand((unsigned int)time(NULL));
//	for(int i=0; i<10; i++)
//	{
//		int rande = rand() % 100 + 1;
//		a[i] = rande;
//		cout << a[i] << endl;
//
//		if(a[i] > b)
//		{
//			b = a[i];
//		}
//	}
//	cout << "최대값=" << b << endl;
//#pragma endregion 

#pragma region 틱택토
	char c[3][3];
	int count = 1;
	int x = 0;
	int y = 0;

	for (int i = 0; i < 3; i++)
	{
		for (int j = 0; j < 3; j++)
		{
			c[i][j] = ' ';
		}
	}
	while (true)
	{
		cout << "(x,y) 좌표";
		cin >> x >> y;
		while (true)
		{
			if (x < 0 || x > 2 || y < 0 || y > 2)
			{
				cout << "범위를 초과함" << endl;
				break;
			}
			else if (c[x][y] != ' ')
			{
				cout << "이미 있음" << endl;
				break;
			}

			c[x][y] = (count % 2 == 0) ? 'o' : 'x';

			count++;
			break;
		}

		for (int i = 0; i < 3; i++)
		{
			cout << "---|---|---" << endl;
			for (int j = 0; j < 3; j++)
			{
				if (j >= 2)
				{
					cout << " " << c[i][j] << " ";
				}
				else
				{
					cout << " " << c[i][j] << " |";
				}

			}
			cout << endl;
		}
	}
#pragma endregion

//#pragma region 성적 평균 계산하기
//
//	vector<int> v1;
//	int k = 0;
//	int sum = 0;
//
//	while (true)
//	{
//		cout << "성적을 입력하시오(종료는 0) : ";
//		cin >> k;
//		sum += k;
//		if(k == 0)
//		{	
//			cout << "성적 평균=" << sum / v1.size();
//			return 0;
//		}
//		v1.push_back(k);
//
//	}
//
//#pragma endregion

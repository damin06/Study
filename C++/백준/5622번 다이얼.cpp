#include <string>
#include <iostream>
using namespace std;

int main()
{
	int time = 0;	   // 시간을 저장할 변수 초기화
	char phone[9][4] = // 전화기의 각 숫자에 해당하는 문자들을 저장한 2차원 배열
		{
			{'A', 'B', 'C'},
			{'D', 'E', 'F'},
			{'G', 'H', 'I'},
			{'J', 'K', 'L'},
			{'M', 'N', 'O'},
			{'P', 'Q', 'R', 'S'},
			{'T', 'U', 'V'},
			{'W', 'X', 'Y', 'Z'},
		};

	string str; // 입력받은 문자열을 저장할 변수
	cin >> str; // 문자열 입력받기

	for (int i = 0; i < str.length(); i++) // 문자열의 길이만큼 반복
	{
		for (int j = 0; j < 9; j++) // 전화기의 숫자 1~9까지 반복
		{
			for (int k = 0; k < 4; k++) // 각 숫자에 해당하는 문자들을 반복
			{
				if (phone[j][k] == str[i]) // 입력받은 문자열의 i번째 문자가 전화기의 j번째 숫자에 해당하는 문자인 경우
				{
					time += j + 3; // 시간에 j+3을 더함
					break;		   // 반복문 탈출
				}
			}
		}
	}

	cout << time; // 시간 출력
}
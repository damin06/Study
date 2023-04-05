#include <iostream>
#include <Windows.h>
#include <conio.h>
using namespace std;

void SoundPlay(int n, bool show)
{
	switch (n)
	{
	case 1:
		show ? cout << "도(1) " : cout << "";
		Beep(523.25, 500);
		break;
	case 2:
		show ? cout << "레(2) " : cout << "";
		Beep(587.33, 500);
		break;
	case 3:
		show ? cout << "미(3) " : cout << "";
		Beep(659.26, 500);
		break;
	case 4:
		show ? cout << "파(4) " : cout << "";
		Beep(698.46, 500);
		break;
	case 5:
		show ? cout << "솔(5) " : cout << "";
		Beep(783.99, 500);
		break;
	case 6:
		show ? cout << "라(6) " : cout << "";
		Beep(880, 500);
		break;
	case 7:
		show ? cout << "시(7) " : cout << "";
		Beep(987.77, 500);
		break;
	case 8:
		show ? cout << "도(8) " : cout << "";
		Beep(1046.50, 500);
		break;
	default:
		break;
	}
}

void Init()
{
	cout << "------------------------------------------" << endl;
	cout << " | 절대 음감 게임 | " << endl;
	cout << "------------------------------------------" << endl;
	cout << "설명 : 처음에 8음계를 들려주고.\n 다음에 8개 중 한 개의 음을 들려 준다.\n 그리고 그 음을 번호로 맞힌다." << endl;
	cout << "-----------------------------------------------------------------" << endl;
	cout << "준비하고 아무 키나 누른다" << endl;
	cout << "------------------------------------------" << endl;
	system("PAUSE");


	for (int i = 1; i < 9; i++)
	{
		SoundPlay(i, true);
	}
	cout << "\n------------------------------------------" << endl;
	for (int i = 3; i > 0; i--)
	{
		for (int j = 0; j < 4; j++)
		{
			cout << "\b";

		}
		cout << i << "...";
		Sleep(1000);
	}
	system("cls");
}

int main()
{
	int input;

	Init();

	while (true)
	{
		cout << "!!!준비!!!" << endl;
		Sleep(1000);

		int random = rand() % 8 + 1;
		SoundPlay(random, false);
		system("cls");

		input = _getch() - '0'; // 문자 '0'을 빼주어 숫자로 변환
		cout << input << endl;

		if (input <= 0 || input > 8)
		{
			cout << "값이 범위를 초과하였습니다." << endl;
			continue;
		}

		if (random == input)
		{
			cout << "정답입니다." << endl;
		}
		else
		{
			cout << "틀렸습니다." << endl;
		}

		while (true)
		{
			cout << "게임을 그만둘까요? (Y/N)" << endl;
			char ch = _getch();

			if (ch == 'N' || ch == 'n')
			{
				break;
			}
			else if (ch == 'Y' || ch == 'y')
			{
				return 0;
			}

			system("cls");
		}

		system("cls");
	}
}
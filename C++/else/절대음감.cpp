#include <iostream>
#include <Windows.h>
#include <conio.h>
using namespace std;

void SoundPlay(int n, bool show)
{
	switch (n)
	{
	case 1:
		show ? cout << "��(1) " : cout << "";
		Beep(523.25, 500);
		break;
	case 2:
		show ? cout << "��(2) " : cout << "";
		Beep(587.33, 500);
		break;
	case 3:
		show ? cout << "��(3) " : cout << "";
		Beep(659.26, 500);
		break;
	case 4:
		show ? cout << "��(4) " : cout << "";
		Beep(698.46, 500);
		break;
	case 5:
		show ? cout << "��(5) " : cout << "";
		Beep(783.99, 500);
		break;
	case 6:
		show ? cout << "��(6) " : cout << "";
		Beep(880, 500);
		break;
	case 7:
		show ? cout << "��(7) " : cout << "";
		Beep(987.77, 500);
		break;
	case 8:
		show ? cout << "��(8) " : cout << "";
		Beep(1046.50, 500);
		break;
	default:
		break;
	}
}

void Init()
{
	cout << "------------------------------------------" << endl;
	cout << " | ���� ���� ���� | " << endl;
	cout << "------------------------------------------" << endl;
	cout << "���� : ó���� 8���踦 ����ְ�.\n ������ 8�� �� �� ���� ���� ��� �ش�.\n �׸��� �� ���� ��ȣ�� ������." << endl;
	cout << "-----------------------------------------------------------------" << endl;
	cout << "�غ��ϰ� �ƹ� Ű�� ������" << endl;
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
		cout << "!!!�غ�!!!" << endl;
		Sleep(1000);

		int random = rand() % 8 + 1;
		SoundPlay(random, false);
		system("cls");

		input = _getch() - '0'; // ���� '0'�� ���־� ���ڷ� ��ȯ
		cout << input << endl;

		if (input <= 0 || input > 8)
		{
			cout << "���� ������ �ʰ��Ͽ����ϴ�." << endl;
			continue;
		}

		if (random == input)
		{
			cout << "�����Դϴ�." << endl;
		}
		else
		{
			cout << "Ʋ�Ƚ��ϴ�." << endl;
		}

		while (true)
		{
			cout << "������ �׸��ѱ��? (Y/N)" << endl;
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
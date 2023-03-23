#include<iostream>
#include<conio.h>
#include<list>

using namespace std;
list<int> inputs;


void BingoCount(int* _pNumber, int& iBingo)
{
    bool isBingo = true;
    iBingo = 0;

    for (int i = 0; i < 5; i++)
    {
        isBingo = true;
        for (int j = 0; j < 5; j++)
        {
            if (_pNumber[i * 5 + j] != INT_MAX)
            {
                isBingo = false;
                break;
            }
        }

        if (isBingo)
        {
            iBingo++;
        }
        
    }


   
    for (int i = 0; i < 5; i++)
    {
        isBingo = true;
        for (int j = 0; j < 5; j++)
        {
            if (_pNumber[j * 5 + i] != INT_MAX)
            {
                isBingo = false;
                break;
            }
        }

        if (isBingo)
        {
            iBingo++;
        }
    }


    for (int i = 0; i < 5; i++)
    {
        isBingo = true;
        if (_pNumber[6 * i] != INT_MAX)
        {
            isBingo = false;
            break;
        }

        if(i >= 4)
        {
            if (isBingo)
            {
                iBingo++;
            }
        }
    }

    

    for (int i = 0; i < 5; i++)
    {
        isBingo = true;
        if (_pNumber[25 - 6 * i] != INT_MAX)
        {
            isBingo = false;
            break;
        }

        if (i >= 4)
        {
            if (isBingo)
            {
                iBingo++;
            }
        }
    }

   
}

void RenderNumber(int* _pNumber, int& iBingo, int& input)
{
    cout << "======================================" << endl;
    cout << " |\t ���� �����Դϴ�.\t" << endl;
    cout << "======================================" << endl;
    cout << "�������� 5�� �̻��̸� ���ӿ��� �¸��մϴ�." << endl;
    cout << "======================================" << endl;

    for (int i = 0; i < 5; i++)
    {
        for (int j = 0; j < 5; j++)
        {
            if (_pNumber[i * 5 + j] == input || _pNumber[i * 5 + j] == INT_MAX)
            {
                cout << "*" << "\t";
                _pNumber[i * 5 + j] = INT_MAX;
            }
            else
                cout << _pNumber[i * 5 + j] << "\t";
        }
        cout << endl;
    }
    BingoCount(_pNumber , iBingo);
   
    cout << "Bingo Line: " << iBingo << endl;
}


void Init(int* _pNumber)
{
    // ����.
    srand((unsigned int)time(NULL));
    // �� �� ���־�.
    for (int i = 0; i < 25; i++)
        _pNumber[i] = i + 1;
    // �����.
    int iTemp, idx1, idx2;
    for (int i = 0; i < 100; i++)
    {
        idx1 = rand() % 24;
        idx2 = rand() % 24;
        iTemp = _pNumber[idx1];
        _pNumber[idx1] = _pNumber[idx2];
        _pNumber[idx2] = iTemp;
    }
}

int main()
{
    int iNumber[25] = {};
    int iBingo = 0;
    int iInput;
    Init(iNumber);
    while (true)
    {
        system("cls");
        RenderNumber(iNumber, iBingo, iInput);
        
        if (iBingo >= 5)
        {
            cout << "�¸��ϼ̽��ϴ�." << endl;
            break;
        }
        cout << "���ڸ� �Է��ϼ���(0: ����): " << endl;
        cin >> iInput;
        ;
        if (iInput == 0)
        {
            cout << "������ �����մϴ�." << endl;
            break;
        }
        else if (iInput < 1 || iInput > 25)
        {
            cout << "�߸� �Է��߽��ϴ�." << endl;
            continue;
        }
    }
}
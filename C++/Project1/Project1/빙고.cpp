#include<iostream>
#include <algorithm>

using namespace std;



enum class AI_MODE
{
    AM_EASY = 1,
    AM_NORMAL,
    AM_HARD
};

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

void ChangeNumber(int* _pNumber, int& input)
{
    for (int i = 0; i < 5; i++)
    {
        for (int j = 0; j < 5; j++)
        {
            if (_pNumber[i * 5 + j] == input)
            {

                _pNumber[i * 5 + j] = INT_MAX;
            }
        }
    }
}

void RenderNumber(int* _pNumber, int& iBingo)
{
    cout << "======================================" << endl;
    cout << " |\t 빙고 게임입니다.\t|" << endl;
    cout << "======================================" << endl;
    cout << "빙고줄이 5줄 이상이면 게임에서 승리합니다." << endl;
    cout << "======================================" << endl;

    for (int i = 0; i < 5; i++)
    {
        for (int j = 0; j < 5; j++)
        {
            if (_pNumber[i * 5 + j] == INT_MAX)
            {
                cout << "*" << "\t";
                _pNumber[i * 5 + j] = INT_MAX;
            }
            else
                cout << _pNumber[i * 5 + j] << "\t";
        }
        cout << endl;
    }
    
   
    cout << "Bingo Line: " << iBingo << endl;
}


void Init(int* _pNumber)
{
    // 셔플.
    
    // 값 다 들어가있어.
    for (int i = 0; i < 25; i++)
        _pNumber[i] = i + 1;
    // 섞어요.
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

int SelectAinumber(int* _puNumber, AI_MODE _eMode)
{
    int iNoneSelect[25] = {};
    int arr[10] = {};
    int count = 0, temp = 10 , n = 0;
    

switch (_eMode)
{
    case AI_MODE::AM_EASY:
    {
        for(int i=0; i<25; i++)
        {
            if(_puNumber[i] != INT_MAX)
            {
                iNoneSelect[count] = _puNumber[i];
                count++;
            }
        }
        return iNoneSelect[rand() % count];
    }
        break;
    case AI_MODE::AM_NORMAL:
    {
        for (int i = 0; i < 5; i++)//가로
        {
            count = 0;
            fill_n(arr, 5, 0);
            for (int j = 0; j < 5; j++)
            {
                if(_puNumber[i * 5 + j] != INT_MAX)
                {
                    count++;
                    arr[count]= _puNumber[i * 5 + j];
                }
            }

            if(count <= temp)
            {
                temp = count;
                fill_n(iNoneSelect, 6, 0);
              
                for(int k=0; k< temp; k++)
                {
                    iNoneSelect[k] = arr[k];
                }
            }
           
        }


        for (int i = 0; i < 5; i++) //세로
        {
            count = 0;
            fill_n(arr, 5, 0);
            for (int j = 0; j < 5; j++)
            {
                if (_puNumber[j * 5 + i] != INT_MAX)
                {
                    count++;
                    arr[count] = _puNumber[j * 5 + i];
                }
            }

            if (count <= temp)
            {
                temp = count;
                fill_n(iNoneSelect, 6, 0);
                for (int k = 0; k < temp; k++)
                {
                    iNoneSelect[k] = arr[k];
                }
            }
        }

        count = 0;
        fill_n(arr, 5, 0);
        for (int i = 0; i < 5; i++) //왼쪽위에서 오른쪽 아래로 대각선
        {
            if (_puNumber[6 * i] != INT_MAX)
            {
                count++;
                arr[count] = _puNumber[6 * i];
            }
        }

        if (count <= temp)
        {
            temp = count;
            fill_n(iNoneSelect, 6, 0);
            for (int k = 0; k < temp; k++)
            {
                iNoneSelect[k] = arr[k];
            }
        }

        count = 0;
        fill_n(arr, 5, 0);
        for (int i = 0; i < 5; i++)
        { 
            if (_puNumber[25 - 6 * i] != INT_MAX)
            {
                count++;
                arr[count] = _puNumber[25 - 6 * i];
            }
        }

        if (count <= temp)
        {
            temp = count;
            fill_n(iNoneSelect, 6, 0);
            for (int k = 0; k < temp; k++)
            {
                iNoneSelect[k] = arr[k];
            }
        }

        
        return iNoneSelect[rand() % temp];
    }
        break;
    case AI_MODE::AM_HARD:
    {

    }
        break;
    default:
        break;
    }
}

AI_MODE SelectAImode()
{
    cout << "======================================" << endl;
    cout << " |\t 빙고 게임입니다.\t|" << endl;
    cout << "======================================" << endl;
    cout << "빙고줄이 5줄 이상이면 게임에서 승리합니다." << endl;
    cout << "======================================" << endl;
    cout << "1.EASY" << endl;
    cout << "2.NORMAL" << endl;
    cout << "AI모드를 선택하세요." << endl;
    int Aimode;
    cin >> Aimode;

    if(Aimode < (int)AI_MODE :: AM_EASY || Aimode > (int)AI_MODE::AM_HARD)
    {
        cout << "잘못 입력하셨습니다." << endl;
    }

    return (AI_MODE)Aimode;
}

int main()
{
    srand((unsigned int)time(NULL));
    int Player[25] = {};
    int AI[25] = {};
    int iBingo = 0, AiBingo = 0;
    int iInput = 0;
    Init(Player);
    Init(AI);
    AI_MODE aimode = SelectAImode();

    

    while (true)
    {
        system("cls");
        cout << "==================Player====================" << endl;
        RenderNumber(Player, iBingo);
        cout << "===================AI===================" << endl;
        switch (aimode)
        {
        case AI_MODE::AM_EASY:
            cout << "AIMode: EASY" << endl;
            break;
        case AI_MODE::AM_NORMAL:
            cout << "AIMode: NORMAL" << endl;
            break;
        case AI_MODE::AM_HARD:
            cout << "AIMode: HARD" << endl;
            break;
        default:
            break;
        }
        RenderNumber(AI, AiBingo);

        if(iInput != 0)
        {
            cout << "AI가 선택한 숫자는" << iInput << "입니다." << endl;

        }
        
        cout << "숫자를 입력하세요(0: 종료): " << endl;
        cin >> iInput;
        
        if (iInput == 0)
        {
            cout << "게임을 종료합니다." << endl;
            break;
        }
        else if (iInput < 1 || iInput > 25)
        {
            cout << "잘못 입력했습니다." << endl;
            continue;
        }
        ChangeNumber(Player, iInput);
        ChangeNumber(AI, iInput);

        iInput = SelectAinumber(AI, aimode);



        ChangeNumber(Player, iInput);
        ChangeNumber(AI, iInput);

        BingoCount(Player, iBingo);
        BingoCount(AI, AiBingo);

        if (iBingo >= 5)
        {
            cout << "Player 님이 게임세서 승리하셨습니다." << endl;
            break;
        }
        else if (AiBingo >= 5)
        {
            cout << "AI 님이 게임세서 승리하셨습니다." << endl;
            break;
        }
    }
}
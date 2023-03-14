#include <iostream>
using namespace std;

int main()
{
	int lotto[7]; //이번 주 로또 번호
	int user[6]; //유저 로또 번호    
    int a = 0; //맞은 개수
    int b = 0; //보너스 번호 일치
    int output; //출력

    for (int i = 0; i < 7; i++) //이번 주 로또 번호 입력
    {
        cin >> lotto[i];
    }
        

    for (int i = 0; i < 6; i++) //유저 로또 번호 입력
    {
        cin >> user[i];
    }
    
    for(int i=0; i<6; i++)
    {
        for(int j=0; j<6; j++)
        {
            if (lotto[i] == user[j]) //이번 주 로또번호와 유저 로또 번호가 같으면 a++
            {
                a++;
            }
        }
        
        if(lotto[6] == user[i]) //보너스 번호와 유저 로또 번호가 같으면 b++
        {
            b++;
        }
    }

    switch (a)
    {
    case 6:
        output = 1;
        break;
    case 5:
        output =b >= 1 ? 2 : 3; //b가 1보다 크거나 같으면 2 아니면 3
        break;
    case 4:
        output = 4;
        break;
    case 3:
        output = 5;
        break;
    default:
        output = 0;
        break;
    }
  
    cout << output; //출력
}
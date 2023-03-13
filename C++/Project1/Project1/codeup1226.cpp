#include <iostream>
using namespace std;

int main()
{
	int lotto[7];
	int user[6];
    int a = 0;
    int b = 0;
    int output;

    for (int i = 0; i < 7; i++)
    {
        cin >> lotto[i];
    }
        

    for (int i = 0; i < 6; i++)
    {
        cin >> user[i];
    }
    
    for(int i=0; i<6; i++)
    {
        for(int j=0; j<6; j++)
        {
            if (lotto[i] == user[j])
            {
                a++;
            }
        }
        

        if(lotto[6] == user[i])
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
        output =b >= 1 ? 2 : 3;
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
  
    cout << output;
}
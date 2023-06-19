#include <iostream>
#include <string>
#include <unordered_set>

using namespace std;

string solution(string my_string) 
{
    unordered_set<char> set;
    string answer = "";

    for(int i = 0; i < my_string.length(); i++)
        set.insert(my_string[i]);
    

    for (int i = 0; i < my_string.length(); i++)
    {
        if(set.find(my_string[i]) != set.end())
        {
            answer += my_string[i];
            set.erase(set.find(my_string[i]));
        }
    }
      

    return answer;
}

int main()
{
    string input;
    
    cin >> input;

    cout << solution(input);
}
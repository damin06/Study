#include <iostream>
#include <unordered_set>
using namespace std;

//���¼��ڴ��ϱ�
int solution(unordered_set<int> set)
{
    int answer = 0;
    for (int i = 0; i < 10; i++)
    {
        if (set.find(i) == set.end()) answer += i;
    }
        
    return answer;
}
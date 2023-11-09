#include <string>
#include <unordered_map>
#include <vector>
using namespace std;

int solution(vector<vector<string>> clothes)
{
    int answer = 1;
    unordered_map<string, int> gears;

    for (int i = 0; i < clothes.size(); i++)
        gears[clothes[i][1]]++;

    for (const auto& item : gears)
        answer *= (item.second + 1);

    return --answer;
}
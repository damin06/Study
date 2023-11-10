#include <iostream>
#include <string>
#include <unordered_map>
#include <vector>
using namespace std;

int solution(vector<pair<string, string>> clothes)
{
    int answer = 1;
    unordered_map<string, int> gears;

    for (int i = 0; i < clothes.size(); i++)
        gears[clothes[i].second]++;

    for (const auto item : gears)
        answer *= (item.second + 1);

    return --answer;
}

int main() 
{
    int t, n;
    string input1, input2;
    vector<pair<string, string>> clothes;

    cin >> t;
    for(int i = 0; i < t; i++)
    {
        clothes.clear();
        cin >> n;
        for(int j = 0; j < n; j++)
        {
            cin >> input1 >> input2;
            clothes.push_back(make_pair(input1, input2));
        }
        cout << solution(clothes) << "\n";
    }
}
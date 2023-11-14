#include<iostream>
#include<vector>
#include<algorithm>
using namespace std;

int main()
{
    int t, input;
    vector<int> vec;

    cin >> t;

    for (int i = 0; i < t; i++)
    {
        vec = vector<int>();
        for(int j = 0; j < 10; j++)
        {
            cin >> input;
            vec.push_back(input);
        }
        sort(vec.begin(), vec.end(), greater<>());
        cout << vec[2] << "\n";
    }
}

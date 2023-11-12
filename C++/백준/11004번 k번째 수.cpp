#include<algorithm>
#include<iostream>
#include<vector>
using namespace std;

int main()
{
    ios_base::sync_with_stdio(false);
    cin.tie(NULL);

    int n, m, input;
    vector<int> vec;
    cin >> n >> m;

    for (int i = 0; i < n; i++)
    {
        cin >> input;
        vec.push_back(input);
    }

    sort(vec.begin(), vec.end());
    cout << vec[m - 1];
}
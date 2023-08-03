#include <iostream>
#include <algorithm>
using namespace std;
int n, m, a, b;
int main() {
    cin >> n >> m;
    int* bas = new int[n + 1];
    for (int i = 1; i <= n; i++)
        bas[i] = i;
    while (m--)
    {
        cin >> a >> b;
        swap(bas[a], bas[b]);
    }
    for (int i = 1; i <= n; i++)
        cout << bas[i] << ' ';
}
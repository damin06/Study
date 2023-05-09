#include <iostream>
using namespace std;

int main() {
    int n, m, i, j, sum;
    float avg;

    cin >> n;
    int* score = new int[n];
    for (i = 0; i < n; i++) 
    {
        cin >> m;
        sum = 0;
        for (j = 0; j < m; j++) 
        {
            cin >> score[j];
            sum += score[j];
        }
        avg = (float)sum / (float)m;
        sum = 0;
        for (j = 0; j < m; j++) 
        {
            if (avg < score[j]) 
                sum++;
        }
        avg = (float)sum / (float)m * 100;
        cout << fixed;
        cout.precision(3);
        cout << avg << '%' << endl;
    }
}
#include <iostream>
#include <cstdlib>  

using namespace std;

int main(int argc, char const* argv[]) {
    int A;
    char B[4];

    cin >> A;
    cin >> B;

    cout << A * (B[2] - '0') << "\n";
    cout << A * (B[1] - '0') << "\n";
    cout << A * (B[0] - '0') << "\n";
    cout << A * atoi(B) << "\n";
    return 0;
}
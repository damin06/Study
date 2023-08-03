#include <iostream>

using namespace std;

int main(int argc, char const* argv[]) {
	int A, B;

	while (true) {
		cin >> A >> B;

		if (A == 0 && B == 0) {	// A와 B가 모두 0이라면 while문 종료
			break;
		}
		cout << A + B << "\n";
	}

	return 0;
}
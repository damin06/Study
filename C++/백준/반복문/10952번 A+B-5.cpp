#include <iostream>

using namespace std;

int main(int argc, char const* argv[]) {
	int A, B;

	while (true) {
		cin >> A >> B;

		if (A == 0 && B == 0) {	// A�� B�� ��� 0�̶�� while�� ����
			break;
		}
		cout << A + B << "\n";
	}

	return 0;
}
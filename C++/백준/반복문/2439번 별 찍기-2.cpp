#include <iostream>

using namespace std;

int main(int argc, char const* argv[]) {
	int N;

	cin >> N;

	for (int row = 1; row <= N; row++) {

		// ������ N - row�� ��ŭ ����Ѵ�.
		for (int i = 0; i < N - row; i++) {
			cout << ' ';
		}

		// ���� row����ŭ ����Ѵ�.
		for (int i = 0; i < row; i++) {
			cout << '*';
		}

		// �� ���� ����� ������ ����(�ٹٲ�)
		cout << '\n';
	}
	return 0;
}
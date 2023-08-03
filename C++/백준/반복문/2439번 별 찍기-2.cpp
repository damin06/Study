#include <iostream>

using namespace std;

int main(int argc, char const* argv[]) {
	int N;

	cin >> N;

	for (int row = 1; row <= N; row++) {

		// 공백은 N - row개 만큼 출력한다.
		for (int i = 0; i < N - row; i++) {
			cout << ' ';
		}

		// 별은 row개만큼 출력한다.
		for (int i = 0; i < row; i++) {
			cout << '*';
		}

		// 한 행의 출력이 끝나면 개행(줄바꿈)
		cout << '\n';
	}
	return 0;
}
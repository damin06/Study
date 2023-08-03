#include <iostream>
using namespace std;

int main() {

	int arr[9][9];
	int max = 0, row = 0, col = 0;

	for (int i = 0; i < 9; i++) {
		for (int k = 0; k < 9; k++) {
			cin >> arr[i][k];
			if (arr[i][k] > max) {
				max = arr[i][k];
				row = i;
				col = k;
			}
		}
	}
	cout << max << "\n";
	cout << row + 1 << " " << col + 1 << endl;

	return 0;
}
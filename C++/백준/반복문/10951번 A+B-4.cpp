#include <iostream>
using namespace std;

int main(int argc, const char* argv[]) {

	int a, b;
	while (!(cin >> a >> b).eof()) {	// 혹은 eof() 대신 fail()을 사용해도 된다.
		cout << a + b << "\n";
	}

	return 0;
}
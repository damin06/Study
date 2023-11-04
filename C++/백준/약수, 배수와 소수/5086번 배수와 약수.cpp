#include <iostream>
using namespace std;

int main()
{
	int input1, inpu2;
	while (true)
	{
		cin >> input1 >> inpu2;

		if (input1 + inpu2 == 0)
			break;
		else if (inpu2 % input1 == 0)
			cout << "factor\n";
		else if (input1 % inpu2 == 0)
			cout << "multiple\n";
		else
			cout << "neither\n";
	}
}
#include <iostream>
#include <algorithm>
#include <string>
using namespace std;

int main() 
{
	string input, reserve;

	cin >> input;
	reserve = input;
	reverse(reserve.begin(), reserve.end());

	if (input == reserve)
		cout << "true";
	else
		cout << "false";
}
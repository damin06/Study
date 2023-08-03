#include <iostream>
#include <vector>
#include <string>
using namespace std;

int main()
{
	string word[5];
	for (int i = 0; i < 5; i++)

		cin >> word[i];



	for (int i = 0; i < 15; i++) //최대 15 글자

		for (int j = 0; j < 5; j++)

			if (i < word[j].size()) //word[j]의 인덱스 범위 내에서만 출력

				cout << word[j][i];

	cout << endl;
}
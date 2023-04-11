//#include <iostream>
//#include<string>
//using namespace std;
//
//int main()
//{//50
//	string word;
//	cin >> word;
//	//int arr[25];
//	int arr[50]{0};
//
//	for (int i = 0; i < word.length(); i++)
//	{
//		//if (word[i] < 91) arr[i] = word[i - 65]++;
//		//else word[i - 97]++;
//		if (word[i] > 90)
//			arr[word[i] - 65 - 6]++;
//		else
//			arr[word[i] - 65]++;
//	}
//
//	int targetIndex = 0;
//	for (int i = 0; i < 50; ++i)
//	{
//		if (arr[targetIndex] == arr[i] && arr[targetIndex] != 0)
//		{
//			cout << '?';
//			return 0;
//		}
//		else if (arr[targetIndex] < arr[i])
//			targetIndex = i;
//	}
//
//	cout << (char)(targetIndex > 24 ? targetIndex - 25 + 'a' : targetIndex + 'A');
//}
#include <iostream>
#include <string>
using namespace std;

int main()
{
	string word;
	cin >> word;
	int arr[26]{ 0 };
	int cnt = 0, answer = 0;

	for (int i = 0; i < word.length(); i++)
	{
		if (word[i] < 91) arr[(int)word[i]-65]++;
		else arr[(int)word[i] - 97]++;
	}

	for(int i=0; i<26; i++)
	{
		if (arr[i] > cnt) 
		{
			cnt = arr[i];
			answer = i;
		}
	}
	
	for (int i = 0; i < 26; i++)
	{
		if(arr[i] == cnt && i != answer)
		{
			cout << "?";
			return 0;
		}
	}

	cout << (char)(answer+65);
}
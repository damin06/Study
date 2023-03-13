#include <iostream>
#include <regex>
#include <string>

using namespace std;

int main() {

#pragma region 문자 위치찾기

	string str = "When in Rome, do as the Romans.";
	int a = str.find("Rome");

	cout << a << endl;

#pragma endregion

#pragma region 특정 문자 삭제하기
	
	string b;
	cout << "주민등록번호를 입력하시오: ";
	cin >> b;
	
	b = regex_replace(b, regex("-"),"");
	cout << "-가 제거된 주민등록번호: " << b << endl;

#pragma endregion

#pragma region 해밍거리

	string dna1;
	string dna2;
	int hamming = 0;

	cout << "DNA1: ";
	cin >> dna1;
	cout << "DNA2: ";
	cin >> dna2;

	for(int i=0; i<dna1.length(); i++)
	{
		if(dna1[i] != dna2[i])
		{
			hamming++;
		}
	}

	cout << "해밍 거리는 " << hamming << endl;

#pragma endregion

#pragma region 행맨

	string e = "C++";
	string c ="";
	string f = "___";
	int d = 0;
	while (true)
	{
		cout << "글자를 입력하시오: ";
		cin >> c;	
		while (true)
		{
			
				if (e[d] == c[0])
				{
					f[d] = e[d];
					//e.erase(i, 1);
					cout << f << endl;
					d++;
					break;
				}
				else
				{
					break;
				}
			
		}
		
		if (f == "C++")
		{
			cout << "성공!";
			break;
		}
	}

#pragma endregion

}
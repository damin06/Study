#include <iostream>
#include <regex>
#include <string>

using namespace std;

int main() {

#pragma region ���� ��ġã��

	string str = "When in Rome, do as the Romans.";
	int a = str.find("Rome");

	cout << a << endl;

#pragma endregion

#pragma region Ư�� ���� �����ϱ�
	
	string b;
	cout << "�ֹε�Ϲ�ȣ�� �Է��Ͻÿ�: ";
	cin >> b;
	
	b = regex_replace(b, regex("-"),"");
	cout << "-�� ���ŵ� �ֹε�Ϲ�ȣ: " << b << endl;

#pragma endregion

#pragma region �عְŸ�

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

	cout << "�ع� �Ÿ��� " << hamming << endl;

#pragma endregion

#pragma region ���

	string e = "C++";
	string c ="";
	string f = "___";
	int d = 0;
	while (true)
	{
		cout << "���ڸ� �Է��Ͻÿ�: ";
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
			cout << "����!";
			break;
		}
	}

#pragma endregion

}
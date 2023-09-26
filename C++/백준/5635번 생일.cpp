#include <iostream>
#include <vector>
using namespace std;

struct Student
{
public:
	string name;
	int day;
	int month;
	int year;

	bool operator > (const Student& s)
	{
		if (year != s.year)
			return year < s.year;

		if (month != s.month)
			return month < s.month;

		if (day != s.day)
			return day < s.day;
	}
};

int main() 
{
	int n;
	Student students[100];
	cin >> n;

	for (int i = 0; i < n; i++) 
	{
		string name;
		int d, m, y;
		cin >> name >> d >> m >> y;
		Student s{ name, d, m, y };
		students[i] = s;
	}

	for (int i = 0; i < n; i++) 
	{
		for (int j = 0; j < (n -1) - i; j++) 
		{
			if(students[j] > students[j + 1])
			{
				Student a = students[j + 1];
				students[j + 1] = students[j];
				students[j] = a;
			}
		}
	}

	//for(int i = 0; i < n; i++)
	//{
	//	cout << students[i].name << endl;
	//}

	cout << students[0].name << "\n" << students[n -1].name;
}
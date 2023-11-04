#include <iostream>
#include <unordered_map>
using namespace std; 

unordered_map<string, float> gradescore =
{ {"A+", 4.5}, {"A0", 4.0}, {"B+", 3.5},
  {"B0", 3.0}, {"C+", 2.5}, {"C0", 2.0},
  {"D+", 1.5}, {"D0", 1.0}, {"F", 0} };

int main()
{
	string subName, rating;
	float grade, totalScore = 0, averageScore = 0;

	for(int i = 0; i < 20; i++)
	{
		cin >> subName >> grade >> rating;

		if (rating == "P")
			continue;

		totalScore += grade;
		averageScore += grade * gradescore[rating];
	}

	cout << averageScore / totalScore;
}
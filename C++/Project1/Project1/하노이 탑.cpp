#include <iostream>
using namespace std;

void HanoiTower(int num, int from, int by, int to)
{
	if(num == 1)
	{
		cout << from<< " " << to << endl;
	}
	else
	{
		HanoiTower(num - 1, from, to, by);
		cout  << from << " " << to << endl;
		HanoiTower(num - 1, by, from, to);
	}
}

int CountHanoi(int num)
{
	if(num == 1)
	{
		return 1;
	}
	return 2 * CountHanoi(num - 1) + 1;
}

int main()
{	
	char A = '1', B = '2', C ='3';
	int count;
	cin >> count;


	cout << CountHanoi(count) << endl;
	HanoiTower(count, 1, 2, 3);
}
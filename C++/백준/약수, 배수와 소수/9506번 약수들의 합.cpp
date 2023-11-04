#include <iostream>
#include <vector>
using namespace std;

int main()
{
	int input, total;
	vector<int> measure;
	while (true)
	{
		measure.clear();
		total = 0;
		cin >> input;

		if (input == -1)
			break;

		for(int i = 1; i < input; i++)
		{
			if (input % i == 0)
			{
				measure.push_back(i);
				total += i;
			}
		}

		if (total != input)
		{
			cout << input << " is NOT perfect.\n";
			continue;
		}

		cout << input << " = " << measure[0];
		for(int i = 1; i < measure.size(); i++)
		{
			cout << " + " << measure[i];
		}
		cout << "\n";
	}
}
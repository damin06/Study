#include <iostream>
using namespace std;

int tree[10000];

void postOrder(int start, int end)
{
	if (start >= end) return;

	if(start == end - 1)
	{
		cout << tree[start] << "\n";
		return;
	}
	int index = start + 1;

	while (index < end) 
	{
		if (tree[start] < tree[index])
			break;
		index++;
	}

	postOrder(start + 1, index);
	postOrder(index, end);

	cout << tree[start] << "\n";
}


int main()
{
	ios::sync_with_stdio(false);
	cin.tie(0);
	cout.tie(0);

	int num;
	int i = 0;

	while (cin >> num)
		tree[i++] = num;

	postOrder(0, i);
}
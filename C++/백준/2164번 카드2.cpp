#include <iostream>
#include<queue>
using namespace std;

int main()
{
	queue<int> myqueue;
	int n, temp = 0;
		
	cin >> n;

	for(int i = 1; i <= n; i++)
	{
		myqueue.push(i);
	}

	while (myqueue.size() > 1)
	{
		myqueue.pop();
		myqueue.push(myqueue.front());
		myqueue.pop();
	}

	cout << myqueue.front();
}
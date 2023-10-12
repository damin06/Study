#include <iostream>
#include <vector>
#include <map>
#include <queue>
using namespace std;

map<int, vector<int>> adj;

void tree()
{
	vector<int>visited;
	visited.resize(adj.size() + 1);
	visited[0] = 1;
	queue<int> myqueue;
	myqueue.push(1);

	while (!myqueue.empty())
	{
		int front = myqueue.front();
		myqueue.pop();

		for(int i = 0; i < adj[front].size(); i++)
		{
			int child = adj[front][i];
			if(visited[child] == 0)
			{
				visited[child] = front;
				myqueue.push(child);
			}
		}
	}

	for (int i = 2; i < visited.size(); i++)
		cout << visited[i] << "\n";
}

int main()
{
	int n, input1, input2;
	cin >> n;

	for(int i = 1; i < n; i++)
	{
		cin >> input1 >> input2;
		adj[input1].push_back(input2);
		adj[input2].push_back(input1);
	}

	tree();
}
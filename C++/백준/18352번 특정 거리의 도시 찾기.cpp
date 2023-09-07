#include<iostream>
#include <vector>
#include <queue>
#include <algorithm>
using namespace std;

int main()
{
	int N, M, K, X;
	vector<vector<int>> adj;
	vector<int> visited;
	vector<int> answer;
	cin >> N >> M >> K >> X;
	adj.resize(N + 1);
	visited.resize(N + 1, -1);
	queue<int> q;
	q.push(X);
	visited[X] = 0;
	int n1, n2;
	for(int i = 0; i < M; i++)
	{
		cin >> n1 >> n2;
		adj[n1].push_back(n2);
	}

	while (!q.empty())
	{
		int now = q.front();
		q.pop();
		for(int i = 0; i < adj[now].size(); i++)
		{
			int next = adj[now][i];
			if (visited[next] != -1) continue;
			q.push(next);
			visited[next] = visited[now] + 1;
			if (visited[next] == K) answer.push_back(next);
		}
	}

	sort(answer.begin(), answer.end());
	if (answer.empty()) cout << "-1";
	else
	{
		for (auto n : answer)
			cout << n << "\n";
	}
}
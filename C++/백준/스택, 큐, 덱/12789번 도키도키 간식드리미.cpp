#include <iostream>
#include <stack>
using namespace std;

int main() 
{
	ios_base::sync_with_stdio(0);cin.tie(0);

	stack<int> S;
	int n, input, cnt = 1;
	cin >> n;

	while (n--) {
		cin >> input;

		if (input == cnt) 
			cnt++; 
		else 
			S.push(input); 

		while (!S.empty() && S.top() == cnt) 
		{
			S.pop();  
			cnt++;
		}
	}

	if (S.empty())
		cout << "Nice"; 
	else
		cout << "Sad";
}
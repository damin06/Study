#include <vector>
#include <string>
#include <unordered_map>
#include <set>
using namespace std;

vector<int> solution(vector<string> id_list, vector<string> report, int k)
{
	unordered_map<string, set<string>> report_listl;
	unordered_map<string, int> mail_count;
	vector<int> answer;


	for (int i = 0; i < report.size(); i++)
	{
		string reporter = report[i].substr(0, report[i].find(" "));
		string reported = report[i].substr(report[i].find(" ") + 1);
		report_listl[reported].insert(reporter);
	}

	for (auto n : report_listl)
	{
		if (n.second.size() < k)
			continue;

		for (auto k : n.second)
			mail_count[k]++;
	}

	for (int i = 0; i < id_list.size(); i++)
		answer.push_back(mail_count[id_list[i]]);

	return answer;
}
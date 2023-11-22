//#include <iostream>
//#include <string>
//#include <vector>
//
//using namespace std;
//
//vector<int> solution(vector<string> words, vector<string> queries)
//{
//    vector<int> answer;
//    for(int i = 0; i < queries.size(); i++)
//    {
//        answer.push_back(0);
//        for(int j = 0; j < words.size(); j++)
//        {
//            if (words[j].size() != queries[i].size())
//                continue;
//
//            for(int k = 0; k < queries[i].size(); k++)
//            {
//                if (queries[i][k] == '?')
//                {
//                    if (k == queries[i].size() - 1)
//                        answer[i]++;
//                    continue;
//                }
//                else if (queries[i][k] != words[j][k])  
//                    break;
//                else if (k == queries[i].size() - 1)
//                    answer[i]++;
//            }
//        }
//    }
//    return answer;
//}



#include <algorithm>
#include <iostream>
#include <string>
#include <vector>
#include <regex>
using namespace std;


bool comp(string a, string b) {
    if (a.length() < b.length())
        return true;

    else if (a.length() == b.length()) {
        if (a < b)
            return true;
    }

    return false;
}

vector<int> solution(vector<string> words, vector<string> queries)
{
    vector<int> answer;

    vector<string> reserveWords = words;
    for (int i = 0; i < reserveWords.size(); i++)
        reverse(reserveWords[i].begin(), reserveWords[i].end());


    sort(words.begin(), words.end(), comp);
    sort(reserveWords.begin(), reserveWords.end(), comp);

    for(int i = 0; i < queries.size(); i++)
    {
        int hi, lo, idx;
        string querie = queries[i];

        if(querie[0] == '?')
        {
            reverse(querie.begin(), querie.end());

            idx = querie.find_first_of('?');

            for (int j = idx; j < querie.length(); j++)
                querie[j] = 'a';
            
            lo = lower_bound(reserveWords.begin(), reserveWords.end(), querie, comp) - reserveWords.begin();
            
            for (int j = idx; j < querie.length(); j++) 
                querie[j] = 'z';

            hi = upper_bound(reserveWords.begin(), reserveWords.end(), querie, comp) - reserveWords.begin();
        }
        else
        {
            idx = querie.find_first_of('?');

            for (int j = idx; j < querie.length(); j++)
                querie[j] = 'a';

            lo = lower_bound(words.begin(), words.end(), querie, comp) - words.begin();

            for (int j = idx; j < querie.length(); j++)
                querie[j] = 'z';

            hi = upper_bound(words.begin(), words.end(), querie, comp) - words.begin();
        }
        answer.push_back(hi - lo);
    }
    
    return answer;
}
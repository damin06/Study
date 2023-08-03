#include <iostream>
#include <string>
using namespace std;

int count(string str);

int main() {
    ios_base::sync_with_stdio(false);	
    cin.tie(NULL);	

    string str;
    cin >> str;
    cout << count(str);
}

int count(string str) {
    string alphabet[8] = { "c=", "c-", "dz=", "d-", "lj", "nj", "s=", "z=" };

    for (int i = 0; i < 8; i++) {
        while (true) 
        {
            if (str.find(alphabet[i]) == string::npos) 
                break;

                str.replace(str.find(alphabet[i]), alphabet[i].length(), "#");
        }
    }
    return str.length();
}

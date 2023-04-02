#include <iostream>
#include <string>
using namespace std;

class Book
{
public:
    Book(string a, int b) : bookname(a), price(b) {};
    string bookname;
    int price;
    int books[100];
};

int main(void)
{
    int num;
    cout << "총 몇권의 책을 저장하고 싶으신가요? :";
    cin >> num;
    Book* ptr = new Book[num];
    
    for(int i=0; i<num; i++)
    {
        cin >> ptr[i].bookname >> ptr[i].price;
    }

    cout << "소장하고 있는 책 정보" << endl;
    cout << "======================" << endl;
    for (int i = 0; i <  2; i++)
    {
        cout << ptr[i].bookname << ptr[i].price << endl;
    }

    cout << "======================" << endl;
    delete[] ptr;
    return 0;
}


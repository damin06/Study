//#include <iostream>
//using namespace std;
//
//class Complex
//{
//public:
//	double real, imag;
//
//	Complex() : real{ 0 }, imag{ 0 } {};
//	Complex(double a, double b) : real{ a }, imag{ b } {};
//
//	void print() 
//	{
//		cout << real << '+' << imag << 'i' << endl;
//	}
//
//};
//
//Complex add(const Complex &a, const Complex &b)
//{
//	return Complex(a.real + b.real, a.imag + b.imag);
//}
//
//int main()
//{
//	Complex c1{ 1,2 }, c2{ 3,4 };
//	Complex t;
//	t = add(c1, c2);
//	t.print();
//}

#include <iostream>
using namespace std;


class MyArray {
public:
	int size;
	int* data;
	MyArray(int size)
	{
		this->size = size;
		data = new int[size];
	}
	~MyArray()
	{
		if (data != NULL) delete[] this->data;
	}
};


int main()
{
	MyArray buffer(10);
	buffer.data[0] = 1;
	{
		MyArray clone = buffer;
	}
	buffer.data[0] = 2;
}
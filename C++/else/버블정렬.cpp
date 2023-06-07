#include <iostream>
using namespace std;

void bubbleSort(const int size, int arr[]); // 버블정렬
void printArr(const int size, const int arr[]); //배열 원소들을 출력
void swap(int& arr1, int& arr2); // 배열 원소를 서로 바꾸어 준다.

int main()
{
	const int size = 8; //배열 크기
	int arr[size]{1, 4, 2, 5, 3, 8, 6, 7}; // 배열
	bubbleSort(size ,arr); // 버블정렬
	printArr(size, arr);   // 출력
}

void bubbleSort(const int size, int arr[])
{
	for (int i = 0; i < size; i++) // 배열에서 제외할 원소의 갯수
	{
		for (int j = 1; j < 8 - i; j++) // 마지막에서 i만큼 원소를 제외하고 1부터 계산(현재 원소는 j, 이전 원소는 j-1)
		{
			if (arr[j - 1] > arr[j]) // 이전 원소가 지금 현재 가르키는 원소보다 크면 서로 바꾼다.
			{
				swap(arr[j], arr[j - 1]);
			}
		}
	}
}

void printArr(const int size, const int arr[]) 
{
	for (int i = 0; i < 8; i++)
		cout << arr[i] << endl;
}

void swap(int& arr1, int& arr2) 
{
	int temp = arr1;
	arr1 = arr2;
	arr2 = temp;
}
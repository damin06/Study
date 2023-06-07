#include <iostream>
using namespace std;

void bubbleSort(const int size, int arr[]); // ��������
void printArr(const int size, const int arr[]); //�迭 ���ҵ��� ���
void swap(int& arr1, int& arr2); // �迭 ���Ҹ� ���� �ٲپ� �ش�.

int main()
{
	const int size = 8; //�迭 ũ��
	int arr[size]{1, 4, 2, 5, 3, 8, 6, 7}; // �迭
	bubbleSort(size ,arr); // ��������
	printArr(size, arr);   // ���
}

void bubbleSort(const int size, int arr[])
{
	for (int i = 0; i < size; i++) // �迭���� ������ ������ ����
	{
		for (int j = 1; j < 8 - i; j++) // ���������� i��ŭ ���Ҹ� �����ϰ� 1���� ���(���� ���Ҵ� j, ���� ���Ҵ� j-1)
		{
			if (arr[j - 1] > arr[j]) // ���� ���Ұ� ���� ���� ����Ű�� ���Һ��� ũ�� ���� �ٲ۴�.
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
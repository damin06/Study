#include<iostream>
using namespace std;

int main() {

    int arr[100][100] = {}; //��ȭ��
    int count; //�������� ��
    int x, y; //x�� y��
    int answer = 0; //����

    cin >> count;
    for (int i = 0; i < count; i++) //������ ����ŭ �ݺ�
    {
        cin >> x >> y; //�������� ��ġ�� �Է¹���
        for (int j = x; j < x + 10; j++) //�������� X����
        {
            for (int k = y; k < y + 10; k++) //�������� Y����
            {
                if (arr[j][k] == 1) //��ȭ���� �ش���ġ�� �̹� �����̰� �ִٸ�
                    continue; //���� for�� �ȿ��ִ� �ڵ� ����

                arr[j][k] = 1; //��ȭ���� �����̸� ���� �κ��� 1
                answer++; //1�� ��
            }
        }
    }
    cout << answer;
}

#include<iostream>
using namespace std;

int main() {

    int arr[100][100] = {}; //도화지
    int count; //색종이의 수
    int x, y; //x축 y축
    int answer = 0; //넓이

    cin >> count;
    for (int i = 0; i < count; i++) //색종이 수만큼 반복
    {
        cin >> x >> y; //색종이의 위치를 입력받음
        for (int j = x; j < x + 10; j++) //색종이의 X범위
        {
            for (int k = y; k < y + 10; k++) //색종이의 Y범위
            {
                if (arr[j][k] == 1) //도화지에 해당위치에 이미 색종이가 있다면
                    continue; //현재 for문 안에있는 코드 생략

                arr[j][k] = 1; //도화지에 색종이를 붙인 부분을 1
                answer++; //1의 수
            }
        }
    }
    cout << answer;
}

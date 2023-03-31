// |이 코드는 Unity 게임 엔진에서 작동하는 C# 스크립트입니다. 이 스크립트는 폭발 효과를 구현하는 데 사용됩니다.
// |
// |좋은 점:
// |- 코드가 간결하고 읽기 쉽습니다.
// |- Awake 함수에서 GetComponentsInChildren 함수를 사용하여 자식 오브젝트들의 Collider와 Rigidbody를 가져옵니다. 이를 통해 코드의 성능을 향상시킬 수 있습니다.
// |- Crake 함수와 Crakeother 함수에서 AddExplosionForce 함수를 사용하여 폭발 효과를 추가합니다. 이 함수는 물리 연산을 사용하여 효과를 구현하므로, 게임에서 더욱 현실적인 효과를 제공합니다.
// |
// |나쁜 점:
// |- Update 함수에서 입력 값을 검사할 때, && 연산자를 사용하지 않고 || 연산자를 사용합니다. 이는 입력 값이 하나만 있어도 조건이 충족되어 폭발 효과가 발생할 수 있습니다. 따라서, 입력 값 검사에 대한 조건을 수정해야 합니다.
// |
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    private Collider[] col; // Collider 배열 변수 선언
    private Rigidbody[] rb; // Rigidbody 배열 변수 선언

    void Awake()
    {
        col = GetComponentsInChildren<Collider>(); // 자식 오브젝트들의 Collider를 가져와서 col 배열에 저장
        rb = GetComponentsInChildren<Rigidbody>(); // 자식 오브젝트들의 Rigidbody를 가져와서 rb 배열에 저장

        foreach (Rigidbody _rb in rb) // rb 배열의 모든 Rigidbody에 대해서
            _rb.isKinematic = true; // isKinematic 속성을 true로 설정하여 물리 연산을 비활성화

        foreach (Collider _col in col) // col 배열의 모든 Collider에 대해서
            _col.enabled = false; // enabled 속성을 false로 설정하여 충돌 검사를 비활성화
    }

    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) // 수평 또는 수직 입력이 있고, 스페이스바가 눌렸을 때
        {
            Vector3 vec = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")); // 입력 값을 벡터로 변환하여 vec 변수에 저장
            Crakeother(vec); // Crakeother 함수 호출
        }
        else if (Input.GetKeyDown(KeyCode.Space)) // 스페이스바가 눌렸을 때
            SpacCrake(); // Crake 함수 호출
    }

    private void Crake() // Crake 함수 정의
    {
        foreach (Rigidbody _rb in rb) // rb 배열의 모든 Rigidbody에 대해서
            _rb.isKinematic = false; // isKinematic 속성을 false로 설정하여 물리 연산을 활성화

        foreach (Collider _col in col) // col 배열의 모든 Collider에 대해서
            _col.enabled = true; // enabled 속성을 true로 설정하여 충돌 검사를 활성화
    }

    private void Crakeother(Vector3 input) // Crakeother 함수 정의
    {
        // Crake 함수 호출


        foreach (Rigidbody _rb in rb) // rb 배열의 모든 Rigidbody에 대해서
            _rb.AddForce(input * 10, ForceMode.Impulse); // 입력 값을 기반으로 힘을 가함





        Crake();

    }

    private void SpacCrake()
    {
        Crake();
        foreach (Rigidbody _rb in rb) // rb 배열의 모든 Rigidbody에 대해서
            _rb.AddExplosionForce(10000, transform.position, 50, 3000); // 폭발 효과를 추가

    }
}



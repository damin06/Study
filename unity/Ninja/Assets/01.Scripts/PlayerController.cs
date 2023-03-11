using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _maxspeed = 5;//속도
    private Rigidbody2D _rigidbody;//캐릭터를 움직일 Rigdbody
    private Animator _animator;//블렌드 트리를 제어할 애니메이터

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Vector2 dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); //X,Y값 입력
        _rigidbody.velocity = dir.normalized * _maxspeed;//움직임 제어
        AnimatorSet(dir);//애니메이션
    }

    private void AnimatorSet(Vector2 dir)
    {
        _animator.SetFloat("InputX", dir.x); //입력받은 X값을 파라미터의 변수 InputX에 할당해준다.
        _animator.SetFloat("InputY", dir.y); //입력받은 Y값을 파라미터의 변수 InputY에 할당해준다.
    }
}
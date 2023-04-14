using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public enum State
{
    IDEL,
    TRACE,
    ATTACK,
    DIE
}
public class MonsterController : MonoBehaviour
{
    [SerializeField] private Transform _playerPos;
    private NavMeshAgent _agent;
    public State _state = State.IDEL;
    private Animator _anim;
    private readonly int hasTrace = Animator.StringToHash("IsTrace");
    private readonly int hasAttack = Animator.StringToHash("IsAttack");

    public float _tracetDist = 10.0f;
    public float _attackDist = 2.0f;
    public bool _isDie = false;

    public UnityEvent OnDamageCast;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        StartCoroutine("checkMonsterState");
        _anim = GetComponent<Animator>();
        StartCoroutine(MonsterAction());
    }

    public void OnAnimationHit()
    {
        OnDamageCast?.Invoke();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        // if (_state == State.IDEL)
        // {
        //     _agent.SetDestination(transform.position);
        //     return;
        // }


        // _agent.SetDestination(_playerPos.position);
    }

    private bool CheckPlayer()
    {
        Vector3 bias = transform.forward;
        Vector3 pos = transform.position;
        pos.y += 1;
        for (int i = 0; i < 60; i++)
        {
            Vector3 dir = Quaternion.Euler(0, -i, 0) * bias;

            Ray ray = new Ray(pos, dir.normalized);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, _tracetDist))
            {
                if (hit.collider.gameObject.name == "Player")
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Vector3 bias = transform.forward;
        Vector3 pos = transform.position;
        pos.y += 1;
        for (int i = 0; i < 60; i++)
        {
            Vector3 dir = Quaternion.Euler(0, -i, 0) * bias;
            Gizmos.color = Color.red;
            Gizmos.DrawRay(pos, dir.normalized * _tracetDist);
        }
    }

    IEnumerator checkMonsterState()
    {
        while (!_isDie)
        {
            if (_state == State.DIE) break;


            float _distance = Vector2.Distance(_playerPos.position, transform.position);

            if (_distance <= _attackDist && CheckPlayer())
            {
                _state = State.ATTACK;
            }
            else if (_distance <= _tracetDist && CheckPlayer())
            {
                _state = State.TRACE;
            }
            else
            {
                _state = State.IDEL;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }

    IEnumerator MonsterAction()
    {
        while (!_isDie)
        {
            switch (_state)
            {
                case State.IDEL:
                    _agent.isStopped = true;
                    _anim.SetBool(hasTrace, false);
                    break;
                case State.TRACE:
                    _agent.SetDestination(_playerPos.position);
                    _agent.isStopped = false;
                    _anim.SetBool(hasTrace, true);
                    _anim.SetBool(hasAttack, false);
                    break;
                case State.ATTACK:
                    _anim.SetBool(hasAttack, true);
                    break;
                case State.DIE:
                    break;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }
}


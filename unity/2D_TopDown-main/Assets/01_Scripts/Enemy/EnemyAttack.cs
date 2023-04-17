using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class EnemyAttack : MonoBehaviour
{
    protected EnemyBrain _brain;

    public UnityEvent AttackFeedback;

    [SerializeField] protected float _atkDelay = 1f;

    [SerializeField] protected int _damage = 1;
    protected AIActionData _actionData;
    protected virtual void Awake()
    {
        _brain = GetComponent<EnemyBrain>();
        _actionData = transform.Find("AI").GetComponent<AIActionData>();
    }

    public abstract void Attack();

    protected IEnumerator WaitBeforeCooltime()
    {
        _actionData.IsAttack = true;
        yield return new WaitForSeconds(_atkDelay);
        _actionData.IsAttack = false;
    }
}

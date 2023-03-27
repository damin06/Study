using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AiDecision : MonoBehaviour
{
    public bool IsReverse = false;
    protected AIActionData _actionData;
    protected EnemyBrain _enemyBrain;

    public virtual void SetUp(Transform parent)
    {

    }

    public virtual void Setup(Transform parentTrm)
    {
        _actionData = parentTrm.Find("AI").GetComponent<AIActionData>();
    }

    public abstract bool MakeDecision();
}

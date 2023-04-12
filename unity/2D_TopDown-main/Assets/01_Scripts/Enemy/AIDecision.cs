using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIDecision : MonoBehaviour
{
    public bool IsReverse  = false;
    protected AIActionData _actionData;
    protected EnemyBrain _enemyBrain;

    public virtual void Setup(Transform parentTrm )
    {
        _actionData = parentTrm.Find("AI").GetComponent<AIActionData>();
        _enemyBrain = parentTrm.GetComponent<EnemyBrain>();
    }

    public abstract bool MakeADecision();
}

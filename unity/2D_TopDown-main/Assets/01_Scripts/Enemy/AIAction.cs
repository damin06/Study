using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIAction : MonoBehaviour
{
    protected AIActionData _actionData;
    protected EnemyBrain _enemyBrain;
    public virtual void SetUp(Transform parentTrm)
    {
        _actionData = parentTrm.Find("AI").GetComponent<AIActionData>();
        _enemyBrain = parentTrm.GetComponent<EnemyBrain>();
    }
    public abstract void TakeAction();
}

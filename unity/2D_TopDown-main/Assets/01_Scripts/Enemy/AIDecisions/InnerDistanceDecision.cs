using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerDistanceDecision : AiDecision
{
    public float Distance = 5f;
    public override bool MakeDecision()
    {
        float dis = Vector2.Distance(_enemyBrain.Target.transform.position, _enemyBrain.BasePosition.transform.position);

        return dis <= Distance;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (UnityEditor.Selection.activeGameObject == gameObject)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, Distance);
            Gizmos.color = Color.white;
        }
    }
#endif
}

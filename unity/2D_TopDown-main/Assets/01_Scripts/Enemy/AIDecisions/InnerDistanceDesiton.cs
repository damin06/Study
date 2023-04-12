using DG.Tweening.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerDistanceDesiton : AIDecision
{
    public float Distance = 5f;
    public override bool MakeADecision()
    {
        float dis = Vector2.Distance(_enemyBrain.Target.position, _enemyBrain.BasePosition.position);
        return dis <= Distance;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if(UnityEditor.Selection.activeGameObject == gameObject)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, Distance);
            Gizmos.color = Color.white;
        }
    }
#endif
}

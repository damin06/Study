using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseAction : AIAction
{
    public override void TakeAction()
    {
        Vector2 dir = _enemyBrain.Target.position - _enemyBrain.BasePosition.position;

        _enemyBrain.Move(dir.normalized, _enemyBrain.Target.position);
    }
}

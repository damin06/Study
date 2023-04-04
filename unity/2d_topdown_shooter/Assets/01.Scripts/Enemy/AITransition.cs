using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AITransition : MonoBehaviour
{
    public List<AIDecision> decisions;

    public AIState TransitionState; //전이할 상태

    private void Awake()
    {
        GetComponents<AIDecision>(decisions); //모든 디시전을 가져와서 리스트를 만든다.
    }

    public void Setup(Transform parentTrm)
    {
        decisions.ForEach(d => d.SetUp(parentTrm));
    }

    public bool CanTransition()
    {
        bool result = false;
        foreach (AIDecision d in decisions)
        {
            result = d.MakeADecision();
            if (d.IsReverse)
                result = !result;

            if (result == false)
                break;
        }

        return result;
    }
}

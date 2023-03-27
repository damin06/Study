using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITransition : MonoBehaviour
{
    public List<AiDecision> decisions;

    public AIState TransitionState;

    public void SetUp(Transform parentTrm)
    {
        decisions.ForEach(d => d.Setup(parentTrm));
    }

    public bool CanTransition()
    {
        bool result = false;
        foreach (AiDecision d in decisions)
        {
            result = d.MakeDecision();
            if (d.IsReverse)
                result = !result;

            if (result == false)
                break;
        }

        return result;
    }
}

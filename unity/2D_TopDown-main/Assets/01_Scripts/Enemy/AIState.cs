using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIState : MonoBehaviour
{
    public List<AIAction> Actions = new List<AIAction>();
    public List<AITransition> Transitions = new List<AITransition>();

    private EnemyBrain _brain;

    private void Awake()
    {
        GetComponentsInChildren<AITransition>(Transitions); //내 자식에 있는 전이들 전부 가져와서 실행
        GetComponents<AIAction>(Actions); //나한테 붙어있는 액션 전부 가져와서 실행
    }

    public void SetUp(Transform parentTrm)
    {
        _brain = parentTrm.GetComponent<EnemyBrain>();
        Actions.ForEach(a => a.SetUp(parentTrm));
        Transitions.ForEach(t => t.Setup(parentTrm));
    }

    public void UpdateState()
    {
        foreach(AIAction act in Actions)
        {
            act.TakeAction();
        }

        foreach(AITransition t in Transitions)
        {
            if(t.CanTransition())
            {
                _brain.ChangeState(t.TransitionState);
            }
        }
    }
}

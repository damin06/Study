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
        GetComponentsInChildren<AITransition>(Transitions); //�� �ڽĿ� �ִ� ���̵� ���� �����ͼ� ����
        GetComponents<AIAction>(Actions); //������ �پ��ִ� �׼����� �����ͼ� ����
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
            act.TakeAction(); //�� ���¿��� �ؾ��� �׼��� ��� �����ϰ�
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

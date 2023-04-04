using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Feedback : MonoBehaviour
{
    public abstract void CreateFeedback(); //�ǵ�� ����
    public abstract void CompleteFeedback();  //�ǵ�� ����
        
    protected virtual void OnDestroy()
    {
        CompleteFeedback();
    }

    protected virtual void OnDisable()
    {
        CompleteFeedback();
    }
}

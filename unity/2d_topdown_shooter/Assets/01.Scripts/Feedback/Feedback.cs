using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Feedback : MonoBehaviour
{
    public abstract void CreateFeedback(); //피드백 시작
    public abstract void CompleteFeedback();  //피드백 종료
        
    protected virtual void OnDestroy()
    {
        CompleteFeedback();
    }

    protected virtual void OnDisable()
    {
        CompleteFeedback();
    }
}

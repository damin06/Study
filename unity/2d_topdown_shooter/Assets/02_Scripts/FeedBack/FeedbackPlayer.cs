using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackPlayer : MonoBehaviour
{
    [SerializeField] private List<FeedBack> _feedbackToPlay = null;

    public void PlayeFeedback()
    {
        FinishFeedback();
        foreach (FeedBack f in _feedbackToPlay)
        {
            f.CreatFeedBack();
        }
    }

    public void FinishFeedback()
    {
        foreach (FeedBack f in _feedbackToPlay)
        {
            f.CompleteFeedback();
        }
    }
}

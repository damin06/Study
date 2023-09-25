using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackPlayer : MonoBehaviour
{
    private List<Feedback> _feedbackToPlay = null;
    private void Awake()
    {
        _feedbackToPlay = new List<Feedback>();
        GetComponents<Feedback>(_feedbackToPlay);
    }

    public void PlayFeedback()
    {
        FinishFeedback();
        foreach (var f in _feedbackToPlay)
        {
            f.CreateFeedback();
        }
    }

    public void FinishFeedback()
    {
        foreach(var f in _feedbackToPlay)
        {
            f.CompleteFeedback();
        }
    }
}

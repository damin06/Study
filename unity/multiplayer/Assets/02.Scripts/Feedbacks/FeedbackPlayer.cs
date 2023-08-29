using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackPlayer : MonoBehaviour
{
    private List<Feedback> _feefbakcToPlay = null;

    private void Awake()
    {
        _feefbakcToPlay = new List<Feedback>();
        GetComponents<Feedback>(_feefbakcToPlay);
    }

    public void PlayFeedbakc()
    {
        FinishFeedback();

        foreach (var f in _feefbakcToPlay)
            f.CreateFeedback();
    }

    public void FinishFeedback()
    {
        foreach (var f in _feefbakcToPlay)
            f.CompleteFeedback();
    }
}

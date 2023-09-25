using UnityEngine;

public abstract class Feedback : MonoBehaviour
{
    public abstract void CreateFeedback();
    public abstract void CompleteFeedback();

    protected virtual void OnDestroy()
    {
        CompleteFeedback();
    }
    protected virtual void OnDisable()
    {
        CompleteFeedback();
    }
}

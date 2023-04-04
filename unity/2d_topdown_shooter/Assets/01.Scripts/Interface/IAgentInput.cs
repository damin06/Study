using UnityEngine;
using UnityEngine.Events;

public interface IAgentInput
{
    public UnityEvent<Vector2> OnMovementKeyPress { get; set; }
    public UnityEvent<Vector2> OnPointerPositionChanged { get; set; }
    public UnityEvent OnFireButtonPress { get; set; }
    public UnityEvent OnFireButtonRelease { get; set; }
}

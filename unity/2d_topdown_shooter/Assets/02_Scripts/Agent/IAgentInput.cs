using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IAgentInput
{
    public UnityEvent<Vector2> OnMovementkeyPress { get; set; }
    public UnityEvent<Vector2> OnPointerPositionChanged { get; set; }
    public UnityEvent OnFiredBittonPress { get; set; }
    public UnityEvent OnFireBurronRelease { get; set; }


}

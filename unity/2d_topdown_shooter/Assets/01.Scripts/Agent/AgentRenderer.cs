using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentRenderer : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();   
    }

    public void FaceDirection(Vector2 pointerInput)
    {
        Vector3 direction = (Vector3)pointerInput - transform.position;
        Vector3 result = Vector3.Cross(Vector2.up, direction);

        if(result.z > 0)
        {
            _spriteRenderer.flipX = true;
        }else if(result.z < 0)
        {
            _spriteRenderer.flipX = false;
        }
    }
}

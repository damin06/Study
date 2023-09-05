using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DecalCircle : MonoBehaviour
{
    [Header("참조 변수")]
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public bool showDecal = false;

    public void OpenCircle(Vector3 point, float radius)
    {
        _spriteRenderer.color = new Color(1, 1, 1, 0); //투명상태로 변경
        transform.position = point;
        transform.localScale = Vector3.one;

        showDecal = true;
        Sequence seq = DOTween.Sequence();
        seq.Append(_spriteRenderer.DOFade(1, 0.3f));
        seq.Append(transform.DOScale(Vector3.one * (radius * 2), 0.8f));
    }

    public void CloseCircle()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOScale(Vector3.zero, 0.8f));
        seq.Join(_spriteRenderer.DOFade(0, 0.8f));
        showDecal = false;
    }

    //private void Update()
    //{
    //    if(Keyboard.current.qKey.wasPressedThisFrame)
    //    {
    //        OpenCircle(transform.position, 8);
    //    }
    //    if(Keyboard.current.eKey.wasPressedThisFrame)
    //    {
    //        CloseCircle();
    //    }
    //}
}

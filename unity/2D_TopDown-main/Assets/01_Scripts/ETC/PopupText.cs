using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class PopupText : PoolableMono
{
    private TextMeshPro _textMesh;

    private void Awake()
    {
        _textMesh = GetComponent<TextMeshPro>();

    }

    public void SetUp(string text, Vector3 pos, Color color, float fontSize = 7f)
    {
        transform.position = pos;
        _textMesh.SetText(text);
        _textMesh.color = color;
        _textMesh.fontSize = fontSize;

        ShowingSequence(1.5f);
    }

    public void ShowingSequence(float time)
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(transform.DOMoveY(transform.position.y + 0.5f, time));
        seq.Join(_textMesh.DOFade(0, 1f));
        seq.AppendCallback(() =>
        {
            PoolManager.Instance.Push(this);
        });
    }

    public override void Reset()
    {
        _textMesh.color = Color.white;
        _textMesh.fontSize = 7f;
        _textMesh.alpha = 1f;
    }
}

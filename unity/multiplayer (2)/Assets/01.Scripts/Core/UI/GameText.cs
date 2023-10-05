using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameText : MonoBehaviour
{
    [SerializeField] private TextMeshPro _textMesh;

    public void SetUpText(string value, Vector3 position, Color color)
    {
        _textMesh.text = value;
        _textMesh.color = color;

        Sequence seq = DOTween.Sequence();

        transform.position = position;  
        
        seq.Append(transform.DOMove(position + new Vector3(0, 3f, 0), 0.8f));
        seq.Join(_textMesh.DOFade(0, 0.8f));

        seq.AppendCallback(() =>
        {
            Destroy(gameObject);
        });
    }
}

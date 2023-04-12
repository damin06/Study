using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GGM : MonoBehaviour
{
    [SerializeField] private float _speed = 3f;
    private Vector2 _destination;
    private Camera _mainCam;
    private Tween t = null;
    private SpriteRenderer sr;

    [SerializeField] float time;
    [SerializeField] float y;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(255f, 1f, 1f, 0);
    }

    private void Start()
    {
        _mainCam = Camera.main;
        _destination = transform.position;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Sequence seq = DOTween.Sequence();
            seq.AppendInterval(time);
            seq.Append(transform.DOMove(new Vector2(transform.position.x, y), 1f));
            seq.Join(sr.DOFade(2f, y));
            seq.AppendInterval(y);
            seq.Append(transform.DOScale(1.5f, 1f));
            seq.Join(transform.DORotate(new Vector3(0, 0, 360f), 1f, RotateMode.FastBeyond360));
            seq.AppendInterval(1f);
            seq.Append(transform.DOScale(1f, 1f));
        }
        //{
        //    _destination = _mainCam.ScreenToWorldPoint(Input.mousePosition);

        //    Sequence seq = DOTween.Sequence();
        //    sr.DOFade(5f, 1f);
        //    seq.AppendInterval(1f);
        //    seq.Append(transform.DOMove(_destination, 1f).SetEase(Ease.InCirc));
        //    seq.Join(transform.DORotate(new Vector3(0, 0, 360f), 1f, RotateMode.FastBeyond360));

        //    seq.AppendInterval(1f);

        //    seq.AppendCallback(() =>
        //    {
        //        Debug.Log("¿Ï·á");
        //    });

        //}
    }
}

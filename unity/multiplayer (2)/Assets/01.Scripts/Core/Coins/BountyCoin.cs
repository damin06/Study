using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System;
using DG.Tweening;
using Cinemachine;

public class BountyCoin : Coin
{
    [SerializeField] private CinemachineImpulseSource _impulseSource;
    public override int Collect()
    {
        if (!IsServer)
        {
            SetVisible(false);
            return 0;
        }
        if(_alreadyCollected) return 0;

        _alreadyCollected = true;   
        Destroy(gameObject);
        return _coinValue;
    }

    public void setCoinToVisible(float coinScale)
    {
        isActive.Value = true;
        CoinspawnClientRpc(coinScale);
    }

    [ClientRpc]
    private void CoinspawnClientRpc(float coinScale)
    {
        Vector3 destination = transform.position;
        Vector3 destinationScale = coinScale * Vector3.one;


        Sequence seq = DOTween.Sequence()
            .OnStart(() =>
            {
                transform.position = transform.position + new Vector3(0, 3f, 0);
                transform.localScale = 0.5f * Vector3.one;
            })
            .Append(transform.DOMove(destination, 0.8f).SetEase(Ease.OutBounce))
            .Join(transform.DOScale(destinationScale, 0.8f))
            .AppendCallback(() =>
            {
                _impulseSource.GenerateImpulse(0.3f);
            });
    }
}

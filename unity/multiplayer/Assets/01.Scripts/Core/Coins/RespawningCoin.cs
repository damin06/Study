using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawningCoin : Coin
{
    public event Action<RespawningCoin> OnCollected;
    //이건 코인스포너에서 구독할 예정
    private Vector2 _prevPos;


    //먹은 코인 수를 반환하는거야
    public override int Collect()
    {
        if (_alreadyCollected) return 0;

        if(!IsServer)
        {
            SetVisible(false); 
            return 0; //클라는 그냥 보이지 않게만 즉시 처리
        }

        //서버만 수행한다.
        _alreadyCollected = true;
        OnCollected?.Invoke(this);
        //isActive.Value = false;

        return _coinValue;
    }

    //이것도 서버만 실행할거다
    public void Reset()
    {
        _alreadyCollected = false;
        isActive.Value = true;
        SetVisible(true); //이걸 바꾼다고 클라이언트 코인이 다시 보이게 될까?
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        _prevPos = transform.position;
    }

    private void Update()
    {
        if (IsServer) return;
        if(Vector2.Distance(_prevPos, transform.position) >= 0.1f)
        {
            _prevPos = transform.position;
            SetVisible(true);
        }
    }
}

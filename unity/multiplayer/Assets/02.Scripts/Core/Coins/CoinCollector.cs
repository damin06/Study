using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class CoinCollector : NetworkBehaviour
{
  public NetworkVariable<int> totalCoins = new NetworkVariable<int>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent<Coin>(out Coin coin))
        {
            int value = coin.Collect();
            if (!IsClient) return;
            totalCoins.Value += value;
        }


    }
}

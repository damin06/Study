using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class SpawnPoint : NetworkBehaviour
{
    public string pointName;
    public Vector3 Position => transform.position;
    [field: SerializeField] public float Radius { get; private set; } = 10f;

    public List<Vector3> spawnPointList { get; private set; }

    public override void OnNetworkSpawn()
    {
        if(IsServer)
        {
            spawnPointList = MapManager.Instance.GetAvailablePositionList(Position, Radius);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Radius);
        Gizmos.color = Color.white;
    }
#endif
}

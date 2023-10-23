using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public struct GameData : INetworkSerializable, IEquatable<GameData>
{
    public ulong clientID;
    public FixedString32Bytes playerName;
    public bool ready;
    public ushort colorIdx;

    public bool Equals(GameData other)
    {
        return clientID == other.clientID && playerName.Equals(other.playerName);
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref clientID);
        serializer.SerializeValue(ref playerName);
        serializer.SerializeValue(ref ready);
        serializer.SerializeValue(ref colorIdx);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class UserData
{
    public string username;
    public string userAuthId;

    public ArraySegment<byte> Serialize()
    {

        ArraySegment<byte> segment = SendBufferHelper.Open(1024);

        Span<byte> span = new Span<byte>(segment.Array, segment.Offset, segment.Count);

        ushort count = 0;
        bool success = true;

        ushort nameLen = (ushort)Encoding.UTF8.GetByteCount(username);
        success &= BitConverter.TryWriteBytes(span.Slice(count, span.Length - count), nameLen);
        count += sizeof(ushort);

        byte[] namearr = Encoding.UTF8.GetBytes(username);
        Array.Copy(namearr, 0, segment.Array, count, nameLen);
        count += nameLen;

        ushort authLen = (ushort)Encoding.UTF8.GetByteCount(userAuthId);
        success &= BitConverter.TryWriteBytes(span.Slice(count, span.Length - count), authLen);
        count += sizeof(ushort);

        byte[] autharr = Encoding.UTF8.GetBytes(userAuthId);
        Array.Copy(autharr, 0, segment.Array, count, authLen);
        count += authLen;

        if(!success)
        {
            Debug.LogError("Packet serialize error!");
            return null;
        }
        
        return SendBufferHelper.Close(count);
    }

    public void Deserialize(byte[] payload)
    {
        int count = 0;
        ushort nameLen = BitConverter.ToUInt16(payload, count);
        count += sizeof(ushort);
        username = Encoding.UTF8.GetString(payload, count, nameLen);
        count += nameLen;
        
        ushort authLen = BitConverter.ToUInt16(payload, count);
        count += sizeof(ushort);
        userAuthId = Encoding.UTF8.GetString(payload, count, authLen);
    }
}

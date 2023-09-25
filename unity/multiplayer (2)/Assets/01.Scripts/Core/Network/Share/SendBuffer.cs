using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SendBuffer
{
    private byte[] _buffer; //여기다가 바이트를 잔뜩 할당해두고 쓰다가 모자라면 버린다.
    private int _usedSize = 0; //현재 얼마나 썼냐?

    public int FreeSize => _buffer.Length - _usedSize;

    public SendBuffer(int chunckSize)
    {
        _buffer = new byte[chunckSize];
    }

    public ArraySegment<byte> Open(int reserveSize)
    {
        if(reserveSize > FreeSize)
        {
            return null;
        }
        return new ArraySegment<byte>(_buffer, _usedSize, reserveSize);
    }

    public ArraySegment<byte> Close(int usedSize)
    {
        ArraySegment<byte> segment = new ArraySegment<byte>(_buffer, _usedSize, usedSize);
        _usedSize += usedSize;
        return segment;
    }
}

public class SendBufferHelper
{
    public static ThreadLocal<SendBuffer> CurrentBuffer = new ThreadLocal<SendBuffer>(() => null);

    public static int ChunkSize { get; set; } = 4096 * 100;

    public static ArraySegment<byte> Open(int reservedSize)
    {
        if(CurrentBuffer.Value == null)
        {
            CurrentBuffer.Value = new SendBuffer(ChunkSize);
        }

        if(CurrentBuffer.Value.FreeSize < reservedSize)
        {
            CurrentBuffer.Value = new SendBuffer(ChunkSize);
        }

        return CurrentBuffer.Value.Open(reservedSize);
    }

    public static ArraySegment<byte> Close(int usedSize)
    {
        return CurrentBuffer.Value.Close(usedSize);
    }

}

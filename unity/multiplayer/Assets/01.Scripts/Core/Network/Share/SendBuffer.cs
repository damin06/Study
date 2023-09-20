using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SendBuffer 
{
    private byte[] _buffer;
    private int _userdSize = 0;

    public int FreeSize => _buffer.Length - _userdSize;

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
        return new ArraySegment<byte>(_buffer, _userdSize, reserveSize);
    } 

    public ArraySegment<byte> Close(int usedSize)
    {
        ArraySegment<byte> segment = new ArraySegment<byte>(_buffer, _userdSize, usedSize);
        _userdSize += usedSize;
        return segment;
    }
}

public class SendBufferHelper
{
    public static ThreadLocal<SendBuffer> CurrentBuffer = new ThreadLocal<SendBuffer>(() => null);

    public static int ChunkSize { get; set; } = 4096 * 100;

    public static ArraySegment<byte> Open(int reserveSize) 
    {
        if(CurrentBuffer.Value == null)
        {
            CurrentBuffer.Value = new SendBuffer(ChunkSize);    
        }

        if(CurrentBuffer.Value.FreeSize < reserveSize)
        {
            CurrentBuffer.Value = new SendBuffer(ChunkSize);
        }
        return CurrentBuffer.Value.Open(reserveSize);   
    }

    public static ArraySegment<byte> Close(int count)
    {
        return CurrentBuffer.Value.Close(count);  
    }
}

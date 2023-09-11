using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class UserData 
{
    public string username;

    public byte[] Serialize()
    {
        byte[] strBuffer = Encoding.UTF8.GetBytes(username);    
        ushort strLen = (ushort)strBuffer.Length;
        byte[] lenBuffer = BitConverter.GetBytes(strLen);

        byte[] result = new byte[lenBuffer.Length + strBuffer.Length]; 
        Array.Copy(lenBuffer, 0, result, 0, lenBuffer.Length);  
        Array.Copy(lenBuffer, 0, result, lenBuffer.Length, strBuffer.Length);

        return result;
    }

    public void Deserialize(byte[] payload)
    {
        ushort len = BitConverter.ToUInt16(payload, 0); 
        username = Encoding.UTF8.GetString(payload, 2, len);
    }
}

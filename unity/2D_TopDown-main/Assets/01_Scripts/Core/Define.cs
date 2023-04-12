using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType
{
    None,
    Helath,
    Ammo,
    Coin
}
public class Define : MonoBehaviour
{

    public static ItemType itemType;
    protected static Camera _mainCam = null;
    public static Camera MainCam
    {
        get
        {
            if (_mainCam == null) _mainCam = Camera.main;
            return _mainCam;
        }
    }
}

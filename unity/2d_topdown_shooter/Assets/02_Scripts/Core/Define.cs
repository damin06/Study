using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define : MonoBehaviour
{
    private static Camera _mainCam = null;
    public static Camera MainCam
    {
        get
        {
            if (_mainCam == null) _mainCam = Camera.main;
            return _mainCam;
        }
    }
}

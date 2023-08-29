using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private static CameraManager _instance; 
    public static CameraManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new CameraManager();
            return _instance;
        }
    }

    private Camera _mainCam;
    public Camera MinCam
       {
            get
            {
                if( _mainCam == null)   
                    _mainCam = Camera.main;
                return _mainCam;
            }
       }

    //private CamerManager()
    //{

    //}
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class CameraAction : MonoBehaviour
{
    public static CameraAction Instance { get; private set; }
    private CinemachineVirtualCamera _virCam;
    private CinemachineBasicMultiChannelPerlin _multiCha;


    private float _totalTime = 0;
    private float _currentTime;
    private float _statrtInitensity;

    private void Awake()
    {
        Instance = this;
        _virCam = GetComponent<CinemachineVirtualCamera>();
        _multiCha = _virCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Update()
    {
        if (_currentTime > 0)
        {
            _currentTime -= Time.deltaTime;
            if (_currentTime < 0) _currentTime = 0;
            _multiCha.m_AmplitudeGain = Mathf.Lerp(_statrtInitensity, 0f, 1 - _currentTime / _totalTime);
        }
    }

    public void ShakeCam(float intensity, float time)
    {
        _multiCha.m_AmplitudeGain = intensity;
        _totalTime = _currentTime = time;
        _statrtInitensity = intensity;
    }

}

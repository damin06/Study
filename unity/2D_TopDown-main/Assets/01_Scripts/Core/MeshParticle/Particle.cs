using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class Particle
{
    private Vector3 _quadPosition;
    private Vector3 _direction;
    private MeshParticleSystem _meshParticleSystem;
    private int _quadIndex;
    private Vector3 _quadSize;
    private float _rotation;
    private int _uvIndex;

    private float _moveSpeed;
    private float _slowDownFactor; //천천히 느려지다가 정지하도록 만들 것

    private bool _isRotate;
    public int QuadIndex => _quadIndex;

    public Particle(Vector3 quadPosition, Vector3 direction,
                MeshParticleSystem meshParticleSystem,
                Vector3 quadSize, float rotation, int uvIndex, float moveSpeed,
                float slowDownFactor, bool isRotate = false)
    {
        _quadPosition = quadPosition;
        _direction = direction;
        _meshParticleSystem = meshParticleSystem;
        _quadSize = quadSize;
        _rotation = rotation;
        _uvIndex = uvIndex;
        _moveSpeed = moveSpeed;
        _slowDownFactor = slowDownFactor;
        _isRotate = isRotate;
        _quadIndex = _meshParticleSystem.AddQuad(_quadPosition, _rotation, _quadSize, false, _uvIndex);
    }

    public void UpdateParticle()
    {
        _quadPosition += _direction * _moveSpeed * Time.deltaTime;
        if(_isRotate)
        {
            _rotation += 360f * (_moveSpeed * 0.1f) * Time.deltaTime;
        }

        _meshParticleSystem.UpdateQuad(_quadIndex, _quadPosition, _rotation, _quadSize,false, _uvIndex);

        _moveSpeed -= _moveSpeed * _slowDownFactor * Time.deltaTime;
    }

    public bool IsComplete()
    {
        return _moveSpeed < 0.05f;
    }
}

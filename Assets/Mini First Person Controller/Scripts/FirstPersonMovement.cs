using System.Collections.Generic;
using UnityEngine;
using System;

public class FirstPersonMovement : MonoBehaviour
{
    [SerializeField] float speed = 5;
    [Header("Running")]
    [SerializeField] bool canRun = true;
    [SerializeField] float runSpeed = 9;
    [SerializeField] KeyCode runningKey = KeyCode.LeftShift;

    List<Func<float>> _speedOverrides = new();
    Rigidbody _myRigidbody;
    float _moveSpeed;

    public bool IsRunning { get; private set; }
    public List<Func<float>> SpeedOverrides => _speedOverrides;

    void Awake()
    {
        _myRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        IsRunning = canRun && Input.GetKey(runningKey);

        if (_speedOverrides.Count > 0)
            _moveSpeed = _speedOverrides[^1]();
        else
            _moveSpeed = IsRunning ? runSpeed : speed;


        Vector2 targetVelocity = new Vector2( Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * _moveSpeed;

        _myRigidbody.velocity = transform.rotation * new Vector3(targetVelocity.x, _myRigidbody.velocity.y, targetVelocity.y);
    }
}
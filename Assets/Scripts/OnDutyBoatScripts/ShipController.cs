using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    private Rigidbody _rb;
    private float verticalInput;
    private float horizontalInput;

    [SerializeField] private HingeJoint _steeringW;
    [SerializeField] private HingeJoint _throttle;
    [SerializeField] Transform boatMotor;
    [SerializeField] float _speed = 5f;
    [SerializeField] float _steeringSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
       
        verticalInput = _throttle.angle / _throttle.limits.max;        
        horizontalInput = _steeringW.angle / _steeringW.limits.max;

        if (Mathf.Abs(verticalInput) < .2f)
            verticalInput = 0;

        if (Mathf.Abs(horizontalInput) < .3f)
            horizontalInput = 0;

    }

    private void FixedUpdate()
    {
        Movement(verticalInput);
        Steering(horizontalInput);
    }

    private void Movement(float multiplyer)
    {
        multiplyer = Mathf.Clamp(multiplyer, -1f, 1f);
        _rb.AddForce(transform.forward * multiplyer * _speed * Time.deltaTime);
        
    }

    private void Steering(float multiplyer)
    {
        if (_rb.velocity.magnitude > .1f)
        {
            multiplyer = Mathf.Clamp(multiplyer, -1f, 1f);
            _rb.AddForceAtPosition(-transform.right * (multiplyer * verticalInput) * _steeringSpeed * Time.deltaTime, boatMotor.position);
        }
           

    }
}

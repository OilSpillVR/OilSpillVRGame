using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    private Rigidbody _rb; // Boat rigidbody
    private float verticalInput;
    private float horizontalInput;

    [SerializeField] private HingeJoint _steeringW; // Steering wheel hinge joint
    [SerializeField] private HingeJoint _throttle; // Throttle hinge joint
    [SerializeField] Transform boatMotor; // Boat motor position
    [SerializeField] float _speed = 5f;
    [SerializeField] float _steeringSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>(); // Get boat rb
    }

    // Update is called once per frame
    void Update()
    {
       
        //Set vertical and horizontal movement values to be in between 0 and 1 by dividing the hingel joitn angle with set max angle
        verticalInput = _throttle.angle / _throttle.limits.max;        
        horizontalInput = _steeringW.angle / _steeringW.limits.max;

        // To zero the values so that steerign or movement stop even the values are not equal to 0.
        if (Mathf.Abs(verticalInput) < .2f)
            verticalInput = 0;

        if (Mathf.Abs(horizontalInput) < .3f)
            horizontalInput = 0;

    }

    private void FixedUpdate()
    {
        // Call movement and rotaion controls
        Movement(verticalInput);
        Steering(horizontalInput);
    }

    private void Movement(float multiplyer)
    {
        multiplyer = Mathf.Clamp(multiplyer, -1f, 1f); // clamping the value between 0 and 1 just to be sure it won't be anything else
        _rb.AddForce(transform.forward * multiplyer * _speed * Time.deltaTime); // Add force to boat rb
        
    }

    private void Steering(float multiplyer)
    {
        if (_rb.velocity.magnitude > .1f) // If boat rb is moving
        {
            multiplyer = Mathf.Clamp(multiplyer, -1f, 1f); // clamping the value between 0 and 1 just to be sure it won't be anything else
            _rb.AddForceAtPosition(-transform.right * (multiplyer * verticalInput) * _steeringSpeed * Time.deltaTime, boatMotor.position); //add force to specific position
        }
           

    }
}

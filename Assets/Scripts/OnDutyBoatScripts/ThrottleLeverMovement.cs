using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrottleLeverMovement : MonoBehaviour
{
    [SerializeField] private Transform _leverTopPosition;
    [SerializeField] private GameObject _leverObjectToFollow;
    private Rigidbody _rbObjectToFollow;
    private Rigidbody _rb;
    private bool isGrabbed;



    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rbObjectToFollow = _leverObjectToFollow.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Keep Boat throttle Vector3.up pointin to leverObjectToFollow
        transform.LookAt(_leverObjectToFollow.transform.position);

        if (!isGrabbed)
        {
            // Setting leverObjectToFollow position to lever top position and setting RB velocities to Zero
            _leverObjectToFollow.transform.position = _leverTopPosition.transform.position;
            _leverObjectToFollow.transform.rotation = _leverTopPosition.transform.rotation;
            _rbObjectToFollow.velocity = Vector3.zero;
            _rbObjectToFollow.angularVelocity = Vector3.zero;

        }

    }    

    public void OnGrab()
    {
        isGrabbed = true;

        Debug.Log("Grabbed Throttle");
    }

    public void OnRelease()
    {        
        isGrabbed = false;

        Debug.Log("Released Throttle");
    }

}

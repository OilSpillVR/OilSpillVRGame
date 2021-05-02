using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Transform[] _waypoints;
    private int _waypointIndex;    

    // Start is called before the first frame update
    void Start()
    {
        _waypointIndex = 0;        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void ActivateDrone()
    {
        StartCoroutine(StartDrone());
        Debug.Log("Drone Called");
    }

    IEnumerator StartDrone()
    {
        while (true)
        {           
            // movement between waypoints
            transform.position = Vector3.MoveTowards(transform.position, _waypoints[_waypointIndex].position, _moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, _waypoints[_waypointIndex].position) < .2f)
            {
                _waypointIndex++;

                if (_waypointIndex > _waypoints.Length - 1) // Go back to first waypoint
                    _waypointIndex = 0;
            }

            yield return null;
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    [SerializeField] private Transform _target;

    // Start is called before the first frame update
    void Start()
    {
        //Find gameobject with tag "target" automatically. Un comment to take into use
        //_target = GameObject.FindGameObjectWithTag("target").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(_target, Vector3.up);
    }
}

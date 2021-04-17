using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonitorController : MonoBehaviour
{
    [SerializeField] private GameObject _planeMonitor;
    private bool _isPlaneMonitorOn;

    private void Start()
    {
        _isPlaneMonitorOn = false;
        _planeMonitor.SetActive(_isPlaneMonitorOn);

    }


    public void TurnOnPlaneMonitor()
    {
        Debug.Log("MonitorButton pressed");

        _isPlaneMonitorOn = !_isPlaneMonitorOn;
        _planeMonitor.SetActive(_isPlaneMonitorOn);        
    }

    
}

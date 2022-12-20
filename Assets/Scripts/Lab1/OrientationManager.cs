using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class OrientationManager : MonoBehaviour
{
    
    public float smoothFactor;
    private TrackerController _trackerController;
    private Vector3 _orientation;
    // Start is called before the first frame update
    void Start()
    {
        // set reference for tracker
        _trackerController = GetComponent<TrackerController>();
        smoothFactor = 0.01f;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    private void LateUpdate()
    {
        // if (_trackerController.local_velocity.magnitude > 0.0)
        // {
        //     Debug.Log("OM Orientation: " + _orientation);
        // }
        transform.forward = Vector3.Slerp(transform.forward, _trackerController.world_velocity, smoothFactor);
    }
}

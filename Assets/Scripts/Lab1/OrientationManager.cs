using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class OrientationManager : MonoBehaviour
{
    
    public float smoothFactor = 0.1f;
    public bool fixedOrientation = false;
    private TrackerController _trackerController;
    private Vector3 _orientation;
    // Start is called before the first frame update
    void Start()
    {
        // set reference for tracker
        _trackerController = GetComponent<TrackerController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    private void LateUpdate()
    {
        if (!fixedOrientation)
        {
            transform.forward = Vector3.Slerp(transform.forward, _trackerController.world_velocity, smoothFactor);
        }
    }
}

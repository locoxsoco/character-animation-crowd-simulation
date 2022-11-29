using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class OrientationManager : MonoBehaviour
{
    
    public float smoothFactor = 0.1f;
    private TrackerController _trackerController;
    private Vector3 _orientation;
    // Start is called before the first frame update
    void Start()
    {
        // set reference for tracker
        _trackerController = GetComponent<TrackerController>();
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {
        _orientation = Vector3.Lerp(_trackerController.orientation, _trackerController.local_velocity, smoothFactor);
        Debug.Log(_orientation);
        float angle = Vector3.Angle(Vector3.forward, _orientation);
        transform.rotation = Quaternion.Euler(Vector3.up*angle);
    }
}

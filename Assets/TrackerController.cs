using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackerController : MonoBehaviour
{
    private Transform transform;
    private Vector3 pos;
    private Vector3 prev_pos;
    private Vector3 world_displacement;
    private Vector3 local_displacement;

    public Vector3 orientation;
    public Vector3 world_velocity;
    public Vector3 local_velocity;
    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();
        pos = transform.position;
        prev_pos = pos;
        world_displacement = Vector3.zero;
        local_displacement = Vector3.zero;
        orientation = transform.rotation.eulerAngles;
        world_velocity = Vector3.zero;
        local_velocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        prev_pos = pos;
        pos = transform.position;
        orientation = transform.rotation * Vector3.forward;
        world_displacement = pos - prev_pos;
        local_displacement = world_displacement;
        world_velocity = world_displacement / Time.deltaTime;
        local_velocity = local_displacement / Time.deltaTime;
    }

    private void OnDrawGizmos()
    {
        // Forward vector
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(pos + Vector3.up*3/2,pos + orientation + Vector3.up*3/2);
        // World Displacement vector
        Gizmos.color = Color.red;
        Gizmos.DrawLine(prev_pos + Vector3.up/2,pos + Vector3.up/2);
        // World velocity vector
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(pos + Vector3.up,pos + world_velocity + Vector3.up);
        
    }
}

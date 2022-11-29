using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackerController : MonoBehaviour
{
    private Transform transform;
    private Vector3 pos;
    private Vector3 prev_pos;
    private Vector3 displacement;

    public Vector3 orientation;
    public Vector3 velocity;
    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();
        pos = transform.position;
        prev_pos = pos;
        displacement = Vector3.zero;
        orientation = transform.rotation.eulerAngles;
        velocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        prev_pos = pos;
        pos = transform.position;
        displacement = pos - prev_pos;
        orientation = transform.rotation * Vector3.forward;
        velocity = displacement / Time.deltaTime;
    }

    private void OnDrawGizmos()
    {
        // Forward vector
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(pos + Vector3.up*3/2,pos + orientation + Vector3.up*3/2);
        // Speed vector
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(pos + Vector3.up,pos + velocity + Vector3.up);
        // Displacement vector
        Gizmos.color = Color.red;
        Gizmos.DrawLine(prev_pos + Vector3.up/2,pos + Vector3.up/2);
    }
}

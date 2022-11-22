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
        // orientation = obtener de rotation quizas con lerp
        velocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        
    }
}

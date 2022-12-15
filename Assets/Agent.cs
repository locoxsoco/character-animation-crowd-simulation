using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public float maxSpeed, radius;

    public Vector3 velocity; // magnitude indicates speed
    // Start is called before the first frame update
    void Start()
    {
        maxSpeed = 5;
        radius = 2;
        velocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

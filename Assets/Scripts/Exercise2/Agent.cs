using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public float maxSpeed = 5, radius = 2, slowingDistance = 2.0f;
    public bool slowingArrival = false;

    public Vector3 velocity; // magnitude indicates speed
    // Start is called before the first frame update
    void Start()
    {
        velocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody>().position += Time.deltaTime * velocity;
    }
}

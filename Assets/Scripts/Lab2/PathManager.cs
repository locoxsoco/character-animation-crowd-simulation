using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    public Vector3 goal;
    public float distanceThreshold = 5;
    public float minBoundary;
    public float maxBoundary;
    
    // Start is called before the first frame update
    void Start()
    {
        Vector3 randomGoal = new Vector3(
            Random.Range(minBoundary,maxBoundary),
            0,
            Random.Range(minBoundary,maxBoundary)
        );
        goal = randomGoal;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = goal - transform.position;
        if (direction.magnitude < distanceThreshold)
        {
            Vector3 randomGoal = new Vector3(
                Random.Range(minBoundary,maxBoundary),
                0,
                Random.Range(minBoundary,maxBoundary)
            );
            goal = randomGoal;
        }
    }
    
    private void OnDrawGizmos()
    {
        // Distance
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position,goal);
        // Goal
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(goal,0.5f);
        
    }
}

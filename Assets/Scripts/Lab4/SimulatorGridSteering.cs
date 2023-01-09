using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class SimulatorGridSteering : MonoBehaviour
{
    public static SimulatorGridSteering _instance = null;
    public float timestep = 0.1f;
    public List<GameObject> agents = new List<GameObject>();
    public float minBoundary = -20;
    public float maxBoundary = 20;
    public Grid grid;
    public float maxForce = 100  ;

    Vector3 truncate(Vector3 v, float max)
    {
        float size = Mathf.Min(v.magnitude, max);
        return v.normalized * size;
    }
    Vector3 seekForce(Agent a, Vector3 target)
    {
        Vector3 desired_velocity = (target-a.transform.position).normalized * a.maxSpeed;
        return desired_velocity - a.velocity;
    }
    
    Vector3 fleeForce(Agent a, Vector3 target)
    {
        Vector3 desired_velocity = (a.transform.position-target).normalized * a.maxSpeed;
        return desired_velocity - a.velocity;
    }
    
    Vector3 arriveForce(Agent a, Vector3 target)
    {
        Vector3 target_offset = target - a.transform.position;
        float distance = target_offset.magnitude;
        float ramped_speed = a.maxSpeed * (distance / a.slowingDistance);
        float clipped_speed = Mathf.Min(ramped_speed, a.maxSpeed);
        Vector3 desired_velocity = (clipped_speed / distance) * target_offset;
        return desired_velocity - a.velocity;
    }
    
    Vector3 obstacleAvoidanceForce(GameObject a, Vector3 target)
    {
        // Compute imaginary cylinder in local reference system
        float cylinderRadius = a.GetComponent<Agent>().radius;
        float cylinderDistance = a.GetComponent<Agent>().velocity.magnitude*2;
        
        // Get list of agents in current and following waypoint node
        List<GameObject> nearAgents = new List<GameObject>();
        for (int i = 0; i < 2; i++)
        {
            foreach (var agentInCell in a.GetComponent<PathManagerGrid>().waypoints[i].AgentsInCell)
            {
                nearAgents.Add(agentInCell);
            }
        }
        
        // For each near agent evaluate collision to add in collidingAgent list
        List<GameObject> collidingAgents = new List<GameObject>();
        foreach (var nearAgent in nearAgents)
        {
            // Change world to "GameObject a" local reference system
            Vector3 nearAgentLocalPos = a.transform.InverseTransformPoint(nearAgent.transform.position);
            // Discard if agent is behind of fully ahead of cylinder
            if (nearAgentLocalPos.z < 0.0f || nearAgentLocalPos.z > cylinderDistance)
            {
                continue;
            }
            // Set forward vector to zero and discard if distance of nearAgent to origin is greater than radius of cylinder + radius of sphere
            nearAgentLocalPos.z = 0.0f;
            if (nearAgentLocalPos.magnitude > cylinderRadius + nearAgent.GetComponent<Agent>().radius)
            {
                continue;
            }
            collidingAgents.Add(nearAgent);
        }
        // Find closest colliding position from collidingAgents list
        Vector3 closestCollidingLocalPosition = Vector3.zero;
        if (collidingAgents.Count > 0)
        {
            closestCollidingLocalPosition = a.transform.InverseTransformPoint(collidingAgents[0].transform.position);
        }
        foreach (var collidingAgent in collidingAgents)
        {
            if(closestCollidingLocalPosition.magnitude > a.transform.InverseTransformPoint(collidingAgent.transform.position).magnitude)
            {
                closestCollidingLocalPosition = a.transform.InverseTransformPoint(collidingAgent.transform.position);
            }
        }
        // Negate this vector
        closestCollidingLocalPosition.x *= -4;
        if (closestCollidingLocalPosition != Vector3.zero && closestCollidingLocalPosition.x < 0.001)
        {
            closestCollidingLocalPosition.x = -1;
        }

        Vector3 desired_velocity = closestCollidingLocalPosition.normalized * a.GetComponent<Agent>().maxSpeed / 4;
        return desired_velocity;
    }

    public void UpdateSimulation()
    {
        foreach (var node in grid.nodes)
        {
            node.AgentsInCell.Clear();
        }
        foreach (var agent in agents)
        {
            if (agent.GetComponent<PathManagerGrid>().waypoints != null && agent.GetComponent<PathManagerGrid>().waypoints.Count > 1)
            {
                Vector3 currWaypoint = agent.GetComponent<PathManagerGrid>().waypoints[1].Center;
                Vector3 force = seekForce(agent.GetComponent<Agent>(),currWaypoint);
                Vector3 avoidForce = obstacleAvoidanceForce(agent,currWaypoint);
                if (avoidForce.magnitude >= 0.001)
                {
                    force = avoidForce;
                }
                force = truncate(force, maxForce);
                Vector3 acceleration = force / agent.GetComponent<Rigidbody>().mass ; // update acceleration with Newton’s 2 nd law
                agent.GetComponent<Agent>().velocity += acceleration * 1 ; // update velocity
                agent.GetComponent<Agent>().velocity = truncate(agent.GetComponent<Agent>().velocity, agent.GetComponent<Agent>().maxSpeed ); // limit agent speed
            }
            else
            {
                Debug.Log("Waiting for the pathfinding algorithm");
            }
            if (agent.GetComponent<PathManagerGrid>().waypoints != null && agent.GetComponent<PathManagerGrid>().waypoints.Count > 0)
            {
                agent.GetComponent<PathManagerGrid>().waypoints[0].AgentsInCell.Add(agent);
            }
        }
    }

    public IEnumerator SimulationCoroutine()
    {
        while (true)
        {
            UpdateSimulation();
            yield return new WaitForSeconds(timestep);
        }
    }
    
    public static SimulatorGridSteering GetInstance()
    {
        if(_instance == null)
        {
            GameObject _simulatorGameObject = new GameObject("SimulatorGridSteering");
            _instance = _simulatorGameObject.AddComponent<SimulatorGridSteering>();
        }

        return _instance;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SimulatorGrid : MonoBehaviour
{
    public static SimulatorGrid _instance = null;
    public float timestep = 0.3f;
    public List<GameObject> agents = new List<GameObject>();
    public float minBoundary = -20;
    public float maxBoundary = 20;
    public Grid grid;

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
                Vector3 direction = currWaypoint - agent.transform.position;
                agent.GetComponent<Agent>().velocity = direction.normalized * agent.GetComponent<Agent>().maxSpeed;
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
    
    public static SimulatorGrid GetInstance()
    {
        if(_instance == null)
        {
            GameObject _simulatorGameObject = new GameObject("SimulatorGrid");
            _instance = _simulatorGameObject.AddComponent<SimulatorGrid>();
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
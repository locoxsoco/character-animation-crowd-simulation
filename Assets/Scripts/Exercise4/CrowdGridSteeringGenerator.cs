using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdGridSteeringGenerator : MonoBehaviour
{
    public GameObject floor;
    public GameObject agentPrefab;
    public int numberAgents = 20;
    public GameObject agentPrefabPersonalSpace;
    public int numberAgentsPersonalSpace = 10;
    public GameObject obstacle1;
    public GameObject obstacle2;
    public float minBoundary = -40;
    public float maxBoundary = 40;
    public float cellSize = 8;
    public float obstacleProbability = 0.2f;

    private SimulatorGridSteering _simulator;

    private Grid _grid;
    // Start is called before the first frame update
    void Start()
    {
        _grid = new Grid(minBoundary, maxBoundary, minBoundary, maxBoundary, cellSize, obstacleProbability);
        floor.transform.localScale *= (maxBoundary-minBoundary)/10+1;
        _simulator = SimulatorGridSteering.GetInstance();
        _simulator.grid = _grid;
        
        // Add obstacles in Grid
        for (int i = 0; i < _grid.nodes.Count; i++)
        {
            if (_grid.nodes[i].Occupied)
            {
                Vector3 position = _grid.nodes[i].Center;
                int typeObstacle = Random.Range(0, 2);
                Quaternion randomRotation = Quaternion.Euler(
                    0,
                    0,
                    0
                );
                if (typeObstacle == 0)
                {
                    GameObject obstacle = Instantiate(obstacle1,position, randomRotation);
                    obstacle.transform.localScale *= cellSize;
                }
                else
                {
                    GameObject obstacle = Instantiate(obstacle2,position, randomRotation);
                    obstacle.transform.localScale *= cellSize;
                }
            }
        }

        // Add agents in Grid
        List<int> occupiedByAgents = new List<int>();
        for (int i = 0; i < numberAgents; i++)
        {
            int randomNodeId = Random.Range(0, _grid.nodes.Count-1);
            while (_grid.nodes[randomNodeId].Occupied || occupiedByAgents.Contains(randomNodeId))
            {
                randomNodeId = Random.Range(0, _grid.nodes.Count-1);
            }
            occupiedByAgents.Add(randomNodeId);
            Vector3 randomPosition = _grid.nodes[randomNodeId].Center;
            Quaternion randomRotation = Quaternion.Euler(
                0,
                Random.Range(0, 360),
                0
            );
            GameObject newAgent = (GameObject)Instantiate(agentPrefab,randomPosition, randomRotation);
            Agent agentComponent = newAgent.GetComponent<Agent>();
            agentComponent.slowingArrival = false;
            PathManagerGrid pathManagerGrid = newAgent.GetComponent<PathManagerGrid>();
            pathManagerGrid.start = _grid.nodes[randomNodeId];
            pathManagerGrid.grid = _grid;
            _simulator.agents.Add(newAgent);
        }
        for (int i = 0; i < numberAgentsPersonalSpace; i++)
        {
            int randomNodeId = Random.Range(0, _grid.nodes.Count-1);
            while (_grid.nodes[randomNodeId].Occupied || occupiedByAgents.Contains(randomNodeId))
            {
                randomNodeId = Random.Range(0, _grid.nodes.Count-1);
            }
            occupiedByAgents.Add(randomNodeId);
            Vector3 randomPosition = _grid.nodes[randomNodeId].Center;
            Quaternion randomRotation = Quaternion.Euler(
                0,
                Random.Range(0, 360),
                0
            );
            GameObject newAgent = (GameObject)Instantiate(agentPrefabPersonalSpace,randomPosition, randomRotation);
            Agent agentComponent = newAgent.GetComponent<Agent>();
            agentComponent.slowingArrival = true;
            PathManagerGrid pathManagerGrid = newAgent.GetComponent<PathManagerGrid>();
            pathManagerGrid.start = _grid.nodes[randomNodeId];
            pathManagerGrid.grid = _grid;
            _simulator.agents.Add(newAgent);
        }
        StartCoroutine(_simulator.SimulationCoroutine());
        Destroy(agentPrefab);
        Destroy(agentPrefabPersonalSpace);
        Destroy(obstacle1);
        Destroy(obstacle2);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnDrawGizmos()
    {
        if (_grid != null)
        {
            for (int i = 0; i < _grid.nodes.Count; i++)
            {
                if (_grid.nodes[i].Occupied)
                {
                    Gizmos.color = Color.red;
                }
                else
                {
                    Gizmos.color = Color.yellow;
                }
                Gizmos.DrawWireCube(_grid.nodes[i].Center,new Vector3(cellSize,cellSize,cellSize));
            }
        }
    }
}

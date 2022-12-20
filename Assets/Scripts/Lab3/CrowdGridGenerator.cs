using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdGridGenerator : MonoBehaviour
{
    public GameObject prefab;

    private Simulator _simulator;

    private Grid _grid;
    // Start is called before the first frame update
    void Start()
    {
        float minBoundary = -20;
        float maxBoundary = 20;
        _simulator = Simulator.GetInstance();
        _grid = new Grid(minBoundary, maxBoundary, minBoundary, maxBoundary, 2);
        for (int i = 0; i < 30; i++)
        {
            int randomNodeId = Random.Range(0, _grid.nodes.Count-1);
            Vector3 randomPosition = _grid.nodes[randomNodeId].Center;
            Quaternion randomRotation = Quaternion.Euler(
                0,
                Random.Range(0, 360),
                0
            );
            GameObject newAgent = (GameObject)Instantiate(prefab,randomPosition, randomRotation);
            _simulator.agents.Add(newAgent);
            
            randomNodeId = Random.Range(0, _grid.nodes.Count-1);
            Vector3 randomGoal = _grid.nodes[randomNodeId].Center;
            newAgent.GetComponent<PathManager>().goal = randomGoal;
        }
        StartCoroutine(_simulator.SimulationCoroutine());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

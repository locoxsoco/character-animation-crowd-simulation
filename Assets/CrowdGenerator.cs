using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdGenerator : MonoBehaviour
{
    public GameObject prefab;

    private Simulator _simulator;
    // Start is called before the first frame update
    void Start()
    {
        float minBoundary = -20;
        float maxBoundary = 20;
        _simulator = Simulator.GetInstance();
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(minBoundary, maxBoundary),
                3,
                Random.Range(minBoundary, maxBoundary)
            );
            Quaternion randomRotation = Quaternion.Euler(
                0,
                Random.Range(0, 360),
                0
            );
            GameObject newAgent = (GameObject)Instantiate(prefab,randomPosition, randomRotation);
            _simulator.agents.Add(newAgent);
            
            Vector3 randomGoal = new Vector3(
                Random.Range(minBoundary, maxBoundary),
                0,
                Random.Range(minBoundary, maxBoundary)
            );
            newAgent.GetComponent<PathManager>().goal = randomGoal;
        }
        StartCoroutine(_simulator.SimulationCoroutine());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

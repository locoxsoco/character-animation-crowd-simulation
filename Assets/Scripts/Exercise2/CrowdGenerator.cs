using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdGenerator : MonoBehaviour
{
    public GameObject floor;
    public GameObject prefab;
    public int numberAgents = 30;
    public float minBoundary = -20;
    public float maxBoundary = 20;

    private Simulator _simulator;
    // Start is called before the first frame update
    void Start()
    {
        _simulator = Simulator.GetInstance();
        floor.transform.localScale *= (maxBoundary-minBoundary)/10+1;
        for (int i = 0; i < numberAgents; i++)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(minBoundary, maxBoundary),
                0,
                Random.Range(minBoundary, maxBoundary)
            );
            Quaternion randomRotation = Quaternion.Euler(
                0,
                Random.Range(0, 360),
                0
            );
            GameObject newAgent = (GameObject)Instantiate(prefab,randomPosition, randomRotation);
            newAgent.GetComponent<PathManager>().minBoundary = minBoundary;
            newAgent.GetComponent<PathManager>().maxBoundary = maxBoundary;
            _simulator.agents.Add(newAgent);
        }
        StartCoroutine(_simulator.SimulationCoroutine());
        Destroy(prefab);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

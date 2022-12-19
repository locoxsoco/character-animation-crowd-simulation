using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Simulator : MonoBehaviour
{
    public static Simulator _instance = null;
    public float timestep = 1;
    public List<GameObject> agents = new List<GameObject>();
    public float minBoundary = -20;
    public float maxBoundary = 20;

    public void UpdateSimulation()
    {
        foreach (var agent in agents)
        {
            Vector3 goalPosition = agent.GetComponent<PathManager>().goal;
            Vector3 direction = goalPosition - agent.transform.position;
            agent.GetComponent<Agent>().velocity = direction.normalized;
            agent.GetComponent<Rigidbody>().velocity = direction.normalized;
            float distance_threshold = 5;
            if (direction.magnitude < distance_threshold)
            {
                Vector3 randomGoal = new Vector3(
                    Random.Range(-20,20),
                    0,
                    Random.Range(-20, 20)
                );
                agent.GetComponent<PathManager>().goal = randomGoal;
                direction = goalPosition - transform.position;
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
    
    public static Simulator GetInstance()
    {
        if(_instance == null)
        {
            GameObject _simulatorGameObject = new GameObject("Simulator");
            _instance = _simulatorGameObject.AddComponent<Simulator>();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdGenerator : MonoBehaviour
{
    public GameObject prefab;

    public float minBoundary, maxBoundary;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 10; i++)
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
            GameObject newAgent = Instantiate(prefab,randomPosition, randomRotation);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

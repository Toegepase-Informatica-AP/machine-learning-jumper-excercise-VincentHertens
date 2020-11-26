using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject obstacle;
    public float interval = 3f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObstacle", interval, interval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnObstacle()
    {
        GameObject.Instantiate(obstacle, transform);
    }
}

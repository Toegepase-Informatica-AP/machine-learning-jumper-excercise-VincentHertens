using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    public Obstacle obstaclePrefab;
    public Player playerPrefab;

    private Player player;
    private GameObject obstacles;
    private Spawn spawn;
    private float currentTime;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0f;
    }

    public void OnEnable()
    {
        player = transform.GetComponentInChildren<Player>();

    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Environment : MonoBehaviour
{
    private const int DEFAULT_CHANCE = 75;

    public static System.Random random = new System.Random();
    public int obstacleChance = DEFAULT_CHANCE;

    public Obstacle obstaclePrefab;
    public Player playerPrefab;
    public Obstacle bonusObstaclePrefab;

    private TextMeshPro scoreBoard;
    private Player player;
    private Spawn spawnPoint;
    private GameObject obstacles;

    public float interval = 1f;
    private float currentTime = 0f;
    private float episodeObstaclespeed;

    private void FixedUpdate()
    {
        if (currentTime > interval)
        {
            currentTime = 0f;
            SpawnObstacle();
        }
        else
        {
            currentTime += Time.deltaTime;
        }
        scoreBoard.text = "Score: " + player.GetCumulativeReward().ToString("f3");
    }

    public void OnEnable()
    {
        if (obstacleChance > 100 || obstacleChance < 1)
        {
            obstacleChance = DEFAULT_CHANCE;
        }

        player = transform.GetComponentInChildren<Player>();
        obstacles = transform.Find("Obstacles").gameObject;
        spawnPoint = transform.GetComponentInChildren<Spawn>();
        scoreBoard = transform.Find("ScoreBoard").GetComponentInChildren<TextMeshPro>();
    }

    public void ClearEnvironment()
    {
        foreach (Transform obstacle in obstacles.transform)
        {
            Destroy(obstacle.gameObject);
        }

        episodeObstaclespeed = Random.Range(obstaclePrefab.minSpeed, obstaclePrefab.maxSpeed);
    }

    public void SpawnObstacle()
    {
        Obstacle newObstacle;
        if (random.Next(1, 101) <= obstacleChance)
        {
            newObstacle = Instantiate(obstaclePrefab, spawnPoint.transform);
            newObstacle.transform.SetParent(obstacles.transform);
        }
        else
        {
            newObstacle = Instantiate(bonusObstaclePrefab, spawnPoint.transform);
            newObstacle.transform.SetParent(obstacles.transform);
        }
        newObstacle.speed = episodeObstaclespeed;
    }

    public void SpawnPlayer()
    {
        Player newPlayer = Instantiate(playerPrefab);
        newPlayer.transform.SetParent(gameObject.transform);
    }
}

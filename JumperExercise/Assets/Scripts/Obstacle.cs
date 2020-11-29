using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float speed = 10f;
    public float minSpeed = 10f;
    public float maxSpeed = 15f;

    private Environment environment;
    private Player player;

    void Start()
    {
        player = transform.parent.parent.gameObject.GetComponentInChildren<Player>();
    }

    void Update()
    {
        MoveObstacle();
    }

    private void MoveObstacle()
    {
        if (environment == null)
        {
            environment = GetComponentInParent<Environment>();
        }
        Debug.Log(speed);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FinishLine"))
        {
            player.AddReward(0.15f);
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using UnityEngine;
using Unity.MLAgents.Sensors;

public class Player : Agent
{
    public float jumpStrength = 5f; 

    private bool canJump = true;

    private new Rigidbody rigidbody;
    private Environment environment;

    public override void Initialize()
    {
        base.Initialize();
        rigidbody = GetComponent<Rigidbody>();
        environment = GetComponentInParent<Environment>();
    }

    private void FixedUpdate()
    {
        if (canJump)
        {
            RequestDecision();
        }
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        if (Mathf.FloorToInt(vectorAction[0]) == 1)
        {
            JumpPlayer();
        }
    }

    public override void Heuristic(float[] actionsOut)
    {
        if (Input.GetKey(KeyCode.Space))
        {
            JumpPlayer();
        }
    }

    public override void OnEpisodeBegin()
    {
        rigidbody.angularVelocity = Vector3.zero;
        rigidbody.velocity = Vector3.zero;

        environment.ClearEnvironment();
    }

    private void JumpPlayer()
    {
        rigidbody.AddForce(new Vector3(0, jumpStrength, 0), ForceMode.VelocityChange);
        canJump = false;
        AddReward(-0.1f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Obstacle"))
        {
            Destroy(collision.gameObject);

            AddReward(-1f);
            EndEpisode();
        }
        else if (collision.transform.CompareTag("BonusObstacle"))
        {
            Destroy(collision.gameObject);

            AddReward(0.1f);
        }
        else if (collision.transform.CompareTag("Platform"))
        {
            canJump = true;
        }
    }
}

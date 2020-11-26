using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using UnityEngine;
using Unity.MLAgents.Sensors;

public class Player : Agent
{
    private bool canJump = true;
    private Rigidbody rigidbody;

    public float jumpStrength = 10f;

    public override void Initialize()
    {
        rigidbody = GetComponent<Rigidbody>();
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

    }

    private void JumpPlayer()
    {
        rigidbody.AddForce(new Vector3(0, jumpStrength, 0), ForceMode.VelocityChange);
        canJump = false;
        AddReward(-0.25f);
    }

    private void OnCollisionEnter(Collision collidedObj)
    {
        if (collidedObj.gameObject.CompareTag("Obstacle"))
        {
            AddReward(-1f);
        }
    }
}

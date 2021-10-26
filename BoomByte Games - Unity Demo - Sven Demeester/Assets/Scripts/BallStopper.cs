using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallStopper : MonoBehaviour
{
 //   [HideInInspector]
    public bool IsStopped;

    void Update()
    {
        Rigidbody RB = GetComponent<Rigidbody>();
        if (RB.velocity.magnitude <= 0.1f)
        {
            RB.velocity = Vector3.zero;
            IsStopped = true;
        }
        else if(RB.velocity.magnitude > 1f)
        {
            IsStopped = false;
            GameManager.Instance.IsBallRolling = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrigger : BounceObstacle
{
    private void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        //Player TakeDamage
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceObstacle : BaseObstacle
{
    [SerializeField] private float _bounceForce = 100f;

    protected void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Vector3 collisionDirection = collision.contacts[0].point - collision.gameObject.transform.position;
            Vector3 reflectDirection = Vector3.Reflect(collisionDirection, collision.contacts[0].normal).normalized;
            if (reflectDirection.y < 0)
                reflectDirection = new Vector3(reflectDirection.x, -reflectDirection.y, reflectDirection.z);
            PlayerRagdollController ragdoll = collision.gameObject.GetComponent<PlayerRagdollController>();
            ragdoll?.AddForceToPevis(reflectDirection * _bounceForce);

        }
    }
}

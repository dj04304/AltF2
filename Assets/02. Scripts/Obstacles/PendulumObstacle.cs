using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendulumObstacle : BounceObstacle
{
    [SerializeField] private float angle = 60;
    [SerializeField] private float speed = 2f;
    
    private float moveTime = 0;

    private void Update()
    {
        moveTime += Time.deltaTime * speed;

        Quaternion currentRotation = transform.rotation;
        float targetZRotation = Mathf.Sin(moveTime) * angle;
        Quaternion targetRotation = Quaternion.Euler(currentRotation.eulerAngles.x, currentRotation.eulerAngles.y, targetZRotation);

        transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, 0.5f);
    }
}

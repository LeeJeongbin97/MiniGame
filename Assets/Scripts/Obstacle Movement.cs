using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    [SerializeField] private float obstacleSpeed = 3f;

    void Update()
    {
        transform.Translate(Vector3.forward * obstacleSpeed * Time.deltaTime);
    }

    public void SetObstacleSpeed(float newSpeed)
    {
        obstacleSpeed = newSpeed;
    }

    public float GetObstacleSpeed()
    {
        return obstacleSpeed;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (transform.position.z > 50f || transform.position.z < -50f)
        {
            Destroy(gameObject);
        }
    }
}
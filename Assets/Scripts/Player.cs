using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float movingSize = 1f;
    [SerializeField] private float minX = -10f;
    [SerializeField] private float maxX = 10f;
    private Vector3 moveDirection = Vector3.zero;
    private bool Moving = true;
    private bool isOnLog = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    void Update()
    {
        if (Moving)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                StartCoroutine(MovePlayer(Vector3.forward * movingSize));
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                StartCoroutine(MovePlayer(Vector3.back * movingSize));
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                StartCoroutine(MovePlayer(Vector3.left * movingSize));
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                StartCoroutine(MovePlayer(Vector3.right * movingSize));
            }
        }
    }

    private IEnumerator MovePlayer(Vector3 moveDirection)
    {
        Moving = false;

        Vector3 newPosition = rb.position + moveDirection;
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);

        if (Physics.Raycast(rb.position, moveDirection, out RaycastHit hit, movingSize))
        {
            if (hit.collider.CompareTag("Tree"))
            {
                yield return new WaitForSeconds(0.1f);
                Moving = true;
                yield break;
            }
        }

        if (moveDirection != Vector3.zero)
        {
            Quaternion newRotation = Quaternion.LookRotation(moveDirection);
            rb.MoveRotation(newRotation);
        }

        rb.MovePosition(newPosition);
        yield return new WaitForSeconds(0.1f);
        Moving = true;
    }

    public bool IsOnLog()
    {
        return isOnLog;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Log"))
        {
            isOnLog = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Log"))
        {
            isOnLog = false;
        }
    }
}
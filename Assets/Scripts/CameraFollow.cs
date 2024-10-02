using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float fixedY = 10f;
    [SerializeField] private Vector3 offset;

    void LateUpdate()
    {
        Vector3 newPosition = new Vector3(player.position.x, fixedY, player.position.z) + offset;
        transform.position = newPosition;
    }
}
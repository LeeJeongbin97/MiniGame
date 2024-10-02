using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Water : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 0.5f;
    private Renderer riverRenderer;

    void Start()
    {
        riverRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        float offset = Time.time * scrollSpeed;
        riverRenderer.material.mainTextureOffset = new Vector2(0, offset);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!other.GetComponent<Player>().IsOnLog())
            {
                GameOver();
            }
        }
    }

    private void GameOver()
    {
        Debug.Log("게임 오버");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
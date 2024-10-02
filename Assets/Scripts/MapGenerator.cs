using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] tilePrefabs;
    [SerializeField] private GameObject treePrefab;
    [SerializeField] private GameObject lilyPrefab;
    [SerializeField] private GameObject logPrefab;
    [SerializeField] private GameObject centerLinePrefab;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform startPoint;
    [SerializeField] private float spawnDistance = 20f;
    [SerializeField] private float spawnRange = 70f;
    [SerializeField] private int tilesToSpawn = 1;
    [SerializeField] private float boundaryXOffset = 2.5f;
    [SerializeField] private Vector2 tileSize = new Vector2(50, 50);

    private float lastSpawnedZ;
    private int lastTileIndex = -1;
    private int roadCount = 0;
    private int riverCount = 0;

    void Start()
    {
        lastSpawnedZ = startPoint.position.z + 1;
    }

    void Update()
    {
        float playerZ = playerTransform.position.z;

        if (playerZ + spawnRange > lastSpawnedZ)
        {
            for (int i = 0; i < tilesToSpawn; i++)
            {
                SpawnTile();
            }
        }
    }

    private void SpawnTile()
    {
        int randomIndex = GetRandomTileIndex();

        if (randomIndex == 2 && riverCount < 2)
        {
            riverCount++;
        }
        else
        {
            riverCount = 0;
        }

        lastSpawnedZ += 1;
        Vector3 spawnPosition = new Vector3(0, 0, lastSpawnedZ);

        if (randomIndex == 2)
        {
            spawnPosition.y -= 0.2f;
        }

        GameObject newTile = Instantiate(tilePrefabs[randomIndex], spawnPosition, Quaternion.identity);

        if (randomIndex == 0)
        {
            SpawnTreesOnFloor(newTile);
            roadCount = 0;
        }
        else if (randomIndex == 2)
        {
            SpawnLilyAndLogsOnRiver(newTile);
            roadCount = 0;
        }
        if (randomIndex == 1)
        {
            roadCount++;
            if (roadCount >= 2 && lastTileIndex == 1)
            {
                SpawnRoadLines(spawnPosition.z);
            }
        }

        lastTileIndex = randomIndex;
    }

    private void SpawnTreesOnFloor(GameObject floorTile)
    {
        int treeCount = Random.Range(1, 5);
        float centerZ = floorTile.transform.position.z;

        for (int i = 0; i < treeCount; i++)
        {
            float randomXOffset = Random.Range(-tileSize.x / 2, tileSize.x / 2);
            Vector3 treePosition = new Vector3(randomXOffset, 0, centerZ);
            Instantiate(treePrefab, treePosition, Quaternion.identity);
        }
    }

    private void SpawnLilyAndLogsOnRiver(GameObject riverTile)
    {
        int lilyCount = Random.Range(3, 6);
        float centerZ = riverTile.transform.position.z;

        for (int i = 0; i < lilyCount; i++)
        {
            float stepX = Random.Range(-boundaryXOffset / 2, boundaryXOffset / 2);
            float lilyXPosition = Mathf.Round(stepX / 2) * 2;
            Vector3 lilyPosition = new Vector3(lilyXPosition, 0.31f, centerZ);
            Instantiate(lilyPrefab, lilyPosition, Quaternion.identity);
        }

        int logCount = Random.Range(3, 6);
        for (int i = 0; i < logCount; i++)
        {
            float stepX = Random.Range(-boundaryXOffset / 2, boundaryXOffset / 2);
            float logXPosition = Mathf.Round(stepX / 2) * 2;
            Vector3 logPosition = new Vector3(logXPosition, 0.31f, centerZ);
            Instantiate(logPrefab, logPosition, Quaternion.identity);
        }
    }

    private void SpawnRoadLines(float lastRoadZ)
    {
        float centerZ = lastRoadZ - 0.5f;

        for (float x = -boundaryXOffset; x <= boundaryXOffset; x += 2f)
        {
            Vector3 roadLinePosition = new Vector3(x, 0.1f, centerZ);
            Instantiate(centerLinePrefab, roadLinePosition, Quaternion.identity);
        }
    }

    private int GetRandomTileIndex()
    {
        int randomValue = Random.Range(0, 100);

        if (riverCount > 0)
        {
            return 2;
        }

        if (randomValue < 20)
        {
            return 0;
        }
        else if (randomValue < 80)
        {
            return 1;
        }
        else
        {
            return 2;
        }
    }
}
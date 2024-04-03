using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlatformSpawner : MonoBehaviour
{
    [Header("Platform")]
    [SerializeField] private GameObject platformPrefab;
    [SerializeField] private float spawnTime = 2f;
    [SerializeField] private float platformLength = 24f;
    
    [Header("Traps")]
    [SerializeField] private GameObject spikeTrapPrefab;

    private void Start()
    {
        StartCoroutine(SpawnPlatform());
    }
    
    private IEnumerator SpawnPlatform()
    {
        while (true)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + platformLength);
            
            var newPlatform = Instantiate(platformPrefab, transform.position, Quaternion.identity);
            SetTraps(newPlatform);
            
            yield return new WaitForSeconds(spawnTime);
        }
    }

    private void SetTraps(GameObject platform)
    {
        List<Transform> rows = new List<Transform>();
        foreach (Transform child in platform.transform)
        {
            rows.Add(child);
        }

        for (int i = 0; i < rows.Count; i++)
        {
            if (i%4 == 0)
            {
                var tiles = rows[i].GetComponentsInChildren<Transform>();
                var randomTileIndex = Random.Range(0, tiles.Length);
                var spikeTrap = Instantiate(spikeTrapPrefab, new Vector3(tiles[randomTileIndex].position.x, tiles[randomTileIndex].position.y + 0.3f,
                    tiles[randomTileIndex].position.z), Quaternion.identity);
                
                spikeTrap.transform.SetParent(tiles[randomTileIndex]);
            }
        }
    }
}
    
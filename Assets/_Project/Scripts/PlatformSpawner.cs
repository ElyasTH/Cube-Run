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
    [SerializeField] private int spikeSpawnRate = 3;

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
        if (spikeSpawnRate > rows.Count)
        {
            spikeSpawnRate = rows.Count;
        }
        
        for (int spikeCount = 0; spikeCount < spikeSpawnRate;)
        {
            var randomRow = rows[Random.Range(0, rows.Count)];
            
            bool isRowEmpty = true;
            foreach (Transform tile in randomRow)
            {
                if (tile.childCount > 0)
                {
                    isRowEmpty = false;
                    break;
                }
            }
            if (!isRowEmpty) continue;
            
            var randomTile = randomRow.GetChild(Random.Range(0, randomRow.childCount));
            
            var newSpike = Instantiate(spikeTrapPrefab, new Vector3(randomTile.position.x,
                randomTile.position.y + randomTile.localScale.y/2f, randomTile.position.z), Quaternion.identity);
            newSpike.transform.SetParent(randomTile);
            
            spikeCount++;
        }
    }
}
    
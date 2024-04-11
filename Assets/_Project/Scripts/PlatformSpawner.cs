using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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
    [SerializeField] private float spikeHeight = 0.5f;
    
    [Space]
    [SerializeField] private GameObject slideTrapPrefab;
    [SerializeField] private int slideSpawnRate = 3;
    [SerializeField] private float slideHeight = 0.5f;

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
            InitPlatform(newPlatform);
            
            yield return new WaitForSeconds(spawnTime);
        }
    }
    
    private enum TrapType
    {
        Spike,
        Slide
    }

    private void InitPlatform(GameObject platform)
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
        if (slideSpawnRate > rows.Count)
        {
            slideSpawnRate = rows.Count;
        }
        
        SpawnTraps(TrapType.Spike, rows);
        SpawnTraps(TrapType.Slide, rows);
    }
    
    private void SpawnTraps(TrapType trapType, List<Transform> rows)
    {
        int trapCount = 0;
        GameObject trapPrefab = null;
        float trapHeight = 0f;
        switch (trapType)
        {
            case TrapType.Spike:
                trapCount = spikeSpawnRate;
                trapPrefab = spikeTrapPrefab;
                trapHeight = spikeHeight;
                break;
            case TrapType.Slide:
                trapCount = slideSpawnRate;
                trapPrefab = slideTrapPrefab;
                trapHeight = slideHeight;
                break;
        }
        
        for (int i = 0; i < trapCount;)
        {
            var randomRow = rows[Random.Range(0, rows.Count)];
            bool isOccupied = false;
            foreach (Transform tile in randomRow)
            {
                if (tile.childCount > 0)
                {
                    isOccupied = true;
                    break;
                }
            }
            if (isOccupied)
                continue;
            
            var randomTile = randomRow.GetChild(Random.Range(0, randomRow.childCount));
            var newTrap = Instantiate(trapPrefab, new Vector3(randomTile.position.x,
                randomTile.position.y + trapHeight, randomTile.position.z), Quaternion.identity);
            newTrap.transform.SetParent(randomTile);
            
            i++;
        }
    }
}


    
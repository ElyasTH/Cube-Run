using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] private GameObject platform;
    [SerializeField] private float spawnTime = 1.5f;
    private float elapsedTime = 0f;
    [SerializeField] private float platformLength = 24f;
    [SerializeField] private GameObject spikeTrap;
    [SerializeField] private float spikeTrapHeight = 0.32f;
    [SerializeField] private int spikeSpawnRate = 4;
    
    private void Update()
    {
        elapsedTime += Time.deltaTime;
        
        if (elapsedTime >= spawnTime)
        {
            var newPlatform = Instantiate(platform, new Vector3(transform.position.x, transform.position.y, transform.position.z + platformLength),
                Quaternion.identity);
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + platformLength);
            
            SetupTraps(newPlatform.transform);
            
            elapsedTime = 0;
        }
    }

    private void SetupTraps(Transform newPlatform)
    {
        List<Transform> occupiedRows = new List<Transform>();
        
        for (int i = 0; i < spikeSpawnRate; i++)
        {
            var randomRow = newPlatform.GetChild(Random.Range(0, newPlatform.childCount));
            while (occupiedRows.Contains(randomRow))
            {
                randomRow = newPlatform.GetChild(Random.Range(0, newPlatform.childCount));
            }
            occupiedRows.Add(randomRow);
            
            var randomTile = randomRow.GetChild(Random.Range(0, randomRow.childCount));

            Instantiate(spikeTrap, new Vector3(randomTile.position.x, randomTile.position.y + spikeTrapHeight,
                randomTile.position.z), Quaternion.identity);
        }
    }
}

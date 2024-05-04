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

        for (int i = 0; i < newPlatform.childCount; i++)
        {
            if (i % 4 == 0)
            {
                var randomTile = newPlatform.GetChild(i).GetChild(Random.Range(0, 3));
                Instantiate(spikeTrap,
                    new Vector3(randomTile.position.x, randomTile.position.y + spikeTrapHeight, randomTile.position.z),
                    Quaternion.identity);
            }
        }
        
    }
}

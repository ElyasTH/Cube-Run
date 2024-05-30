using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlatformSpawner : MonoBehaviour
{
    [Header("Platform Settings")]
    [SerializeField] private GameObject platform;
    [SerializeField] private Collider floorCollider;
    [SerializeField] private float platformLength = 24f;
    [SerializeField] private float spawnTime;
    private float elapsedTime;
    
    [Header("Spike Trap Settings")]
    [SerializeField] private GameObject spikeTrap;
    [SerializeField] private int spawnMultiplier;
    [SerializeField] private float spikeTrapHeight;
    
    // private void Update()
    // {
    //     elapsedTime += Time.deltaTime;
    //     if (elapsedTime > spawnTime)
    //     {
    //         GameObject newPlatform = Instantiate(platform, transform.position, Quaternion.identity);
    //         floorCollider.transform.position = new Vector3(newPlatform.transform.position.x,
    //             newPlatform.transform.position.y, newPlatform.transform.position.z + floorColliderZOffset);
    //         
    //         transform.position = new Vector3(transform.position.x, transform.position.y,
    //             transform.position.z + platformLength);
    //         
    //         
    //         SetupTraps(newPlatform.transform);
    //         
    //         elapsedTime = 0;
    //     }
    // }

    private void SetupTraps(Transform newPlatform)
    {
        for (int i = 0; i < newPlatform.childCount; i++)
        {
            if (i % spawnMultiplier == 0)
            {
                Transform row = newPlatform.GetChild(i);
                int randomIndex = Random.Range(0, row.childCount);
                Transform randomTile = row.GetChild(randomIndex);
                Instantiate(spikeTrap, new Vector3(randomTile.position.x,
                    randomTile.position.y + spikeTrapHeight, randomTile.position.z), Quaternion.identity);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y,
                transform.position.z + platformLength);
            
            GameObject newPlatform = Instantiate(platform, transform.position, Quaternion.identity);
            floorCollider.transform.position = newPlatform.transform.position;
            
            SetupTraps(newPlatform.transform);
        }
    }
}

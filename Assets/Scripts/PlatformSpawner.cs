using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] private GameObject platform;
    [SerializeField] private float spawnTime = 1.5f;
    private float elapsedTime = 0f;
    [SerializeField] private float platformLength = 24f;
    private void Update()
    {
        elapsedTime += Time.deltaTime;
        
        if (elapsedTime >= spawnTime)
        {
            Instantiate(platform, new Vector3(transform.position.x, transform.position.y, transform.position.z + platformLength),
                Quaternion.identity);
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + platformLength);
            
            elapsedTime = 0;
        }
    }
}

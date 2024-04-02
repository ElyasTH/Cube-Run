using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] private GameObject platformPrefab;
    [SerializeField] private float spawnRate = 2f;
    [SerializeField] private float platformLength = 24f;

    private void Start()
    {
        StartCoroutine(SpawnPlatform());
    }
    
    private IEnumerator SpawnPlatform()
    {
        while (true)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + platformLength);
            Instantiate(platformPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(spawnRate);
        }
    }
}

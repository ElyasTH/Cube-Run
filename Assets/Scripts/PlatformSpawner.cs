using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] private GameObject platform;
    [SerializeField] private float elapsedTime = 0f;
    private void Update()
    {
        elapsedTime += Time.deltaTime;
        
        if (elapsedTime >= 2f)
        {
            Instantiate(platform, )
            
            elapsedTime = 0;
        }
    }
}

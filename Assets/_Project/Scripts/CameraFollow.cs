using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float zOffset = -10f;

    private void Update()
    {
        if (!target) return;
        transform.position = new Vector3(transform.position.x, transform.position.y, target.position.z + zOffset);
    }
}

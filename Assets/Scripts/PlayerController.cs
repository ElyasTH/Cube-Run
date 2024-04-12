using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    
    [SerializeField] private float speed = 5f;
    [SerializeField] private List<Transform> wayPoints;
    private int _currentWayPointIndex = 1;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (_currentWayPointIndex >= wayPoints.Count - 1) return;
            _currentWayPointIndex++;

            transform.position = new Vector3(wayPoints[_currentWayPointIndex].position.x, transform.position.y,
                transform.position.z);


        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (_currentWayPointIndex <= 0) return;
            _currentWayPointIndex--;
            
            transform.position = new Vector3(wayPoints[_currentWayPointIndex].position.x, transform.position.y,
                transform.position.z);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speed * Time.fixedDeltaTime * 100f);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rigidbody;
    
    [SerializeField] private float speed = 5f;

    public float Speed => speed;
    
    [SerializeField] private List<Transform> wayPoints;
    private int _currentWayPointIndex = 1;
    [SerializeField] private float jumpForce = 500f;

    [SerializeField] private bool allowJump = true;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
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
        else if (Input.GetKeyDown(KeyCode.Space) && allowJump)
        {
            rigidbody.AddForce(Vector3.up * jumpForce);
            allowJump = false;
        }
    }

    private void FixedUpdate()
    {
        rigidbody.velocity = new Vector3(rigidbody.velocity.x, rigidbody.velocity.y, speed * Time.fixedDeltaTime * 100f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Camera.main.transform.SetParent(null);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Floor"))
        {
            allowJump = true;
        }
    }
}

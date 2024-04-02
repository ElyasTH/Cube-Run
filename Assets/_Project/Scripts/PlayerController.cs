using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Vector3 _direction;
    
    [Header("Movement")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private List<Transform> wayPoints;
    private int _currentWayPointIndex = 1;
    
    [Header("Jump")]
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private bool _isGrounded = true;
    
    [Header("Slide")]
    [SerializeField] private float slideScale = 0.25f;
    [SerializeField] private float slideHeight = 0.375f;
    private float _originalHeight;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _originalHeight = transform.localScale.y;
    }

    private void Update()
    {
        if (!_isGrounded) return;
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
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            _isGrounded = false;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            transform.localScale = new Vector3(transform.localScale.x, slideHeight, transform.localScale.z);
            transform.position = new Vector3(transform.position.x, transform.position.y - slideScale, transform.position.z);
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            transform.localScale = new Vector3(transform.localScale.x, _originalHeight, transform.localScale.z);
            transform.position = new Vector3(transform.position.x, transform.position.y + slideScale, transform.position.z);
        }
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector3(0, _rigidbody.velocity.y, speed * Time.fixedDeltaTime * 100f);
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(TagManager.Floor))
        {
            _isGrounded = true;
        }
    }
}

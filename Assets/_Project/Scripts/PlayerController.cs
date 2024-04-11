using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Animator _animator;
    
    [Header("Movement")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float changeLineSpeed = 3f;
    [SerializeField] private List<Transform> wayPoints;
    private int _currentWayPointIndex = 1;
    private bool _isChangingLine = false;
    
    [Header("Jump")]
    [SerializeField] private float jumpForce = 5f;
    private bool _isGrounded = true;
    
    [Header("Slide")]
    [SerializeField] private float slideScale = 0.25f;
    [SerializeField] private float slideHeight = 0.375f;
    private float _originalScale;
    private bool _isSliding = false;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _originalScale = transform.localScale.y;
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!_isGrounded || _isChangingLine) return;
        
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (_currentWayPointIndex >= wayPoints.Count - 1 || _isSliding) return;
            _currentWayPointIndex++;

            _isChangingLine = true;
            
            StartCoroutine(Move(Vector3.right));

            // transform.position = new Vector3(wayPoints[_currentWayPointIndex].position.x, transform.position.y,
            //     transform.position.z);


        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (_currentWayPointIndex <= 0 || _isSliding) return;
            _currentWayPointIndex--;
            
            _isChangingLine = true;
            
            StartCoroutine(Move(Vector3.left));
            
            // transform.position = new Vector3(wayPoints[_currentWayPointIndex].position.x, transform.position.y,
            //     transform.position.z);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_isSliding) return;
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            _isGrounded = false;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            transform.localScale = new Vector3(transform.localScale.x, slideScale, transform.localScale.z);
            transform.position = new Vector3(transform.position.x, transform.position.y - slideHeight, transform.position.z);
            _isSliding = true;
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            if (!_isSliding) return;
            transform.localScale = new Vector3(transform.localScale.x, _originalScale, transform.localScale.z);
            transform.position = new Vector3(transform.position.x, transform.position.y + slideHeight, transform.position.z);
            _isSliding = false;
        }
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector3(0, _rigidbody.velocity.y, speed * Time.fixedDeltaTime * 100f);
    }
    
    private void OnAnimatorMove()
    {
        _rigidbody.MovePosition(_rigidbody.position + _animator.deltaPosition);
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(TagManager.Floor))
        {
            _isGrounded = true;
        }
        else if (other.gameObject.CompareTag(TagManager.Obstacle))
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator Move(Vector3 direction)
    {
        if (direction == Vector3.right)
        {
            while (transform.position.x < wayPoints[_currentWayPointIndex].position.x)
            {
                transform.position = new Vector3(transform.position.x + changeLineSpeed * Time.deltaTime, transform.position.y,
                    transform.position.z);
                yield return null;
            }
        }
        else
        {
            while (transform.position.x > wayPoints[_currentWayPointIndex].position.x)
            {
                transform.position = new Vector3(transform.position.x - changeLineSpeed * Time.deltaTime, transform.position.y,
                    transform.position.z);
                yield return null;
            }
        }
        
        transform.position = new Vector3(wayPoints[_currentWayPointIndex].position.x, transform.position.y,
            transform.position.z);
        _isChangingLine = false;
    }
}

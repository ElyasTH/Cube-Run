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

    [Header("Death")]
    [SerializeField] private ParticleSystem deathParticle;

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

            SoundManager.instance.PLAY_CHANGE_LINE_SOUND();
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (_currentWayPointIndex <= 0 || _isSliding) return;
            _currentWayPointIndex--;
            
            _isChangingLine = true;
            
            StartCoroutine(Move(Vector3.left));
            
            // transform.position = new Vector3(wayPoints[_currentWayPointIndex].position.x, transform.position.y,
            //     transform.position.z);
            
            SoundManager.instance.PLAY_CHANGE_LINE_SOUND();
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_isSliding) return;
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            _animator.SetTrigger(TagManager.Jump);
            _isGrounded = false;
            
            SoundManager.instance.PLAY_JUMP_SOUND();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            // transform.localScale = new Vector3(transform.localScale.x, slideScale, transform.localScale.z);
            // transform.position = new Vector3(transform.position.x, transform.position.y - slideHeight, transform.position.z);
            
            _animator.SetBool(TagManager.Sliding, true);
            _isSliding = true;
            
            SoundManager.instance.PLAY_SLIDE_SOUND();
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            if (!_isSliding) return;
            // transform.localScale = new Vector3(transform.localScale.x, _originalScale, transform.localScale.z);
            // transform.position = new Vector3(transform.position.x, transform.position.y + slideHeight, transform.position.z);
            
            _animator.SetBool(TagManager.Sliding, false);
            _isSliding = false;
            
            SoundManager.instance.PLAY_SLIDE_SOUND();
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
        if (!_isGrounded && other.gameObject.CompareTag(TagManager.Floor))
        {
            _isGrounded = true;
        }
        else if (other.gameObject.CompareTag(TagManager.Obstacle))
        {
            SoundManager.instance.PLAY_DEATH_SOUND();
            deathParticle.gameObject.transform.SetParent(null);
            deathParticle.Play();
            Destroy(gameObject);
            
            GameManager.instance.FinishGame();
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

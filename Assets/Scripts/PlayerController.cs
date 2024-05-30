using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] private GameManager gameManager;
    [SerializeField] private float speed = 5f;
    [SerializeField] private List<Transform> wayPoints;
    [SerializeField] private ParticleSystem deathParticle;
    [SerializeField] private SoundManager soundManager;
    private int _currentWayPointIndex = 1;
    
    [Header("Jump Settings")]
    [SerializeField] private float jumpForce = 100f;
    private bool isJumping;

    [Header("Slide Settings")]
    [SerializeField] private float slideHeight = 0.375f;
    [SerializeField] private float slideScale = 0.25f;

    private float defaultHeight;
    private float defaultScale;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        defaultHeight = transform.position.y;
        defaultScale = transform.localScale.y;
    }

    private void Update()
    {
        if (isJumping) return;
        
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (_currentWayPointIndex >= wayPoints.Count - 1) return;
            _currentWayPointIndex++;

            transform.position = new Vector3(wayPoints[_currentWayPointIndex].position.x, transform.position.y,
                transform.position.z);

            soundManager.PLAY_CHANGE_LINE_SOUND();
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (_currentWayPointIndex <= 0) return;
            _currentWayPointIndex--;
            
            transform.position = new Vector3(wayPoints[_currentWayPointIndex].position.x, transform.position.y,
                transform.position.z);

            soundManager.PLAY_CHANGE_LINE_SOUND();
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            rb.AddForce(Vector3.up * jumpForce);
            soundManager.PLAY_JUMP_SOUND();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            transform.position = new Vector3(transform.position.x, slideHeight, transform.position.z);
            transform.localScale = new Vector3(transform.localScale.x, slideScale, transform.localScale.z);
            soundManager.PLAY_SLIDE_SOUND();
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            transform.position = new Vector3(transform.position.x, defaultHeight, transform.position.z);
            transform.localScale = new Vector3(transform.localScale.x, defaultScale, transform.localScale.z);
            soundManager.PLAY_SLIDE_SOUND();
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speed * Time.fixedDeltaTime * 100f);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            deathParticle.transform.SetParent(null);
            deathParticle.Play();
            Destroy(gameObject);
            gameManager.FinishGame();
        }

        if (other.gameObject.CompareTag("Floor"))
        {
            isJumping = false;
        }
    }
}

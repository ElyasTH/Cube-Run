
    using System;
    using UnityEngine;

    public class GroundMover: MonoBehaviour
    {
        [SerializeField] private Rigidbody rigidbody;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private float speed = 5f;


        private void FixedUpdate() => rigidbody.velocity = new Vector3(rigidbody.velocity.x, rigidbody.velocity.y, speed * Time.fixedDeltaTime * 100f);
    }
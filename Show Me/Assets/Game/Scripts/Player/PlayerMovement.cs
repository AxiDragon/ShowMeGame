using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gunbloem
{
    public class PlayerMovement : MonoBehaviour
    {
        public float speed = 5f;
        private float startSpeed;
        private float maxSpeed = 5f;
        [SerializeField] float decelerationRate = 5f;
        [SerializeField] float rotationSpeed = 5f;
        [SerializeField] Transform pivot;
        Rigidbody rb;

        Vector3 moveVector;
        Vector2 input;

        void Awake()
        {
            rb = GetComponent<Rigidbody>();
            startSpeed = speed;
        }

        private void FixedUpdate()
        {
            UpdateMovement();
        }

        private void UpdateMovement()
        {
            moveVector = Quaternion.AngleAxis(transform.rotation.eulerAngles.y, Vector3.up) * new Vector3(input.x, 0f, input.y);

            //if (!Mathf.Approximately(moveVector.magnitude, 0f))
                UpdateRotation();

            if (Mathf.Approximately(moveVector.magnitude, 0f))
            {
                Decelerate();
            }
            else
            {
                Move();
            }

            ClampVelocity();
        }

        private void Move()
        {
            Vector3 moveForce = moveVector * speed;
            rb.AddForce(moveForce, ForceMode.Impulse);
        }

        private void Decelerate()
        {
            Vector3 deceleration = new Vector3(rb.velocity.x, 0f, rb.velocity.z)
                * -decelerationRate;
            rb.AddForce(deceleration);
        }

        private void UpdateRotation()
        {
            Quaternion towards = Quaternion.Euler(transform.eulerAngles.x, pivot.eulerAngles.y, transform.eulerAngles.z);
            Quaternion rot = Quaternion.RotateTowards(transform.rotation, towards, rotationSpeed * Time.deltaTime);
            transform.rotation = rot;
        }

        private void ClampVelocity()
        {
            Vector3 clampedVelocity = Vector3.Scale(rb.velocity, new Vector3(1f, 0f, 1f));
            clampedVelocity = Vector3.ClampMagnitude(clampedVelocity, maxSpeed / 3f);
            rb.velocity = new Vector3(clampedVelocity.x, Mathf.Min(rb.velocity.y, 0f), clampedVelocity.z);
        }

        public void Move(InputAction.CallbackContext callback)
        {
            input = callback.action.ReadValue<Vector2>().normalized;
        }

        public void UpdateSpeed(float statValue)
        {
            speed = startSpeed * (statValue / 10f);
            maxSpeed = speed;
        }
    }
}
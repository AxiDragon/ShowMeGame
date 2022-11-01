using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gunbloem
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] float acceleration = 5f;
        [SerializeField] float maxSpeed = 5f;
        [SerializeField] float decelerationRate = 5f;
        Rigidbody rb;

        Vector3 moveVector;
        Vector2 input;

        void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            UpdateMovement();
        }

        private void UpdateMovement()
        {
            moveVector = Quaternion.AngleAxis(transform.eulerAngles.y, Vector3.up) * new Vector3(input.x, 0f, input.y);

            if (Mathf.Approximately(moveVector.magnitude, 0f))
            {
                Vector3 deceleration = new Vector3(rb.velocity.x, 0f, rb.velocity.z)
                    * -decelerationRate;
                rb.AddForce(deceleration);
            }
            else
            {
                Vector3 moveForce = moveVector * acceleration;
                rb.AddForce(moveForce, ForceMode.Impulse);
            }

            ClampVelocity();
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
    }
}
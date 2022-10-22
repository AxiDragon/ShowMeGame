using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BicycleSurfer
{
    public class BicycleMovement : MonoBehaviour
    {
        Rigidbody rb;
        Vector2 input = Vector2.zero;
        Vector3 groundOffset;
        [SerializeField] float speed = 5f;
        [SerializeField] float rotationSpeed = 5f;
        [SerializeField] float groundCheckRadius = .2f;
        [SerializeField] float gravity = 2f;
        //[SerializeField] float maxSpeed = 15f;
        [SerializeField] Transform ground;
        [SerializeField] LayerMask groundMask;
        private float airTime = 0f;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            //groundOffset = transform.position - ground.position;
            //ground.parent = null;
        }

        void Update()
        {
            input.x = Input.GetAxis("Horizontal");
            input.y = Input.GetAxis("Vertical");
            if (CheckGrounded())
            {
                airTime -= Time.deltaTime * 2f;
            }
            else
            {
                airTime += Time.deltaTime;
            }
            airTime = Mathf.Clamp(airTime, 0f, 6f);
        }

        private void LateUpdate()
        {
            //ground.position = transform.position + groundOffset;

            if (CheckGrounded())
            {
                Ray ray = new Ray(ground.position, Vector3.down);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, groundCheckRadius, groundMask))
                {
                    transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
                }
            }
            else
            {
                Ray ray = new Ray(transform.position, Vector3.down);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, .6f, groundMask))
                {
                    transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
                }
            }
        }

        private bool CheckGrounded()
        {
            return Physics.CheckSphere(ground.position, groundCheckRadius, groundMask);
        }

        private void FixedUpdate()
        {
            Vector3 dir = transform.forward;
            
            if (!CheckGrounded())
            {
                dir = new Vector3(dir.x, 0f, dir.z).normalized;
            }

            float force = input.y * speed * Time.deltaTime;
            
            if (CheckGrounded() && airTime > 0f)
            {
                force *= (airTime + 2f);
            }
            
            transform.rotation = Quaternion.Euler(transform.eulerAngles + Vector3.up * input.x * rotationSpeed * Time.deltaTime);
            rb.AddForce(dir * force, ForceMode.Impulse);
            rb.AddForce(Vector3.down * (gravity - 1f) * 9.81f);
        }

        private void OnGUI()
        {
            GUI.skin.label.fontSize = 50;
            GUI.Label(new Rect(50, 50, 500, 500), "Airtime " + airTime.ToString("F2"));
            GUI.Label(new Rect(50, 150, 500, 500), "Speed " + rb.velocity.magnitude.ToString("F2"));
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(ground.position, groundCheckRadius);
            Gizmos.DrawWireSphere(transform.position, .6f);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gunbloem
{
    public class Gun : MonoBehaviour
    {
        [Header("Stats")]
        public float power = 1;
        public float impact = 1;
        public float fireRate = 5f;
        public float speed = -0.1f;
        
        public Bullet bullet;
        private LayerMask playerMask;

        public Transform shootTransform;
        private CameraMovement camMove;
        [HideInInspector] private Transform cam;

        public ParticleSystem shootEffect;

        private float shootingSpeed = 150f;

        float gunTimer;

        private void Start()
        {
            cam = Camera.main.transform;
            camMove = GetComponentInParent<CameraMovement>();
            playerMask = LayerMask.GetMask("Player");
            playerMask = ~playerMask;
        }

        void Update()
        {
            gunTimer += Time.deltaTime;
        }

        public void Shoot()
        {
            if (gunTimer < (1f / fireRate))
                return;

            gunTimer = 0f;

            Vector3 shootTarget = GetShootTarget();
            Bullet bulletInstance = Instantiate(bullet, shootTransform.position + transform.forward, Quaternion.identity);
            bulletInstance.gun = this;

            Rigidbody bulletRb = bulletInstance.GetComponent<Rigidbody>();
            bulletRb.velocity = GetBulletDirection(shootTarget) * shootingSpeed;
            bullet.transform.LookAt(bullet.transform.position + bulletRb.velocity);

            Instantiate(shootEffect, shootTransform);
        }

        private Vector3 GetBulletDirection(Vector3 shootTarget)
        {
            return (shootTarget - shootTransform.position).normalized;
        }

        private Vector3 GetShootTarget()
        {
            RaycastHit hit;
            Ray ray = new Ray(GetRayOrigin(), cam.transform.forward);

            if (Physics.Raycast(ray, out hit, 500f, playerMask))
            {
                return hit.point;
            }

            return cam.position + cam.transform.forward * 500f;
        }

        private Vector3 GetRayOrigin()
        {
            return (cam.position - cam.transform.forward * camMove.cameraTarget.localPosition.z);
        }

        private void OnDrawGizmos()
        {
            if (Application.isPlaying)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawSphere(GetRayOrigin(), .2f);
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(GetShootTarget(), .3f);
                Gizmos.color = Color.blue;
                Gizmos.DrawSphere(shootTransform.position, .1f);
            }
        }
    }
}
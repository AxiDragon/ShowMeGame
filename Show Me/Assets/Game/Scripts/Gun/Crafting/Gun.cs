using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Gunbloem
{
    public class Gun : MonoBehaviour
    {
        [Header("Stats")]
        public int power = 1;
        public int impact = 1;
        public int fireRate = 5;
        public int speed = 10;

        public Bullet bullet;
        private LayerMask playerMask;

        public Transform shootTransform;
        private CameraMovement camMove;
        [HideInInspector] private Transform cam;

        private float shootingSpeed = 250f;

        float gunTimer;

        private void Start()
        {
            cam = Camera.main.transform;
            camMove = GetComponentInParent<CameraMovement>();
            playerMask = LayerMask.GetMask("Player");
            playerMask = ~playerMask;
            UpdateIKAimers();
        }

        private void UpdateIKAimers()
        {
            foreach (IKGunAim constraint in FindObjectsOfType<IKGunAim>())
            {
                constraint.gun = this;
            }
        }

        void Update()
        {
            gunTimer += Time.deltaTime;
            transform.LookAt(GetShootTarget());
        }

        public void Shoot()
        {
            if (gunTimer < (1f / fireRate))
                return;

            gunTimer = 0f;

            Vector3 shootTarget = GetShootTarget();
            Bullet bulletInstance = Instantiate(bullet, shootTransform.position, Quaternion.identity);
            bulletInstance.gun = this;

            Rigidbody bulletRb = bulletInstance.GetComponent<Rigidbody>();
            bulletRb.velocity = GetBulletDirection(shootTarget) * shootingSpeed;
            bullet.transform.LookAt(bullet.transform.position + bulletRb.velocity);

            PlayerParticleManager.ShootEffect(shootTransform);
        }

        private Vector3 GetBulletDirection(Vector3 shootTarget)
        {
            return (shootTarget - shootTransform.position).normalized;
        }

        public Vector3 GetShootTarget()
        {
            RaycastHit hit;
            Ray ray;
            if (cam)
                ray = new Ray(GetRayOrigin(), cam.transform.forward);
            else
                ray = new Ray(transform.position, transform.forward);

            if (Physics.Raycast(ray, out hit, 500f, playerMask))
            {
                return hit.point;
            }

            return cam.position + cam.transform.forward * 500f;
        }

        private Vector3 GetRayOrigin()
        {
            if (cam)
                return (cam.position - cam.transform.forward * camMove.cameraTarget.localPosition.z);

            return transform.position;
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
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gunbloem
{
    public class BoomSphereAttackTest : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Attack();
            }
        }

        private void Attack()
        {
            foreach(Collider c in Physics.OverlapSphere(transform.position + transform.forward * 2f + Vector3.up, 2f))
            {
                if (c.TryGetComponent<EnemyHealth>(out var h))
                {
                    h.TakeDamage(25);
                }
            }

            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = transform.position + transform.forward * 2f + Vector3.up;
            Destroy(sphere, .05f);
        }
    }
}
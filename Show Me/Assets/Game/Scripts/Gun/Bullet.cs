using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gunbloem
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour
    {
        [HideInInspector] public Gun gun;
        private Rigidbody rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            transform.LookAt(transform.position + rb.velocity);    
        }

        private void OnCollisionEnter(Collision collision)
        {
            foreach (Collider coll in Physics.OverlapSphere(transform.position, gun.impact / 10f))
            {
                if (coll.TryGetComponent<EnemyHealth>(out var h))
                {
                    h.TakeDamage((int)gun.power);
                    PlayerParticleManager.HitEffect(collision.GetContact(0).point, gun.impact / 15f);

                    if (h.TryGetComponent<EnemyMovement>(out var m))
                        m.Stun(gun.impact / 10f);
                    
                    if (h.TryGetComponent<Rigidbody>(out var rb))
                        rb.AddExplosionForce(gun.power, transform.position, gun.impact / 10f);
                }
                else
                {
                    PlayerParticleManager.HitEffect(collision.GetContact(0).point, gun.impact / 45f);
                }
            }

            Destroy(gameObject);
        }
    }
}

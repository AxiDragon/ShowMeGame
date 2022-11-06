using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gunbloem 
{
    public class Bullet : MonoBehaviour
    {
        public GunStats stats;

        private void OnCollisionEnter(Collision collision)
        {
            foreach (Collider coll in Physics.OverlapSphere(transform.position, stats.explosionRadius))
            {
                if (coll.TryGetComponent<Rigidbody>(out var rb))
                {
                    rb.AddExplosionForce(stats.explosionForce, transform.position, stats.explosionRadius);
                }
            }

            Destroy(gameObject);
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeweerhousePrototype
{
    public class Bullet : MonoBehaviour
    {
        public GunStats stats;

        private void OnCollisionEnter(Collision collision)
        {
            //GameObject go = Instantiate(GameObject.CreatePrimitive(PrimitiveType.Sphere), transform.position, Quaternion.identity);
            //go.transform.localScale *= stats.explosionRadius;
            //Destroy(go, .5f);
            foreach(Collider coll in Physics.OverlapSphere(transform.position, stats.explosionRadius))
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
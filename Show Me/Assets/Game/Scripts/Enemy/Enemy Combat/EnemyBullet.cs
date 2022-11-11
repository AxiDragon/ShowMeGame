using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gunbloem
{
    public class EnemyBullet : MonoBehaviour
    {
        public Health target;

        private void Awake()
        {
            Destroy(gameObject, 5f);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject == target.gameObject)
            {
                target.TakeDamage(1);
            }

            PlayerParticleManager.EnemyHitEffect(transform.position);
            Destroy(gameObject);
        }
    }
}
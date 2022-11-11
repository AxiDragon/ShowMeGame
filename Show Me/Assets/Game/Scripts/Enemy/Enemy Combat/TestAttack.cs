using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gunbloem
{
    public class TestAttack : MonoBehaviour, IAttack
    {
        [SerializeField] Transform attackOrigin;
        [SerializeField] float attackRange = .2f;

        public void Attack(Health target)
        {
            foreach (Collider coll in Physics.OverlapSphere(attackOrigin.position, attackRange))
            {
                if (coll.TryGetComponent<ProtectionTargetHealth>(out var pth))
                {
                    pth.TakeDamage(1);
                }

                if (coll.TryGetComponent<PlayerHealth>(out var ph))
                {
                    ph.TakeDamage(1);
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(attackOrigin.position, attackRange);
        }
    }
}
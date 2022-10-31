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
                if (coll.TryGetComponent<Health>(out var h))
                {
                    if (h == target)
                        target.TakeDamage(1);
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
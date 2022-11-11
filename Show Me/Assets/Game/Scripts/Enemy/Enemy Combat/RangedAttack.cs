using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gunbloem
{
    public class RangedAttack : MonoBehaviour, IAttack
    {
        [SerializeField] Transform attackOrigin;
        [SerializeField] EnemyBullet enemyBullet;
        [SerializeField] float firingSpeed = 25f;

        public void Attack(Health target)
        {
            EnemyBullet inst = Instantiate(enemyBullet, attackOrigin.position + (transform.forward - transform.up) * .5f, Quaternion.identity);
            inst.transform.LookAt(target.transform);
            inst.GetComponent<Rigidbody>().velocity = inst.transform.forward * firingSpeed;
            inst.target = target;
        }
    }
}
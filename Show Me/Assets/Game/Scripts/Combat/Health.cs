using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gunbloem
{
    public class Health : MonoBehaviour
    {
        public int health;
        internal int maxHealth;

        private void Awake()
        {
            maxHealth = health;
        }

        public void TakeDamage(int damage)
        {
            health -= damage;
            if (health <= 0)
                Die();
        }

        public virtual void Die()
        {
            print(name + " has died!");
        }
    }
}
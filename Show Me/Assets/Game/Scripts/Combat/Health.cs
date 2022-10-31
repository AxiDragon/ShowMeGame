using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gunbloem
{
    public class Health : MonoBehaviour
    {
        public int health = 5;
        [HideInInspector] public int maxHealth;

        private void Start()
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
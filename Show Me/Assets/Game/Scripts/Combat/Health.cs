using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gunbloem
{
    public class Health : MonoBehaviour
    {
        public int health = 5;
        public float flashTime = .05f;
        internal Renderer[] rends;

        [HideInInspector] public int maxHealth;
        [SerializeField] internal float deathEffectSize = 1f;

        public virtual void Start()
        {
            maxHealth = health;
            rends = GetComponentsInChildren<Renderer>();
        }

        public virtual void TakeDamage(int damage)
        {
            health -= damage;
            health = Mathf.Max(health, 0);
            Flash();
            if (health <= 0)
                Die();
        }

        private void Flash()
        {
            CancelInvoke("StopFlash");
            Invoke("StopFlash", flashTime);
            foreach(Renderer rend in rends)
            {
                for (int i = 0; i < rend.materials.Length; i++)
                {
                    rend.materials[i].SetFloat("_DamageFlash", 1);
                }
            }
        }

        private void StopFlash()
        {
            foreach (Renderer rend in rends)
            {
                for (int i = 0; i < rend.materials.Length; i++)
                {
                    rend.materials[i].SetFloat("_DamageFlash", 0);
                }
            }
        }

        public virtual void Die()
        {
            PlayerParticleManager.DeathEffect(transform.position, deathEffectSize);
        }
    }
}
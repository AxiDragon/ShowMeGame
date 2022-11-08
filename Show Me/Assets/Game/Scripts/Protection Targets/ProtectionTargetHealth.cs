using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gunbloem
{
    public class ProtectionTargetHealth : Health
    {
        HealthUI healthUI;

        public override void TakeDamage(int damage)
        {
            UpdateRenderers();
            base.TakeDamage(damage);
        }

        public void UpdateRenderers()
        {
            List<Renderer> rendList = GetComponentsInChildren<Renderer>().ToList();
            for (int i = rendList.Count - 1; i >= 0; i--)
            {
                if (rendList[i].TryGetComponent<ParticleSystem>(out var _))
                    rendList.RemoveAt(i);
            }

            rends = rendList.ToArray();
        }

        private void Awake()
        {
            healthUI = GetComponent<HealthUI>();
        }

        public override void Die()
        {
            Destroy(gameObject);
        }

        private void Update()
        {
            healthUI.healthText.text = health.ToString();
        }
    }
}
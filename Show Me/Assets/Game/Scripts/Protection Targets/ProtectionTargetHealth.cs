using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gunbloem
{
    public class ProtectionTargetHealth : Health
    {
        HealthUI healthUI;

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
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gunbloem
{
    public class PlayerHealth : Health
    {
        public float regenTime = 20f;
        private float regenTimer = 0f;
        [SerializeField] float pulseTime;
        [SerializeField] float pulseSize;
        HealthUI healthUI;
        Vector3 baseScale;
        Vector3 largeScale;

        private void Awake()
        {
            healthUI = GetComponent<HealthUI>();
            baseScale = healthUI.heart.localScale;
            largeScale = baseScale * pulseSize;
        }

        public override void Die()
        {
            print("Game Over!");
        }

        private void Update()
        {
            healthUI.healthText.text = health.ToString();

            if (health < maxHealth)
            {
                regenTimer += Time.deltaTime;
                healthUI.healthSlider.value = regenTimer / regenTime;
            }

            if (regenTimer > regenTime)
            {
                health++;
                regenTimer = 0f;
                StartCoroutine(Pulse());
            }
        }

        private IEnumerator Pulse()
        {
            float time = Mathf.Min(regenTime, pulseTime) / 2f;
            healthUI.heart.LeanScale(largeScale, time).setEaseInBounce();
            yield return new WaitForSeconds(time);
            healthUI.heart.LeanScale(baseScale, time).setEaseInBounce();
        }
    }
}
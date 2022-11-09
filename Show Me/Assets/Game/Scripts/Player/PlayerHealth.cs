using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
            base.Die();
            GetComponent<PlayerInput>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
            StartCoroutine(TimeSlowDown());
        }

        private IEnumerator TimeSlowDown()
        {
            float timer = 0f;
            float time = .45f;
            while (timer < 1f)
            {
                float x = Mathf.Clamp01(timer / time);
                float timeSlowdownFactor = 1f - (1f - x) * (1f - x);
                Time.timeScale = Mathf.Lerp(1f, 0f, timeSlowdownFactor);
                timer += Time.unscaledDeltaTime;
                yield return null;
            }

            GameOver();
        }

        private void GameOver()
        {
            FindObjectOfType<ScoreKeeper>().GameOver();
        }

        public override void TakeDamage(int damage)
        {
            UpdateRenderers();
            base.TakeDamage(damage);
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
    }
}
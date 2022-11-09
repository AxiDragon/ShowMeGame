using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gunbloem
{
    public class GunSeed : MonoBehaviour
    {
        [Header("Max Stats")]
        public int minPower = 1;
        public int minImpact = 1;
        public int minFireRate = 5;
        public int minSpeed = 8;
        [Header("Min Stats")]
        public int maxPower = 1;
        public int maxImpact = 1;
        public int maxFireRate = 5;
        public int maxSpeed = 12;
        [Header("Other")]
        public Sprite sprite;
        public GunPart resultPart;
        [SerializeField] private GunPlant plant;
        [HideInInspector] public bool collected = false;
        private Action destroyAction;
        private float despawnTime = 45f;

        private void Awake()
        {
            destroyAction = () => { Destroy(gameObject); };
        }

        private void Start()
        {
            UpdateStats();
            RandomizeStats();
            Vector3 startScale = transform.localScale;
            transform.localScale = transform.localScale / 1000f;
            transform.LeanScale(startScale, .5f).setEaseOutCubic();

            Invoke("Despawn", despawnTime);
        }

        private void UpdateStats()
        {
            EnemySpawner es = FindObjectOfType<EnemySpawner>();
            float modifier = Mathf.Pow(1.05f, es.waveCount);
            minPower = (int)(minPower * modifier);
            minImpact = (int)(minImpact * modifier);
            minFireRate = (int)(minFireRate * modifier);
            maxPower = (int)(maxPower * modifier);
            maxImpact = (int)(maxImpact * modifier);
            maxFireRate = (int)(maxFireRate * modifier);
        }

        public void PlantSeed(Vector3 pos)
        {
            Quaternion randomRot = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);

            GunPlant p = Instantiate(plant, pos, randomRot);
            GunPart gp = Instantiate(resultPart, Vector3.zero, randomRot);
            p.resultPart = gp;
            float time = Mathf.Pow((resultPart.power + resultPart.fireRate + resultPart.impact) / 1.5f, .8f);
            p.SetGrowTime(time);
        }

        private void RandomizeStats()
        {
            resultPart.power = UnityEngine.Random.Range(minPower, maxPower);
            resultPart.impact = UnityEngine.Random.Range(minImpact, maxImpact);
            resultPart.fireRate = UnityEngine.Random.Range(minFireRate, maxFireRate);
            resultPart.speed = UnityEngine.Random.Range(minSpeed, maxSpeed);
        }

        public void Collect()
        {
            if (!collected)
            {
                collected = true;
                CancelInvoke("Despawn");
                transform.LeanScale(Vector3.one / 1000f, .5f).setEaseInCubic().setOnComplete(destroyAction);
                transform.LeanMove(GameObject.FindWithTag("Player").transform.position, .5f);
            }
        }

        private void Despawn()
        {
            transform.LeanScale(Vector3.one / 1000f, .5f).setEaseInCubic().setOnComplete(destroyAction);
        }
    }
}
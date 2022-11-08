using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        private void Awake()
        {
            destroyAction = () => { Destroy(gameObject); };
        }

        private void Start()
        {
            UpdateStats();
            RandomizeStats();
        }

        private void UpdateStats()
        {
            EnemySpawner es = FindObjectOfType<EnemySpawner>();
            float modifier = Mathf.Pow(1.1f, es.waveCount);
            minPower = (int)(minPower * modifier);
            minImpact = (int)(minImpact * modifier);
            minFireRate = (int)(minFireRate * modifier);
            maxPower = (int)(maxPower * modifier);
            maxImpact = (int)(maxImpact * modifier);
            maxFireRate = (int)(maxFireRate * modifier);
        }

        public void PlantSeed(Vector3 pos)
        {
            RandomizeStats();

            GunPlant p = Instantiate(plant, pos, Quaternion.identity);
            p.resultPart = resultPart;
            float time = (resultPart.power + resultPart.fireRate + resultPart.impact) / 1.5f;
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
                transform.LeanScale(Vector3.one / 1000f, .5f).setEaseInCubic().setOnComplete(destroyAction);
                transform.LeanMove(GameObject.FindWithTag("Player").transform.position, .5f);
            }
        }
    }
}
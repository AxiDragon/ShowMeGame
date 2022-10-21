using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BicycleSurfer
{
    public class WaveSpawner : MonoBehaviour
    {
        [SerializeField] Wave wave;
        [SerializeField] Collider spawn;
        [SerializeField] float spawnCooldown = 2f;

        void Start()
        {
            StartCoroutine(WaveSpawnerCoroutine());
        }

        private IEnumerator WaveSpawnerCoroutine()
        {
            while (true)
            {
                SpawnWave();
                yield return new WaitForSeconds(spawnCooldown);
            }
        }

        private void SpawnWave()
        {
            Bounds bound = spawn.bounds;

        }
    }
}
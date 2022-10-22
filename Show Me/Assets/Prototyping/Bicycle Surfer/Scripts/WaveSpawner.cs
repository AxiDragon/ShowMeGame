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
            Vector3 randPoint = bound.center;
            randPoint.x = UnityEngine.Random.Range(bound.min.x, bound.max.x);
            randPoint.y = UnityEngine.Random.Range(bound.min.y, bound.max.y);
            randPoint.z = UnityEngine.Random.Range(bound.min.z, bound.max.z);

            Instantiate(wave, randPoint, wave.transform.rotation);
        }
    }
}
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Gunbloem
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] EnemySpawnList spawnList;
        [SerializeField] public int startCredits = 50;
        [SerializeField] private float waveTime = 10f;
        [SerializeField] private float waveTimeIncremental = 2f;
        [SerializeField] private float graceTime = 5f;
        [SerializeField] float waveMultiplier = 1.15f;
        [SerializeField] float minimumSpawnRange = 5f;
        [SerializeField] float maximumSpawnRange = 10f;
        [SerializeField] int difficultySpikeWaves = 5;
        [SerializeField] float spikeIncrease = .1f;

        [HideInInspector] public bool active = true;
        [HideInInspector] public int waveCount = 0;
        private int credits;

        private int spawnAttempts = 30;

        void Start()
        {
            StartCoroutine(WaveManagerCoroutine());
        }

        private IEnumerator WaveManagerCoroutine()
        {
            while (active)
            {
                yield return new WaitForSeconds(graceTime);
                waveCount++;
                credits = Mathf.RoundToInt(startCredits * GetMultiplier());

                if (waveCount % difficultySpikeWaves == 0)
                {
                    waveMultiplier += spikeIncrease;
                    credits = Mathf.RoundToInt(startCredits * GetMultiplier() * 1.5f);
                }

                yield return WaveCoroutine();
            }
        }

        private IEnumerator WaveCoroutine()
        {
            List<EnemyController> waveSpawns = GetWaveSpawns();
            float spawnTime = waveTime + waveTimeIncremental * waveCount;
            float timePerEnemy = spawnTime / waveSpawns.Count;

            for (int i = 0; i < waveSpawns.Count; i++)
            {
                Vector3 spawnPos = GetRandomSpawnPosition();

                if (spawnPos != Vector3.positiveInfinity)
                {
                    if (i < 3)
                    {
                        SpawnEnemy(waveSpawns[i], spawnPos, true);
                    }

                    SpawnEnemy(waveSpawns[i], spawnPos);
                }

                yield return new WaitForSeconds(timePerEnemy);
            }
        }

        private List<EnemyController> GetWaveSpawns()
        {
            List<EnemySpawn> possibleSpawns = spawnList.enemies.ToList();
            List<EnemyController> spawns = new List<EnemyController>();

            while(possibleSpawns.Count > 0)
            {
                int random = Random.Range(0, possibleSpawns.Count);
                if ((credits > possibleSpawns[random].creditCost) && (waveCount >= possibleSpawns[random].firstWaveAppearance))
                {
                    spawns.Add(possibleSpawns[random].enemy);
                    credits -= possibleSpawns[random].creditCost;
                }
                else
                {
                    possibleSpawns.RemoveAt(random);
                }
            }

            return spawns;
        }

        private Vector3 GetRandomSpawnPosition()
        {
            Vector3 target = GetRandomTarget();

            for (int i = 0; i < spawnAttempts; i++)
            {
                Vector3 offset = GetSpawnOffset();
                Vector3 pos = target + offset;
                NavMeshHit hit;

                if (NavMesh.SamplePosition(pos, out hit, maximumSpawnRange, NavMesh.AllAreas))
                {
                    return hit.position;
                }
            }

            return Vector3.positiveInfinity;
        }

        private Vector3 GetSpawnOffset()
        {
            float spawnOffset = Random.Range(minimumSpawnRange, maximumSpawnRange);
            float randomRotation = Random.Range(0, 360);
            Vector3 randomVector = Quaternion.AngleAxis(randomRotation, Vector3.up) * Vector3.forward * spawnOffset;
            return randomVector;
        }

        private void SpawnEnemy(EnemyController enemy, Vector3 pos)
        {
            EnemyController enemyInstance = Instantiate(enemy, pos, Quaternion.identity);

            EnemyHealth instanceHealth = enemyInstance.GetComponent<EnemyHealth>();
            int newHealth = Mathf.RoundToInt(instanceHealth.health * GetMultiplier());
            instanceHealth.maxHealth = instanceHealth.health = newHealth;
        }

        private void SpawnEnemy(EnemyController enemy, Vector3 pos, bool guaranteedDrop)
        {
            EnemyController enemyInstance = Instantiate(enemy, pos, Quaternion.identity);

            EnemyHealth instanceHealth = enemyInstance.GetComponent<EnemyHealth>();
            int newHealth = Mathf.RoundToInt(instanceHealth.health * GetMultiplier());
            instanceHealth.maxHealth = instanceHealth.health = newHealth;

            enemyInstance.GetComponent<EnemyDrops>().guaranteedDrop = guaranteedDrop;
        }

        private float GetMultiplier()
        {
            return Mathf.Pow(waveMultiplier, waveCount);
        }

        private Vector3 GetRandomTarget()
        {
            List<GameObject> targets = GameObject.FindGameObjectsWithTag("Protection Target").ToList();
            targets.Add(GameObject.FindGameObjectWithTag("Player"));

            int random = Random.Range(0, targets.Count);

            return targets[random].transform.position;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, maximumSpawnRange);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, minimumSpawnRange);
        }
    }
}
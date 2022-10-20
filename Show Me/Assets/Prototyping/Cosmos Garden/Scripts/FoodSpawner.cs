using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CosmosGarden
{
    public class FoodSpawner : MonoBehaviour
    {
        [SerializeField] List<Food> food = new List<Food>();
        [SerializeField] private float spawnRange = 10f;
        [SerializeField] private float cooldown;

        void Start()
        {
            StartCoroutine(SpawnFoodCoroutine());
        } 

        private IEnumerator SpawnFoodCoroutine()
        {
            while(true)
            {
                SpawnFood();
                yield return new WaitForSeconds(cooldown);
            }
        }

        private void SpawnFood()
        {
            Food spawnedFood = GetRandomFood();
            Vector3 pos = GetRandomPosition();
            Instantiate(spawnedFood, pos, Quaternion.identity);
        }

        private Vector3 GetRandomPosition()
        {
            float rX = UnityEngine.Random.Range(-spawnRange, spawnRange);
            float rZ = UnityEngine.Random.Range(-spawnRange, spawnRange);
            return new Vector3(rX, 0f, rZ);
        }

        private Food GetRandomFood()
        {
            int r = UnityEngine.Random.Range(0, food.Count);
            return food[r];
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, spawnRange);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gunbloem
{
    public class EnemyDrops : MonoBehaviour
    {
        public float dropChance = .2f;

        public void DropItem()
        {
            if (Random.value > dropChance)
                return;

            int item = GetDrop();

            print($"Dropped a spectacular {item}!");
        }

        private int GetDrop()
        {
            return Random.Range(0, 9);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gunbloem
{
    public class EnemyDrops : MonoBehaviour
    {
        [SerializeField] EnemyDropTable dropTable;
        [SerializeField] float dropOffset = .2f;

        public void DropItem()
        {
            if (Random.value > dropTable.overallDropChance)
                return;

            GameObject drop = GetDrop();

            Instantiate(drop, transform.position + Vector3.one * dropOffset, Quaternion.identity);
        }

        private GameObject GetDrop()
        {
            DropList dropList = GetDropList();
            int random = Random.Range(0, dropList.droppables.Length);

            return dropList.droppables[random];
        }

        private DropList GetDropList()
        {
            float total = 0f;

            foreach (DropList dl in dropTable.drops)
            {
                total += dl.chance;
            }

            float random = Random.Range(0f, total);
            total = 0f;

            foreach (DropList dl in dropTable.drops)
            {
                total += dl.chance;
                if (random < total)
                    return dl;
            }

            return new DropList();
        }
    }
}
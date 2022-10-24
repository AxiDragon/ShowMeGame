using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeweerhousePrototype
{
    public class PlayerPlanter : MonoBehaviour
    {
        PlayerInventory inventory;

        void Start()
        {
            inventory = GetComponent<PlayerInventory>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (TryPlant())
                    return;
                else
                    TryCollect();
            }
        }

        private void TryCollect()
        {
            SeedPot targetPot = GetPot();
            if (targetPot != null && targetPot.finishedPart != null)
            {
                inventory.gunParts.Add(targetPot.CollectPart());
            }
        }

        private bool TryPlant()
        {
            if (inventory.seeds.Count == 0)
                return false;

            SeedPot targetPot = GetPot();
            if (targetPot != null && targetPot.plantedSeed == null)
            {
                targetPot.PlantSeed(inventory.seeds[0]);
                inventory.seeds.RemoveAt(0);
                return true;
            }
            return false;
        }

        private SeedPot GetPot()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.TryGetComponent<SeedPot>(out var pot))
                    return pot;
            }

            return null;
        }
    }
}
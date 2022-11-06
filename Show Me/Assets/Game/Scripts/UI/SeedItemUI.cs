using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gunbloem
{
    public class SeedItemUI : ItemUI
    {
        [HideInInspector] public GunSeed seed;

        private void Start()
        {
            trigger.seed = seed;
            trigger.type = ToolTipTrigger.ToolTipTriggerType.Seed;
            image.sprite = seed.sprite;
        }

        public override void Select()
        {
            base.Select();
            PlantSeed();
        }

        private void PlantSeed()
        {
            GetComponentInParent<Harvest>().Plant(seed);
            PlayerInventory inv = GetComponentInParent<PlayerInventory>();
            inv.seeds.Remove(seed);
            inv.InteractInventory();
        }
    }
}
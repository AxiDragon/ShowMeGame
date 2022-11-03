using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gunbloem
{
    public class SeedItemUI : ItemUI
    {
        public GunSeed seed;

        private void Start()
        {
            trigger.seed = seed;
            trigger.type = ToolTipTrigger.ToolTipTriggerType.Seed;
            image.sprite = seed.sprite;
        }

        public override void Select()
        {
            base.Select();
        }
    }
}
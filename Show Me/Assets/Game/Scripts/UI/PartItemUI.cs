using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gunbloem
{
    public class PartItemUI : ItemUI
    {
        [HideInInspector] public GunPart part;
        private Harvest harvest;

        private void Start()
        {
            trigger.part = part;
            trigger.type = ToolTipTrigger.ToolTipTriggerType.Part;

            if (image != null)
                image.sprite = part.GetSprite();

            harvest = GetComponentInParent<Harvest>();
        }

        public override void Select()
        {
            base.Select();
            if (selected)
                harvest.SelectBreed(part);
            else
                harvest.DeselectBreed(part);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gunbloem
{
    public class PartItemUI : ItemUI
    {
        [HideInInspector] public GunPart part;

        private void Start()
        {
            trigger.part = part;
            trigger.type = ToolTipTrigger.ToolTipTriggerType.Part;

            if (image != null)
                image.sprite = part.GetSprite();
        }

        public override void Select()
        {
            base.Select();
        }
    }
}
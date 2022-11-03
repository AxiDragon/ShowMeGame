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
            image.sprite = part.sprite;
        }

        public override void Select()
        {
            base.Select();
        }
    }
}
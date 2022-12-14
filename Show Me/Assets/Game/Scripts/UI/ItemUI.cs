using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gunbloem
{
    public class ItemUI : MonoBehaviour
    {
        internal bool selected;
        public Image image;
        internal Image bg;
        internal ToolTipTrigger trigger;
        internal ItemToSelect selectable;

        private void Awake()
        {
            bg = GetComponent<Image>();
            trigger = GetComponent<ToolTipTrigger>();
        }

        public virtual void Select()
        {
            selected = !selected;
            //rgb(220, 123, 109)
            bg.color = selected ? new Color(220f / 255f, 123f / 255f, 109f / 255f): Color.white;
        }
    }
}
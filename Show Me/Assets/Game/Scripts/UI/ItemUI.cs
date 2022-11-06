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
            Debug.Log("selected");
            selected = !selected;
            bg.color = selected ? Color.gray : Color.white;
        }
    }
}
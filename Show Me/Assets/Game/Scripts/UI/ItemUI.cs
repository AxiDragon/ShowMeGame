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
        internal ToolTipTrigger trigger;
        internal ItemToSelect selectable;

        private void Awake()
        {
            trigger = GetComponent<ToolTipTrigger>();
        }

        public virtual void Select()
        {
            Debug.Log("selected");
            selected = !selected;
            image.color = selected ? Color.red : Color.green;
        }
    }
}
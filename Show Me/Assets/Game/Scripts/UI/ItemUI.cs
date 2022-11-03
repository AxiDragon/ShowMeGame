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

        private void Awake()
        {
            trigger = GetComponent<ToolTipTrigger>();
        }

        public virtual void Select()
        {
            selected = !selected;
            image.color = selected ? Color.red : Color.green;
        }
    }
}
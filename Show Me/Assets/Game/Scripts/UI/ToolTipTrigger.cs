using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Gunbloem
{
    public class ToolTipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public ToolTipTriggerType type;
        [Header("If Seed")]
        public GunSeed seed;
        [Header("If Part")]
        public GunPart part;

        public enum ToolTipTriggerType
        {
            Seed,
            Part
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (type == ToolTipTriggerType.Seed)
                ToolTipSystem.Show(seed);
            else
                ToolTipSystem.Show(part);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ToolTipSystem.Hide();
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Gunbloem
{
    public class Workbench : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [HideInInspector] public bool mouseOver = false;

        public void OnPointerEnter(PointerEventData eventData)
        {
            mouseOver = true;
            print("Entered Workbench");
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            mouseOver = false;
            print("Left Workbench");
        }
    }
}
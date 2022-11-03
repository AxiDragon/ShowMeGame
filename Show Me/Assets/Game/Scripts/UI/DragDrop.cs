using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Gunbloem
{
    public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        private Workbench workbench;
        private Canvas canvas;
        private RectTransform rect;
        private Image sprite;
        private Transform originalParent;
        private Attachment attachment;
        private Vector2 startAnchoredPosition;

        private void Awake()
        {
            sprite = GetComponent<Image>();
            rect = GetComponent<RectTransform>();
            workbench = FindObjectOfType<Workbench>();
            canvas = GetComponentInParent<Canvas>();
            attachment = GetComponentInChildren<Attachment>();
            originalParent = transform.parent;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            startAnchoredPosition = rect.anchoredPosition;
            sprite.raycastTarget = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            rect.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (workbench.mouseOver)
            {
                Vector2 pos = rect.position;
                transform.SetParent(workbench.transform, false);
                rect.position = pos;
            }
            else
            {
                transform.SetParent(originalParent, false);
                rect.anchoredPosition = startAnchoredPosition;
            }
            sprite.raycastTarget = true;
        }
    }
}
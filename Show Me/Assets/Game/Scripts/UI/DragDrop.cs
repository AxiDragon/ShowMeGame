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
        private Vector2 originalPosition;

        private void Awake()
        {
            sprite = GetComponent<Image>();
            rect = GetComponent<RectTransform>();
            workbench = FindObjectOfType<Workbench>();
            canvas = GetComponentInParent<Canvas>();
            attachment = GetComponentInChildren<Attachment>();
            originalParent = transform.parent;
            originalPosition = transform.localPosition;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            sprite.raycastTarget = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            rect.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (workbench.mouseOver)
                PlaceOnWorkbench();
            else
                SnapBack();

            sprite.raycastTarget = true;
        }

        public void SnapBack()
        {
            transform.SetParent(originalParent, false);
            transform.localPosition = originalPosition;
            attachment.benched = false;
        }

        public void PlaceOnWorkbench()
        {
            Vector2 pos = rect.position;
            transform.SetParent(workbench.transform, false);
            rect.position = pos;
            attachment.benched = true;
            attachment.TrySnap();
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gunbloem
{
    public class Swipe : MonoBehaviour
    {
        public RectTransform rect;
        public float start;
        public float end;

        void Awake()
        {
            start = 0f;
            end = -3000f;

            SwipeRect(false, null);
        }

        public void SwipeRect(bool swipeIn, Action endAction)
        {
            float endPos = swipeIn ? start : end;

            if (endAction != null)
                rect.LeanMoveLocalY(endPos, 2f).setEaseInOutCubic().setOnComplete(endAction).setIgnoreTimeScale(true);
            else
                rect.LeanMoveLocalY(endPos, 2f).setEaseInOutCubic().setIgnoreTimeScale(true);
        }
    }
}
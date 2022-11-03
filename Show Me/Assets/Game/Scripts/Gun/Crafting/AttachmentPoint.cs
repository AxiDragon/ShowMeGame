using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gunbloem
{
    public partial class AttachmentPoint : MonoBehaviour
    {
        public Side side;
        private Side oppSide;
        private Attachment attacher;

        [SerializeField] private float snapDistance = 2f;

        public enum Side
        {
            Left,
            Right,
            Top,
            Bottom
        }

        private void Awake()
        {
            attacher = GetComponentInParent<Attachment>();
        }

        private void Start()
        {
            oppSide = GetOppositeSide();
        }

        private Side GetOppositeSide()
        {
            switch (side)
            {
                case Side.Left:
                    return Side.Right;
                case Side.Right:
                    return Side.Left;
                case Side.Top:
                    return Side.Bottom;
                case Side.Bottom:
                    return Side.Left;
                default:
                    throw new NotImplementedException("No side specified, somehow");
            }
        }
    }
}
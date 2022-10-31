using System;
using UnityEngine;

namespace Gunbloem
{
    [Serializable]
    public struct DropList
    {
        [Tooltip("Does nothing, just for organization")]
        public string name;
        public float chance;
        public GameObject[] droppables;
    }
}
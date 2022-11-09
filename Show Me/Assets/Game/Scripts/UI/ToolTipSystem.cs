using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gunbloem
{
    public class ToolTipSystem : MonoBehaviour
    {
        private static ToolTipSystem current;

        public ToolTip tip;

        void Awake()
        {
            current = this;
            Hide();
        }
        
        public static void Show(GunSeed s)
        {
            current.tip.Show(s);
        }

        public static void Show(GunPart p)
        {
            current.tip.Show(p);
        }

        public static void Hide()
        {
            current.tip.Hide();
        }
    }
}
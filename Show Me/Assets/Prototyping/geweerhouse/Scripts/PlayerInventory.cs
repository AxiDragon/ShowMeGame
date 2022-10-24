using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeweerhousePrototype
{
    public class PlayerInventory : MonoBehaviour
    {
        public List<BulletSeed> seeds = new List<BulletSeed>();
        public List<GunPart> gunParts = new List<GunPart>();
        private int money = 5;

        public void ChangeMoney(int amount)
        {
            money += amount;
        }

        private void OnGUI()
        {
            GUI.skin.label.fontSize = 50;
            GUI.Label(new Rect(100, 100, 500, 500), money.ToString());
        }
    }
}

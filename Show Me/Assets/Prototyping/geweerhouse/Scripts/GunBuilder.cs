using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeweerhousePrototype
{
    public class GunBuilder : MonoBehaviour
    {
        private void OnGUI()
        {
            GUI.skin.button.fontSize = 25;
            if (GUI.Button(new Rect(100, 100, 250, 100), "Build Gun"))
            {
                BuildGun();
            }
        }

        private void BuildGun()
        {
            GunPart[] parts = FindObjectsOfType<GunPart>();
            GunStats stats = GetGunStats(parts);
        }

        private GunStats GetGunStats(GunPart[] parts)
        {
            GunStats stats = new GunStats();
            int man = 0;
            int aut = 0;

            for (int i = 0; i < parts.Length; i++)
            {
                stats.fireRate += parts[i].stats.fireRate;
                stats.explosionRadius += parts[i].stats.explosionRadius;
                stats.explosionForce += parts[i].stats.explosionForce;
                stats.firingSpeed += parts[i].stats.firingSpeed;
                stats.recoil += parts[i].stats.recoil;
                stats.emotionalStability += parts[i].stats.emotionalStability;

                if (parts[i].stats.function == Functioning.Manual)
                    man++;
                else
                    aut++;
            }

            stats.function = (man > aut) ? Functioning.Manual : Functioning.Automatic;

            float div = Mathf.Sqrt(parts.Length);
            stats.fireRate /= div;
            stats.explosionRadius /= div;
            stats.explosionForce /= div;
            stats.firingSpeed /= div;
            stats.recoil /= div;
            stats.emotionalStability /= div;

            return stats;
        }
    }
}
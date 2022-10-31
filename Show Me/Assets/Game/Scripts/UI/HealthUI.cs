using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Gunbloem
{
    public class HealthUI : MonoBehaviour
    {
        [HideInInspector] public TextMeshProUGUI healthText;
        [HideInInspector] public Slider healthSlider;
        public Transform heart;

        private void Awake()
        {
            healthText = heart.GetComponentInChildren<TextMeshProUGUI>();
            healthSlider = heart.GetComponentInChildren<Slider>();
        }

        void LateUpdate()
        {
            heart.LookAt(Camera.main.transform.position);
        }
    }
}
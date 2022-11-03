using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

namespace Gunbloem
{
    public class ToolTip : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI power;
        [SerializeField] private TextMeshProUGUI impact;
        [SerializeField] private TextMeshProUGUI fireRate;
        [SerializeField] private TextMeshProUGUI speed;
        RectTransform rect;

        private void Awake()
        {
            rect = transform.parent.GetComponent<RectTransform>();
        }

        private void Update()
        {
            Vector2 position = Input.mousePosition;
            transform.position = position;
        }

        public void Show(GunSeed seed)
        {
            gameObject.SetActive(true);
            power.text = $"{seed.minPower} - {seed.maxPower}";
            impact.text = $"{seed.minImpact} - {seed.maxImpact}";
            fireRate.text = $"{seed.minFireRate} - {seed.minFireRate}";
            speed.text = $"{seed.minSpeed} - {seed.maxSpeed}";
        }

        public void Show(GunPart part)
        {
            gameObject.SetActive(true);
            power.text = $"{part.power}";
            impact.text = $"{part.impact}";
            fireRate.text = $"{part.fireRate}";
            speed.text = $"{part.speed}";
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
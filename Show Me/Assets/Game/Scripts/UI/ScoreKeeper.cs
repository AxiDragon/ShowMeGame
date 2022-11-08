using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Gunbloem
{
    public class ScoreKeeper : MonoBehaviour
    {
        public static ScoreKeeper instance;
        public static int score = 0;
        [SerializeField] private TextMeshProUGUI scoreText;

        void Awake()
        {
            instance = this;
            score = 0;
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        public static void IncreaseScore(int amount)
        {
            score += amount;
        }

        public static void DisplayScore()
        {
            instance.scoreText.text = $"Score: {score}";
        }

        public void GameOver()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }

            DisplayScore();
        }
    }
}
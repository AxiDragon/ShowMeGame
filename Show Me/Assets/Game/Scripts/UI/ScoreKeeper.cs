using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using UnityEngine.UI;

namespace Gunbloem
{
    public class ScoreKeeper : MonoBehaviour
    {
        public static ScoreKeeper instance;
        public static int score = 0;
        [SerializeField] private TextMeshProUGUI scoreText;
        private Action restartAction;
        private GraphicRaycaster raycaster;

        void Awake()
        {
            instance = this;
            score = 0;
            raycaster = GetComponent<GraphicRaycaster>();
            restartAction = () => { Time.timeScale = 1f;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); };

            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }

            raycaster.enabled = false;
        }

        public static void IncreaseScore(int amount)
        {
            score += amount;
        }

        public static void DisplayScore()
        {
            instance.scoreText.text = $"score : {score}";
        }

        public void GameOver()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }

            raycaster.enabled = true;
            DisplayScore();
        }

        public void Restart()
        {
            FindObjectOfType<Swipe>().SwipeRect(true, restartAction);
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeweerhousePrototype
{
    public class GunEmotionHandler : MonoBehaviour
    {
        [SerializeField] GameObject neutral;
        [SerializeField] GameObject angry;
        [SerializeField] GameObject happy;
        [SerializeField] List<GunEmotionHandler> friends = new List<GunEmotionHandler>();
        [SerializeField] List<GunEmotionHandler> enemies = new List<GunEmotionHandler>();
        private float affectRange = 5f;

        public enum Emotion
        {
            Happy,
            Angry,
            Neutral
        }

        private void Update()
        {
            float closest = Mathf.Infinity;
            GunEmotionHandler closestGun = null;
            foreach (GunEmotionHandler geh in FindObjectsOfType<GunEmotionHandler>())
            {
                if (geh == this)
                    continue;

                float dis = (transform.position - geh.transform.position).magnitude;

                if (dis < closest)
                {
                    closestGun = geh;
                    closest = dis;
                }
            }

            if (closestGun != null)
            {
                React(closestGun);
                return;
            }

            SetEmotion(Emotion.Neutral);
        }

        private void SetEmotion(Emotion em)
        {
            switch (em)
            {
                case Emotion.Happy:
                    neutral.SetActive(false);
                    angry.SetActive(false);
                    happy.SetActive(true);
                    break;
                case Emotion.Angry:
                    neutral.SetActive(false);
                    angry.SetActive(true);
                    happy.SetActive(false);
                    break;
                case Emotion.Neutral:
                    neutral.SetActive(true);
                    angry.SetActive(false);
                    happy.SetActive(false);
                    break;
            }
        }

        private void React(GunEmotionHandler geh)
        {
            Emotion em = Emotion.Neutral;
            if (enemies.Contains(geh))
            {
                em = Emotion.Angry;
            }
            if (friends.Contains(geh))
            {
                em = Emotion.Happy;
            }
            SetEmotion(em);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, affectRange);
        }
    }
}
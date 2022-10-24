using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeweerhousePrototype
{
    public partial class Gun : MonoBehaviour
    {
        public GunStats stats;
        public GameObject gun;
        [SerializeField] Bullet bullet;
        public float happiness;
        public float angerThreshold;
        public float angerChance = 2f;
        float gunTimer;

        private void Update()
        {
            switch (stats.function)
            {
                case Functioning.Manual:
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            Shoot();
                        }
                        break;
                    }
                case Functioning.Automatic:
                    {
                        {
                            if (Input.GetMouseButton(0))
                            {
                                Shoot();
                            }
                            break;
                        }
                    }
            }

            gunTimer += Time.deltaTime;
        }

        private void FixedUpdate()
        {
            if ((happiness < angerThreshold) && (Random.value < (angerChance / 1000)))
            {
                Shoot();
            }
        }

        public void Shoot()
        {
            if (gunTimer < stats.fireRate)
                return;

            gunTimer = 0f;
            Bullet bulletInstance = Instantiate(bullet, transform.position + transform.forward, Quaternion.identity);
            bulletInstance.stats = stats;
            bulletInstance.GetComponent<Rigidbody>().velocity = transform.forward * stats.firingSpeed;
        }
    }
}
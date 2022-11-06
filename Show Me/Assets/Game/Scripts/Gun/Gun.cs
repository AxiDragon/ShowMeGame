using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gunbloem 
{
    public class Gun : MonoBehaviour
    {
        public GunStats stats;
        public GameObject gun;
        [SerializeField] Bullet bullet;

        float gunTimer;

        // Update is called once per frame
        void Update()
        {
            gunTimer += Time.deltaTime;
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


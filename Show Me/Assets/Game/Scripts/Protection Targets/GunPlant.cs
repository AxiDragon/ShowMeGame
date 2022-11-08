using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gunbloem
{
    public class GunPlant : MonoBehaviour
    {
        private Animator anim;
        [HideInInspector] public bool harvestable = false;
        public GunPart resultPart;
        [SerializeField] private Transform displayTransform;
        private Action destroyAction;

        private void Awake()
        {
            destroyAction = () => { Destroy(gameObject); };
            anim = GetComponent<Animator>();
        }

        public void SetGrowTime(float time)
        {
            //Regular animation time is 6.458
            anim.speed = 6.458f / time;
        }

        public GunPart HarvestGunPart()
        {
            if (harvestable)
            {
                harvestable = false;
                transform.LeanScale(Vector3.one / 1000f, .5f).setEaseInCubic().setOnComplete(destroyAction);
                return resultPart;
            }

            return null;
        }

        //Animation trigger
        public void HarvestTrigger()
        {
            harvestable = true;
            //Display harvestable gunpart
            GunPart part = Instantiate(resultPart, displayTransform);
            part.transform.localScale = Vector3.one / 100f;
            part.transform.LeanScale(Vector3.one * 2f, .5f).setEaseOutBack();
        }
    }
}
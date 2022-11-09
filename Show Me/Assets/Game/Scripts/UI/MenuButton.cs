using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gunbloem
{
    public class MenuButton : MonoBehaviour
    {
        [SerializeField] GameObject foldOut;
        [SerializeField] private float slideTime = .4f;
        private MenuButton[] siblings;
        private bool sliding = false;
        private float start;
        private float end;
        private Action slideIn;
        private Action slideOut;

        void Start()
        {
            siblings = FindObjectsOfType<MenuButton>();
            start = foldOut.transform.localPosition.y;
            end = start - 2500f;

            foldOut.transform.localPosition -= Vector3.up * 2500f;

            slideIn = () => { sliding = false; };
            slideOut = () =>
            {
                sliding = false;
                foldOut.SetActive(false);
            };
        }

        public void Interact()
        {
            if (sliding)
                return;

            if (foldOut.activeSelf)
            {
                Close();
            }
            else
            {
                foreach (MenuButton mb in siblings)
                {
                    if (mb != this)
                        mb.Close();
                    else
                        mb.Open();
                }
            }
        }

        public void Open()
        {
            sliding = true;
            foldOut.SetActive(true);

            foldOut.LeanMoveLocalY(start, slideTime).setEaseInOutCubic().setIgnoreTimeScale(true).setOnComplete(slideIn);
        }

        public void Close()
        {
            sliding = true;

            foldOut.LeanMoveLocalY(end, slideTime).setEaseInOutCubic().setIgnoreTimeScale(true).setOnComplete(slideOut);
        }
    }
}
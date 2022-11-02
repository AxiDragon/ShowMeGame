using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gunbloem
{
    public class MenuButton : MonoBehaviour
    {
        [SerializeField] GameObject foldOut;
        private MenuButton[] siblings;

        void Start()
        {
            siblings = FindObjectsOfType<MenuButton>();
        }

        void Update()
        {

        }

        public void Interact()
        {
            if (foldOut.activeSelf)
            {
                Close();
            }
            else
            {
                foreach(MenuButton mb in siblings)
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
            foldOut.SetActive(true);
        }

        public void Close()
        {
            foldOut.SetActive(false);
        }
    }
}
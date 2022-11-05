using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Gunbloem
{
    public class GunAssembler : MonoBehaviour
    {
        private PlayerInventory inventory;
        private Workbench bench;
        [SerializeField] private Transform playerHand;
        [SerializeField] private Transform craftInventory;
        [SerializeField] private UnityEvent gunCrafted;

        private void Awake()
        {
            inventory = GetComponentInParent<PlayerInventory>();
            bench = GetComponent<Workbench>();
        }

        public void CraftGun()
        {
            List<GunPart> usedParts = GetUsedParts();
            print(usedParts.Count);

            if (usedParts.Count <= 0)
            {
                Debug.LogWarning("No gun parts found!");
                return;
            }

            AssembleGun(usedParts);
            RemoveFromInventory(usedParts);
            bench.ClearWorkbench();
        }

        private void RemoveFromInventory(List<GunPart> usedParts)
        {
            for (int i = 0; i < usedParts.Count; i++)
            {
                inventory.parts.Remove(usedParts[i]);
            }

            inventory.GenerateInventory();
        }

        private List<GunPart> GetUsedParts()
        {
            //Looks at the contents of the craft inventory, returning those without a child (therefore being used)
            List<GunPart> usedParts = new List<GunPart>();

            for (int i = 0; i < craftInventory.childCount; i++)
            {
                Transform c = craftInventory.GetChild(i);
                if (c.childCount <= 0)
                    usedParts.Add(c.GetComponent<PartItemUI>().part);
            }

            return usedParts;
        }

        private void AssembleGun(List<GunPart> parts)
        {
            GameObject model = AssembleModel(parts);
            Destroy(model.GetComponent<Collider>());
            Gun gun = model.AddComponent<Gun>();

            float div = Mathf.Sqrt(parts.Count);
            gun.power = (from part in parts select part.power).Sum() / div;
            gun.fireRate = (from part in parts select part.fireRate).Sum() / div;
            gun.impact = (from part in parts select part.impact).Sum() / div;
            gun.speed = (from part in parts select part.speed).Sum() / div;
            
            PlaceModelInHand(ref model);
        }

        private void PlaceModelInHand(ref GameObject model)
        {
            if (playerHand.childCount > 0)
            {
                for (int i = playerHand.childCount - 1; i >= 0; i--)
                {
                    Destroy(playerHand.GetChild(i).gameObject);
                }
            }
            model.transform.parent = playerHand;
            model.transform.localPosition = Vector3.zero;
            model.transform.localRotation = Quaternion.identity;
            model.transform.localScale = Vector3.one / 3f;
            model.GetComponent<MeshRenderer>().material.color = new Color(Random.value, Random.value, Random.value);
        }

        private GameObject AssembleModel(List<GunPart> parts)
        {
            //create the actual gun model here
            return GameObject.CreatePrimitive(PrimitiveType.Cube);
        }
    }
}
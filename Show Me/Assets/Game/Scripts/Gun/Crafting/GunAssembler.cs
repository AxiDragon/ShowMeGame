using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

namespace Gunbloem
{
    public class GunAssembler : MonoBehaviour
    {
        private PlayerInventory inventory;
        private PlayerFighter fighter;
        private Workbench bench;
        [SerializeField] private Transform playerHand;
        [SerializeField] private Transform craftInventory;
        [SerializeField] private UnityEvent gunCrafted;
        [SerializeField] private Bullet bullet;
        [SerializeField] private ParticleSystem shootEffect;

        private void Awake()
        {
            inventory = GetComponentInParent<PlayerInventory>();
            fighter = inventory.GetComponent<PlayerFighter>();
            bench = GetComponent<Workbench>();
        }

        public void CraftGun()
        {
            List<GunPart> usedParts = GetUsedParts();

            if (usedParts.Count <= 0)
                return;

            AssembleGun(usedParts);
            RemoveFromInventory(usedParts);
            bench.ClearWorkbench();
            gunCrafted?.Invoke();
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
            List<Attachment> atts = bench.transform.GetComponentsInChildren<Attachment>().ToList();
            List<GunPart> usedParts = new List<GunPart>();

            for (int i = 0; i < atts.Count; i++)
                usedParts.Add(atts[i].part);

            return usedParts;
        }

        private void AssembleGun(List<GunPart> parts)
        {
            GameObject model = AssembleModel(parts);
            Gun gun = model.AddComponent<Gun>();

            float div = Mathf.Sqrt(parts.Count);
            gun.power = (from part in parts select part.power).Sum() / div;
            gun.fireRate = (from part in parts select part.fireRate).Sum() / div;
            gun.impact = (from part in parts select part.impact).Sum() / div;
            gun.speed = 10f - ((from part in parts select part.speed).Sum() - (parts.Count * 10));
            gun.speed = Mathf.Clamp(gun.speed, 3f, 25f);
            gun.bullet = bullet;
            gun.shootEffect = shootEffect;

            PlaceModelInHand(ref model);
            gun.shootTransform = GetShootTransform(model.transform); //doesn't work quite yet either
            fighter.UpdateGun(gun);
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
            OffsetModel(model.transform); //not entirely functional but good enough
        }

        private Transform GetShootTransform(Transform par)
        {
            List<GunPart> childGunParts = par.GetComponentsInChildren<GunPart>().ToList();
            List<Transform> childTransforms = new List<Transform>();
            Transform shootTransform = par.GetChild(0).transform;

            foreach (GunPart part in childGunParts)
                childTransforms.Add(part.transform);

            float farthestZ = -Mathf.Infinity;
            foreach (Transform c in childTransforms)
            {
                float zDiff = c.position.z;
                farthestZ = Mathf.Max(farthestZ, zDiff);
            }

            List<Transform> rightTransforms = new List<Transform>();

            foreach (Transform c in childTransforms)
            {
                if ((c.position.z) == farthestZ)
                    rightTransforms.Add(c);
            }

            float highestY = -Mathf.Infinity;
            foreach (Transform c in rightTransforms)
            {
                float yDiff = c.position.y - par.position.y;
                if (yDiff > highestY)
                {
                    highestY = yDiff;
                    shootTransform = c;
                }
            }

            return shootTransform;
        }

        private void OffsetModel(Transform par)
        {
            List<GunPart> childGunParts = par.GetComponentsInChildren<GunPart>().ToList();
            List<Transform> childTransforms = new List<Transform>();
            Transform holdTransform = par.GetChild(0).transform;

            foreach(GunPart part in childGunParts)
                childTransforms.Add(part.transform);

            float yOffset = Mathf.Infinity;
            foreach(Transform c in childTransforms)
            {
                float yDiff = c.position.y - par.position.y;
                yOffset = Mathf.Min(yOffset, yDiff);
            }

            List<Transform> bottomTransforms = new List<Transform>();

            foreach (Transform c in childTransforms)
            {
                if ((c.position.y - par.position.y) == yOffset)
                    bottomTransforms.Add(c);
            }

            float zOffset = Mathf.Infinity;
            foreach (Transform c in bottomTransforms)
            {
                float zDiff = c.position.z - par.position.z;
                if (zDiff < zOffset)
                {
                    zOffset = zDiff;
                    holdTransform = c;
                }
            }

            Vector3 newPos = -(holdTransform.position - par.position);
            newPos.x = 0f;
            par.GetChild(0).localPosition = newPos;
        }

        private GameObject AssembleModel(List<GunPart> parts)
        {
            GameObject gunParent = new GameObject("Gun");
            BuildModel(gunParent);
            return gunParent;
        }

        private void BuildModel(GameObject gunParent)
        {
            Attachment pivotAttachment = bench.transform.GetChild(0).GetComponent<Attachment>();
            GunPart pivotPart = Instantiate(pivotAttachment.part, gunParent.transform);

            InstantiateChildren(pivotAttachment, pivotPart.transform);
        }

        private void InstantiateChildren(Attachment parAtt, Transform gunPart)
        {
            for (int i = 0; i < parAtt.aps.Count; i++)
            {
                Transform c = parAtt.aps[i].transform;
                if (c.childCount > 0)
                {
                    AttachmentPoint ap = parAtt.aps[i];
                    Attachment cAtt = c.GetChild(0).GetComponent<Attachment>();
                    GunPart p = Instantiate(cAtt.part, gunPart);
                    ModelAttachmentPoint connectedModAp = GetConnectModelAttachmentPoint(gunPart.GetComponent<GunPart>(), ap.snappedTo);
                    ModelAttachmentPoint otherConnectedModAp = GetConnectModelAttachmentPoint(p, ap);
                    p.transform.localPosition = otherConnectedModAp.offset - connectedModAp.offset;
                    InstantiateChildren(cAtt, p.transform);
                }
            }
        }

        private ModelAttachmentPoint GetConnectModelAttachmentPoint(GunPart part, AttachmentPoint ap)
        {
            foreach (ModelAttachmentPoint modAp in part.GetComponentsInChildren<ModelAttachmentPoint>())
            {
                if (modAp.side != ap.snappedTo.side)
                    continue;

                if (modAp.id == ap.snappedTo.id)
                    return modAp;
            }

            return null;
        }
    }
}
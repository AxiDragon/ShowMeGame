using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

            if (usedParts.Count <= 0)
                return;

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
            List<Attachment> atts = bench.transform.GetComponentsInChildren<Attachment>().ToList();
            List<GunPart> usedParts = new List<GunPart>();

            for (int i = 0; i < atts.Count; i++)
                usedParts.Add(atts[i].part);

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
            model.transform.localScale = Vector3.one * 1f;
            OffsetModel(model.transform);
        }

        private void OffsetModel(Transform par)
        {
            List<GunPart> childGunParts = par.gameObject.GetComponentsInChildren<GunPart>().ToList();
            List<Transform> childTransforms = new List<Transform>();

            foreach(GunPart part in childGunParts)
                childTransforms.Add(part.transform);

            float yOffset = Mathf.Infinity;

            foreach(Transform c in childTransforms)
                yOffset = Mathf.Min(yOffset, c.localPosition.y);

            List<Transform> bottomTransforms = new List<Transform>();

            foreach (Transform c in childTransforms)
                if (c.localPosition.y == yOffset)
                    bottomTransforms.Add(c);

            float zOffset = Mathf.Infinity;
            foreach (Transform c in bottomTransforms)
                zOffset = Mathf.Min(yOffset, c.localPosition.z);

            par.GetChild(0).localPosition += new Vector3(0f, -yOffset, -zOffset);
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
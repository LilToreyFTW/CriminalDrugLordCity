using CriminalDrugLordCity.Content;
using UnityEngine;

namespace CriminalDrugLordCity.Gameplay
{
    public static class ArsenalRoomBuilder
    {
        public static void Build(ArsenalRoomDefinition room, Material material, GameObject gunPrefab)
        {
            GameObject root = new GameObject("ArsenalRoom");
            CreatePart(root.transform, "Shell", room.position.ToVector3(), room.size.ToVector3(), material, new Color(0.34f, 0.34f, 0.38f));

            foreach (PickupDefinition pickup in room.pickups)
            {
                GameObject pickupObject;
                if (gunPrefab != null)
                {
                    pickupObject = Object.Instantiate(gunPrefab, pickup.position.ToVector3(), Quaternion.identity, root.transform);
                    pickupObject.name = pickup.label;
                }
                else
                {
                    pickupObject = CreatePart(root.transform, pickup.label, pickup.position.ToVector3(), new Vector3(0.7f, 0.2f, 2.2f), material, pickup.color.ToColor());
                }

                BoxCollider collider = pickupObject.GetComponent<BoxCollider>();
                if (collider == null)
                {
                    collider = pickupObject.AddComponent<BoxCollider>();
                }
                collider.isTrigger = true;

                PickupWeapon weapon = pickupObject.AddComponent<PickupWeapon>();
                SerializedPickupName.Apply(weapon, pickup.label);
            }
        }

        private static GameObject CreatePart(Transform parent, string partName, Vector3 position, Vector3 scale, Material material, Color color)
        {
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            go.name = partName;
            go.transform.SetParent(parent, false);
            go.transform.position = position;
            go.transform.localScale = scale;

            Renderer renderer = go.GetComponent<Renderer>();
            if (renderer == null)
            {
                return go;
            }

            renderer.sharedMaterial = material != null ? material : new Material(Shader.Find("Standard"));
            renderer.sharedMaterial.color = color;
            return go;
        }
    }
}

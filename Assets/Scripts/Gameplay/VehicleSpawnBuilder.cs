using CriminalDrugLordCity.Content;
using UnityEngine;

namespace CriminalDrugLordCity.Gameplay
{
    public static class VehicleSpawnBuilder
    {
        public static void Build(VehicleSpawnDefinition[] spawns, Material material, GameObject carPrefab)
        {
            GameObject root = new GameObject("VehicleSpawns");

            foreach (VehicleSpawnDefinition spawn in spawns)
            {
                GameObject body;
                if (carPrefab != null)
                {
                    body = Object.Instantiate(carPrefab, spawn.position.ToVector3(), Quaternion.identity, root.transform);
                    body.name = spawn.archetype + "_" + spawn.id;
                }
                else
                {
                    body = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    body.name = spawn.archetype + "_" + spawn.id;
                    body.transform.SetParent(root.transform, false);
                    body.transform.position = spawn.position.ToVector3();
                    body.transform.localScale = new Vector3(4.5f, 1.4f, 8f);
                }

                BoxCollider trigger = body.GetComponent<BoxCollider>();
                if (trigger == null)
                {
                    trigger = body.AddComponent<BoxCollider>();
                }
                trigger.isTrigger = true;

                Renderer renderer = body.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.sharedMaterial = material != null ? material : new Material(Shader.Find("Standard"));
                    renderer.sharedMaterial.color = spawn.color.ToColor();
                }

                VehicleInteractable interactable = body.AddComponent<VehicleInteractable>();
                SerializedVehicleName.Apply(interactable, spawn.archetype);
            }
        }
    }
}

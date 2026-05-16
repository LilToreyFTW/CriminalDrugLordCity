using CriminalDrugLordCity.Content;
using UnityEngine;

namespace CriminalDrugLordCity.Gameplay
{
    public static class PlayerBuilder
    {
        public static void Build(float[] spawnPoint, Material material, GameObject playerPrefab, GameObject bulletPrefab, GameObject muzzleFlashPrefab)
        {
            GameObject root;
            Vector3 basePosition = spawnPoint.ToVector3();

            if (playerPrefab != null)
            {
                root = Object.Instantiate(playerPrefab, basePosition, Quaternion.identity);
                root.name = "PlayerCharacter";
            }
            else
            {
                root = new GameObject("PlayerBlockout");
                CreatePart(root.transform, "Head", basePosition + new Vector3(0f, 5.6f, 0f), new Vector3(1.1f, 1.3f, 1.1f), material, new Color(0.88f, 0.72f, 0.6f));
                CreatePart(root.transform, "Torso", basePosition + new Vector3(0f, 3.8f, 0f), new Vector3(2f, 2.4f, 1.2f), material, new Color(0.13f, 0.13f, 0.16f));
                CreatePart(root.transform, "LeftArm", basePosition + new Vector3(-1.6f, 3.8f, 0f), new Vector3(0.6f, 2.2f, 0.6f), material, new Color(0.88f, 0.72f, 0.6f));
                CreatePart(root.transform, "RightArm", basePosition + new Vector3(1.6f, 3.8f, 0f), new Vector3(0.6f, 2.2f, 0.6f), material, new Color(0.88f, 0.72f, 0.6f));
                CreatePart(root.transform, "LeftLeg", basePosition + new Vector3(-0.6f, 1.6f, 0f), new Vector3(0.7f, 2.7f, 0.7f), material, new Color(0.08f, 0.08f, 0.1f));
                CreatePart(root.transform, "RightLeg", basePosition + new Vector3(0.6f, 1.6f, 0f), new Vector3(0.7f, 2.7f, 0.7f), material, new Color(0.08f, 0.08f, 0.1f));
                CreatePart(root.transform, "LeftFoot", basePosition + new Vector3(-0.6f, 0.2f, 0.2f), new Vector3(0.8f, 0.3f, 1.2f), material, Color.black);
                CreatePart(root.transform, "RightFoot", basePosition + new Vector3(0.6f, 0.2f, 0.2f), new Vector3(0.8f, 0.3f, 1.2f), material, Color.black);
            }

            root.tag = "Player";
            CharacterController controller = root.GetComponent<CharacterController>();
            if (controller == null)
            {
                controller = root.AddComponent<CharacterController>();
            }
            controller.center = new Vector3(0f, 2.5f, 0f);
            controller.height = 5f;
            controller.radius = 0.6f;
            root.transform.position = basePosition;

            CrimPlayerController pc = root.AddComponent<CrimPlayerController>();
            pc.moveSpeed = 12f;
            pc.lookSensitivity = 2f;
            pc.bulletPrefab = bulletPrefab;
            pc.muzzleFlashPrefab = muzzleFlashPrefab;
        }

            private static void CreatePart(Transform parent, string partName, Vector3 position, Vector3 scale, Material material, Color color)
        {
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            go.name = partName;
            go.transform.SetParent(parent, false);
            go.transform.position = position;
            go.transform.localScale = scale;

            Renderer renderer = go.GetComponent<Renderer>();
            if (renderer == null)
            {
                return;
            }

            renderer.sharedMaterial = material != null ? material : new Material(Shader.Find("Standard"));
            renderer.sharedMaterial.color = color;
        }
    }
}

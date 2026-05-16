using UnityEngine;

namespace CriminalDrugLordCity.Gameplay
{
    public static class ImportedAssetSpawner
    {
        public static void BuildFallbackDistrict()
        {
            GameObject district = new GameObject("FallbackDistrict");

            for (int i = 0; i < 8; i++)
            {
                GameObject block = GameObject.CreatePrimitive(PrimitiveType.Cube);
                block.name = "BuildingBlock_" + i;
                block.transform.SetParent(district.transform, false);
                block.transform.position = new Vector3(20f + i * 12f, 6f, -24f);
                block.transform.localScale = new Vector3(8f, 12f + (i % 3) * 4f, 8f);
            }

            for (int j = 0; j < 3; j++)
            {
                GameObject shipPad = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                shipPad.name = "ShipPad_" + j;
                shipPad.transform.SetParent(district.transform, false);
                shipPad.transform.position = new Vector3(80f + j * 18f, 0.1f, 60f);
                shipPad.transform.localScale = new Vector3(7f, 0.1f, 7f);
            }
            }
            }
            }

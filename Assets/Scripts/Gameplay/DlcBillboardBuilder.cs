using CriminalDrugLordCity.Content;
using UnityEngine;

namespace CriminalDrugLordCity.Gameplay
{
    public static class DlcBillboardBuilder
    {
        public static void Build(DlcDefinition[] dlcs)
        {
            GameObject root = new GameObject("DlcPromenade");

            for (int i = 0; i < dlcs.Length; i++)
            {
                DlcDefinition dlc = dlcs[i];
                GameObject board = GameObject.CreatePrimitive(PrimitiveType.Cube);
                board.name = "Billboard_" + dlc.id;
                board.transform.SetParent(root.transform, false);
                board.transform.position = new Vector3(40f + i * 18f, 6f, 18f);
                board.transform.localScale = new Vector3(10f, 6f, 0.6f);

                DlcBillboard billboard = board.AddComponent<DlcBillboard>();
                billboard.SetContent(dlc.name, dlc.season, dlc.summary);
            }
        }
    }
}

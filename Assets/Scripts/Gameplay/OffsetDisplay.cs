using CriminalDrugLordCity.Content;
using UnityEngine;

namespace CriminalDrugLordCity.Gameplay
{
    public sealed class OffsetDisplay : MonoBehaviour
    {
        private void OnGUI()
        {
            var map = RuntimeGameState.ActiveMap;
            if (map == null || map.runtimeOffsets == null)
            {
                return;
            }

            var offsets = map.runtimeOffsets;
            
            GUI.Box(new Rect(10, 10, 300, 420), "Criminal Drug Lord City - Engine Offsets");
            int y = 40;
            int step = 20;

            DrawOffset("ComponentToWorld", offsets.componentToWorld, ref y, step);
            DrawOffset("OwningGameInstance", offsets.owningGameInstance, ref y, step);
            DrawOffset("PersistentLevel", offsets.persistentLevel, ref y, step);
            DrawOffset("LocalPlayers", offsets.localPlayers, ref y, step);
            DrawOffset("PlayerController", offsets.playerController, ref y, step);
            DrawOffset("PlayerState", offsets.playerState, ref y, step);
            DrawOffset("OwningActor", offsets.owningActor, ref y, step);
            DrawOffset("AcknowledgedPawn", offsets.acknowledgedPawn, ref y, step);
            DrawOffset("PlayerCameraManager", offsets.playerCameraManager, ref y, step);
            DrawOffset("Location", offsets.location, ref y, step);
            DrawOffset("Rotation", offsets.rotation, ref y, step);
            DrawOffset("FOV", offsets.fov, ref y, step);
            DrawOffset("Mesh", offsets.mesh, ref y, step);
            DrawOffset("RootComponent", offsets.rootComponent, ref y, step);
            
            GUI.Label(new Rect(20, y + 10, 260, 40), "Status: ACTIVE ARCHITECTURE");
        }

        private void DrawOffset(string label, int value, ref int y, int step)
        {
            GUI.Label(new Rect(20, y, 200, 20), label + ":");
            GUI.Label(new Rect(220, y, 80, 20), "0x" + value.ToString("X"));
            y += step;
        }
    }
}
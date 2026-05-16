using System;
using UnityEngine;

namespace CriminalDrugLordCity.Content
{
    [Serializable]
    public enum WeaponType
    {
        Pistol,
        SMG,
        Shotgun,
        Rifle,
        Melee
    }

    [Serializable]
    public struct PlayerLordStruct
    {
        public int level;
        public int respect;
        public float cash;
        public float health;
        public string currentRank;
    }

    [Serializable]
    public sealed class MapDefinition
    {
        public string id = "";
        public string displayName = "";
        public int width;
        public int height;
        public float terrainAmplitude;
        public float[] spawnPoint = Array.Empty<float>();
        public ArsenalRoomDefinition arsenalRoom = new ArsenalRoomDefinition();
        public VehicleSpawnDefinition[] vehicleSpawns = Array.Empty<VehicleSpawnDefinition>();
        public RuntimeOffsetDefinition runtimeOffsets = new RuntimeOffsetDefinition();
        public BuildingDefinition[] cityBuildings = Array.Empty<BuildingDefinition>();
    }

    [Serializable]
    public sealed class BuildingDefinition
    {
        public string architectureId = "";
        public float[] position = Array.Empty<float>();
        public float[] rotation = Array.Empty<float>();
        public float scale = 1.0f;
    }

    [Serializable]
    public sealed class ArsenalRoomDefinition
    {
        public float[] position = Array.Empty<float>();
        public float[] size = Array.Empty<float>();
        public PickupDefinition[] pickups = Array.Empty<PickupDefinition>();
    }

    [Serializable]
    public sealed class PickupDefinition
    {
        public string id = "";
        public string label = "";
        public WeaponType type = WeaponType.Pistol;
        public float[] position = Array.Empty<float>();
        public float[] color = Array.Empty<float>();
    }

    [Serializable]
    public sealed class VehicleSpawnDefinition
    {
        public string id = "";
        public string archetype = "";
        public float[] position = Array.Empty<float>();
        public float[] color = Array.Empty<float>();
    }

    [Serializable]
    public sealed class DlcDefinition
    {
        public string id = "";
        public string name = "";
        public string season = "";
        public string summary = "";
        public string[] addsMaps = Array.Empty<string>();
        public string[] addsWeapons = Array.Empty<string>();
        public string[] dateOfRelease = Array.Empty<string>();
    }

    [Serializable]
    public sealed class RuntimeOffsetDefinition
    {
        public int componentToWorld;
        public int owningGameInstance;
        public int persistentLevel;
        public int localPlayers;
        public int playerController;
        public int playerState;
        public int playerNamePrivate;
        public int owningActor;
        public int maxPacket;
        public int acknowledgedPawn;
        public int playerCameraManager;
        public int cameraCachePrivate;
        public int pov;
        public int location;
        public int rotation;
        public int fov;
        public int mesh;
        public int rootComponent;
        public int relativeLocation;
        public int componentVelocity;
    }

    public static class VectorArrayExtensions
    {
        public static Vector3 ToVector3(this float[] values)
        {
            if (values == null || values.Length < 3)
            {
                return Vector3.zero;
            }

            return new Vector3(values[0], values[1], values[2]);
        }

        public static Color ToColor(this float[] values)
        {
            if (values == null || values.Length < 3)
            {
                return Color.white;
            }

            return new Color(values[0], values[1], values[2], 1f);
        }
    }
}

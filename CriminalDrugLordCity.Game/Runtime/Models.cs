using OpenTK.Mathematics;

namespace CriminalDrugLordCity.Game.Runtime;

internal sealed record MapDefinition(
    string Id,
    string DisplayName,
    int Width,
    int Height,
    float TerrainAmplitude,
    Vector3 SpawnPoint,
    ArsenalRoomDefinition ArsenalRoom,
    List<VehicleSpawnDefinition> VehicleSpawns);

internal sealed record ArsenalRoomDefinition(Vector3 Position, Vector3 Size, List<PickupDefinition> Pickups);

internal sealed record PickupDefinition(string Id, string Label, Vector3 Position, Vector3 Color);

internal sealed record VehicleSpawnDefinition(string Id, string Archetype, Vector3 Position, Vector3 Color);

internal sealed record DlcDefinition(string Id, string Name, string Season, string Summary, List<string> AddsMaps, List<string> AddsWeapons);

internal sealed record CameraState(float Yaw, float Pitch, bool ThirdPerson);

internal sealed class PlayerState
{
    public Vector3 Position { get; set; } = new(32f, 8f, 32f);
    public float Yaw { get; set; } = 0f;
    public float Pitch { get; set; } = -0.2f;
    public bool ThirdPerson { get; set; } = true;
    public string? EquippedWeapon { get; set; }
}

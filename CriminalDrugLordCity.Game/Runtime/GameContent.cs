using System.Text.Json;
using OpenTK.Mathematics;

namespace CriminalDrugLordCity.Game.Runtime;

internal sealed class GameContent
{
    public required MapDefinition Map { get; init; }
    public required List<DlcDefinition> Dlcs { get; init; }

    public static GameContent Load(string root)
    {
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var mapPath = Path.Combine(root, "maps", "metro_city_4096x2056.json");
        var dlcPath = Path.Combine(root, "dlc", "season_manifest.json");

        using var mapStream = File.OpenRead(mapPath);
        using var dlcStream = File.OpenRead(dlcPath);

        var mapDto = JsonSerializer.Deserialize<MapDto>(mapStream, options) ?? throw new InvalidOperationException("Map content failed to load.");
        var dlcDto = JsonSerializer.Deserialize<List<DlcDto>>(dlcStream, options) ?? throw new InvalidOperationException("DLC content failed to load.");

        return new GameContent
        {
            Map = mapDto.ToDefinition(),
            Dlcs = dlcDto.Select(x => x.ToDefinition()).ToList()
        };
    }

    private sealed class MapDto
    {
        public string Id { get; set; } = "";
        public string DisplayName { get; set; } = "";
        public int Width { get; set; }
        public int Height { get; set; }
        public float TerrainAmplitude { get; set; }
        public float[] SpawnPoint { get; set; } = [];
        public ArsenalRoomDto ArsenalRoom { get; set; } = new();
        public List<VehicleSpawnDto> VehicleSpawns { get; set; } = [];

        public MapDefinition ToDefinition() => new(
            Id,
            DisplayName,
            Width,
            Height,
            TerrainAmplitude,
            ToVector3(SpawnPoint),
            ArsenalRoom.ToDefinition(),
            VehicleSpawns.Select(x => x.ToDefinition()).ToList());
    }

    private sealed class ArsenalRoomDto
    {
        public float[] Position { get; set; } = [];
        public float[] Size { get; set; } = [];
        public List<PickupDto> Pickups { get; set; } = [];

        public ArsenalRoomDefinition ToDefinition() => new(ToVector3(Position), ToVector3(Size), Pickups.Select(x => x.ToDefinition()).ToList());
    }

    private sealed class PickupDto
    {
        public string Id { get; set; } = "";
        public string Label { get; set; } = "";
        public float[] Position { get; set; } = [];
        public float[] Color { get; set; } = [];

        public PickupDefinition ToDefinition() => new(Id, Label, ToVector3(Position), ToVector3(Color));
    }

    private sealed class VehicleSpawnDto
    {
        public string Id { get; set; } = "";
        public string Archetype { get; set; } = "";
        public float[] Position { get; set; } = [];
        public float[] Color { get; set; } = [];

        public VehicleSpawnDefinition ToDefinition() => new(Id, Archetype, ToVector3(Position), ToVector3(Color));
    }

    private sealed class DlcDto
    {
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";
        public string Season { get; set; } = "";
        public string Summary { get; set; } = "";
        public List<string> AddsMaps { get; set; } = [];
        public List<string> AddsWeapons { get; set; } = [];

        public DlcDefinition ToDefinition() => new(Id, Name, Season, Summary, AddsMaps, AddsWeapons);
    }

    private static Vector3 ToVector3(float[] values) => new(values[0], values[1], values[2]);
}

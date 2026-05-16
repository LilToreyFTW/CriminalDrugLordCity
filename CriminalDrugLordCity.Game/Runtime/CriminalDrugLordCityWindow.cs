using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace CriminalDrugLordCity.Game.Runtime;

internal sealed class CriminalDrugLordCityWindow : GameWindow
{
    private readonly PlayerState _player = new();
    private readonly List<VehicleSpawnDefinition> _vehicles = [];
    private GameContent _content = null!;
    private SimpleRenderer _renderer = null!;
    private double _time;

    public CriminalDrugLordCityWindow(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
        : base(gameWindowSettings, nativeWindowSettings)
    {
    }

    protected override void OnLoad()
    {
        base.OnLoad();
        CursorState = CursorState.Grabbed;
        GL.ClearColor(0.69f, 0.82f, 0.94f, 1f);
        GL.Enable(EnableCap.DepthTest);
        _content = GameContent.Load(Path.Combine(AppContext.BaseDirectory, "Content"));
        _player.Position = _content.Map.SpawnPoint;
        _vehicles.AddRange(_content.Map.VehicleSpawns);
        _renderer = new SimpleRenderer(Size.X, Size.Y);
    }

    protected override void OnResize(ResizeEventArgs e)
    {
        base.OnResize(e);
        GL.Viewport(0, 0, e.Width, e.Height);
        _renderer.Resize(e.Width, e.Height);
    }

    protected override void OnUpdateFrame(FrameEventArgs args)
    {
        base.OnUpdateFrame(args);
        _time += args.Time;
        var input = KeyboardState;
        if (input.IsKeyPressed(Keys.Escape))
        {
            Close();
        }

        if (input.IsKeyPressed(Keys.V))
        {
            _player.ThirdPerson = !_player.ThirdPerson;
        }

        var move = Vector3.Zero;
        if (input.IsKeyDown(Keys.W)) move.Z -= 1f;
        if (input.IsKeyDown(Keys.S)) move.Z += 1f;
        if (input.IsKeyDown(Keys.A)) move.X -= 1f;
        if (input.IsKeyDown(Keys.D)) move.X += 1f;
        if (input.IsKeyDown(Keys.Space)) move.Y += 1f;
        if (input.IsKeyDown(Keys.LeftShift)) move.Y -= 1f;

        if (move.LengthSquared > 0f)
        {
            move = Vector3.Normalize(move);
            var forward = new Vector3(MathF.Sin(_player.Yaw), 0f, MathF.Cos(_player.Yaw));
            var right = new Vector3(forward.Z, 0f, -forward.X);
            _player.Position += (forward * -move.Z + right * move.X + Vector3.UnitY * move.Y) * (float)args.Time * 18f;
        }

        var mouse = MouseState.Delta;
        _player.Yaw -= mouse.X * 0.0025f;
        _player.Pitch = Math.Clamp(_player.Pitch - mouse.Y * 0.0025f, -1.3f, 1.1f);

        if (input.IsKeyPressed(Keys.E))
        {
            var pickup = _content.Map.ArsenalRoom.Pickups.FirstOrDefault(x => Vector3.Distance(x.Position, _player.Position) < 4f);
            if (pickup is not null)
            {
                _player.EquippedWeapon = pickup.Label;
                Title = $"Criminal Drug Lord City | Equipped: {pickup.Label}";
            }
        }
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        base.OnRenderFrame(args);
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        var eye = _player.Position + (_player.ThirdPerson ? new Vector3(-9f * MathF.Sin(_player.Yaw), 4.5f, -9f * MathF.Cos(_player.Yaw)) : new Vector3(0f, 2f, 0f));
        var lookDir = new Vector3(
            MathF.Sin(_player.Yaw) * MathF.Cos(_player.Pitch),
            MathF.Sin(_player.Pitch),
            MathF.Cos(_player.Yaw) * MathF.Cos(_player.Pitch));

        var view = Matrix4.LookAt(eye, _player.Position + new Vector3(0f, 2f, 0f) + lookDir * 15f, Vector3.UnitY);
        _renderer.Begin(view);
        _renderer.DrawTerrain(_content.Map.Width, _content.Map.Height, _content.Map.TerrainAmplitude);
        _renderer.DrawArsenalRoom(_content.Map.ArsenalRoom);

        foreach (var pickup in _content.Map.ArsenalRoom.Pickups)
        {
            var bob = 0.35f * MathF.Sin((float)_time * 2.5f + pickup.Position.X);
            _renderer.DrawPickup(pickup.Position + new Vector3(0f, bob, 0f), pickup.Color);
        }

        foreach (var vehicle in _vehicles)
        {
            _renderer.DrawVehicle(vehicle.Position, vehicle.Color);
        }

        _renderer.DrawHumanoid(_player.Position, _player.Yaw, new Vector3(0.88f, 0.72f, 0.6f), new Vector3(0.12f, 0.12f, 0.14f));
        _renderer.End();
        SwapBuffers();
    }

    protected override void OnUnload()
    {
        base.OnUnload();
        _renderer.Dispose();
    }
}

using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace CriminalDrugLordCity.Game.Runtime;

internal sealed class SimpleRenderer : IDisposable
{
    private readonly BasicShader _shader;
    private Matrix4 _projection;
    private Matrix4 _view;

    public SimpleRenderer(int width, int height)
    {
        _shader = new BasicShader();
        Resize(width, height);
    }

    public void Resize(int width, int height)
    {
        _projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(65f), width / (float)height, 0.1f, 5000f);
    }

    public void Begin(Matrix4 view)
    {
        _view = view;
        _shader.Use();
        _shader.SetMatrix("uProjection", _projection);
        _shader.SetMatrix("uView", _view);
    }

    public void DrawTerrain(int width, int height, float amplitude)
    {
        for (var x = 0; x < width; x += 96)
        {
            for (var z = 0; z < height; z += 96)
            {
                var world = new Vector3(x - width / 2f, Noise(x, z, amplitude), z - height / 2f);
                var scale = new Vector3(96f, 2f, 96f);
                DrawCube(world, scale, new Vector3(0.18f, 0.42f, 0.2f));
            }
        }
    }

    public void DrawArsenalRoom(ArsenalRoomDefinition room)
    {
        DrawCube(room.Position, room.Size, new Vector3(0.32f, 0.32f, 0.36f));
        DrawCube(room.Position + new Vector3(0f, room.Size.Y * 0.6f, -room.Size.Z * 0.35f), new Vector3(room.Size.X * 0.8f, 1f, 2f), new Vector3(0.5f, 0.2f, 0.18f));
    }

    public void DrawPickup(Vector3 position, Vector3 color) => DrawCube(position, new Vector3(0.7f, 0.2f, 2.2f), color);

    public void DrawVehicle(Vector3 position, Vector3 color)
    {
        DrawCube(position, new Vector3(4.5f, 1.4f, 8f), color);
        DrawCube(position + new Vector3(0f, 1.2f, -0.5f), new Vector3(3f, 1.3f, 3.5f), color * 0.85f);
    }

    public void DrawHumanoid(Vector3 position, float yaw, Vector3 skin, Vector3 clothing)
    {
        var rotation = Matrix4.CreateRotationY(yaw);
        DrawCube(Transform(position, rotation, new Vector3(0f, 5.6f, 0f)), new Vector3(1.1f, 1.3f, 1.1f), skin);
        DrawCube(Transform(position, rotation, new Vector3(0f, 3.8f, 0f)), new Vector3(2f, 2.4f, 1.2f), clothing);
        DrawCube(Transform(position, rotation, new Vector3(-1.6f, 3.8f, 0f)), new Vector3(0.6f, 2.2f, 0.6f), skin);
        DrawCube(Transform(position, rotation, new Vector3(1.6f, 3.8f, 0f)), new Vector3(0.6f, 2.2f, 0.6f), skin);
        DrawCube(Transform(position, rotation, new Vector3(-0.6f, 1.6f, 0f)), new Vector3(0.7f, 2.7f, 0.7f), clothing * 0.8f);
        DrawCube(Transform(position, rotation, new Vector3(0.6f, 1.6f, 0f)), new Vector3(0.7f, 2.7f, 0.7f), clothing * 0.8f);
        DrawCube(Transform(position, rotation, new Vector3(-0.6f, 0.2f, 0.2f)), new Vector3(0.8f, 0.3f, 1.2f), new Vector3(0.08f, 0.08f, 0.08f));
        DrawCube(Transform(position, rotation, new Vector3(0.6f, 0.2f, 0.2f)), new Vector3(0.8f, 0.3f, 1.2f), new Vector3(0.08f, 0.08f, 0.08f));
    }

    public void End()
    {
    }

    private void DrawCube(Vector3 position, Vector3 scale, Vector3 color)
    {
        var model = Matrix4.CreateScale(scale) * Matrix4.CreateTranslation(position);
        _shader.SetMatrix("uModel", model);
        _shader.SetVector("uColor", color);
        CubeMesh.Instance.Draw();
    }

    private static Vector3 Transform(Vector3 basePosition, Matrix4 rotation, Vector3 localOffset)
    {
        return Vector3.TransformPosition(localOffset, rotation) + basePosition;
    }

    private static float Noise(int x, int z, float amplitude)
    {
        return (MathF.Sin(x * 0.015f) + MathF.Cos(z * 0.011f) + MathF.Sin((x + z) * 0.006f)) * amplitude;
    }

    public void Dispose()
    {
        _shader.Dispose();
        CubeMesh.Instance.Dispose();
    }
}

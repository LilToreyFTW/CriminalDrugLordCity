using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace CriminalDrugLordCity.Game.Runtime;

internal sealed class BasicShader : IDisposable
{
    private readonly int _handle;

    public BasicShader()
    {
        const string vertex = """
        #version 330 core
        layout (location = 0) in vec3 aPosition;
        uniform mat4 uModel;
        uniform mat4 uView;
        uniform mat4 uProjection;
        void main()
        {
            gl_Position = uProjection * uView * uModel * vec4(aPosition, 1.0);
        }
        """;

        const string fragment = """
        #version 330 core
        out vec4 FragColor;
        uniform vec3 uColor;
        void main()
        {
            FragColor = vec4(uColor, 1.0);
        }
        """;

        var vs = CompileShader(ShaderType.VertexShader, vertex);
        var fs = CompileShader(ShaderType.FragmentShader, fragment);
        _handle = GL.CreateProgram();
        GL.AttachShader(_handle, vs);
        GL.AttachShader(_handle, fs);
        GL.LinkProgram(_handle);
        GL.GetProgram(_handle, GetProgramParameterName.LinkStatus, out var linked);
        if (linked == 0)
        {
            throw new InvalidOperationException(GL.GetProgramInfoLog(_handle));
        }

        GL.DetachShader(_handle, vs);
        GL.DetachShader(_handle, fs);
        GL.DeleteShader(vs);
        GL.DeleteShader(fs);
    }

    public void Use() => GL.UseProgram(_handle);

    public void SetMatrix(string name, Matrix4 value) => GL.UniformMatrix4(GL.GetUniformLocation(_handle, name), false, ref value);

    public void SetVector(string name, Vector3 value) => GL.Uniform3(GL.GetUniformLocation(_handle, name), value);

    public void Dispose() => GL.DeleteProgram(_handle);

    private static int CompileShader(ShaderType type, string source)
    {
        var handle = GL.CreateShader(type);
        GL.ShaderSource(handle, source);
        GL.CompileShader(handle);
        GL.GetShader(handle, ShaderParameter.CompileStatus, out var compiled);
        if (compiled == 0)
        {
            throw new InvalidOperationException(GL.GetShaderInfoLog(handle));
        }

        return handle;
    }
}

internal sealed class CubeMesh : IDisposable
{
    private readonly int _vao;
    private readonly int _vbo;
    private readonly float[] _vertices =
    [
        -0.5f,-0.5f,-0.5f,  0.5f,-0.5f,-0.5f,  0.5f, 0.5f,-0.5f,
         0.5f, 0.5f,-0.5f, -0.5f, 0.5f,-0.5f, -0.5f,-0.5f,-0.5f,
        -0.5f,-0.5f, 0.5f,  0.5f,-0.5f, 0.5f,  0.5f, 0.5f, 0.5f,
         0.5f, 0.5f, 0.5f, -0.5f, 0.5f, 0.5f, -0.5f,-0.5f, 0.5f,
        -0.5f, 0.5f, 0.5f, -0.5f, 0.5f,-0.5f, -0.5f,-0.5f,-0.5f,
        -0.5f,-0.5f,-0.5f, -0.5f,-0.5f, 0.5f, -0.5f, 0.5f, 0.5f,
         0.5f, 0.5f, 0.5f,  0.5f, 0.5f,-0.5f,  0.5f,-0.5f,-0.5f,
         0.5f,-0.5f,-0.5f,  0.5f,-0.5f, 0.5f,  0.5f, 0.5f, 0.5f,
        -0.5f,-0.5f,-0.5f,  0.5f,-0.5f,-0.5f,  0.5f,-0.5f, 0.5f,
         0.5f,-0.5f, 0.5f, -0.5f,-0.5f, 0.5f, -0.5f,-0.5f,-0.5f,
        -0.5f, 0.5f,-0.5f,  0.5f, 0.5f,-0.5f,  0.5f, 0.5f, 0.5f,
         0.5f, 0.5f, 0.5f, -0.5f, 0.5f, 0.5f, -0.5f, 0.5f,-0.5f
    ];

    public static CubeMesh Instance { get; } = new();

    private CubeMesh()
    {
        _vao = GL.GenVertexArray();
        _vbo = GL.GenBuffer();
        GL.BindVertexArray(_vao);
        GL.BindBuffer(BufferTarget.ArrayBuffer, _vbo);
        GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);
    }

    public void Draw()
    {
        GL.BindVertexArray(_vao);
        GL.DrawArrays(PrimitiveType.Triangles, 0, 36);
    }

    public void Dispose()
    {
        GL.DeleteBuffer(_vbo);
        GL.DeleteVertexArray(_vao);
    }
}

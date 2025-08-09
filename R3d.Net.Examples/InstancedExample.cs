using R3d.Net.Types;
using Raylib_cs;
using System.Numerics;

namespace R3d.Net.Examples
{
    /// <summary>
    /// Instanced example
    /// </summary>
    internal class InstancedExample : IExample
    {
        private const int InstanceCount = 1000;

        private Types.Mesh _mesh;
        private Types.Material _material;

        private Camera3D _camera;

        private readonly Matrix4x4[] _transforms = new Matrix4x4[InstanceCount];
        private readonly Color[] _colors = new Color[InstanceCount];

        /// <inheritdoc/>
        public void Init(int width, int height)
        {
            R3d.Init(width, height, ConfigFlag.None);
            Raylib.SetTargetFPS(60);

            _mesh = R3d.GenMeshCube(1, 1, 1, true);
            _material = R3d.GetDefaultMaterial();

            for (var i = 0; i < InstanceCount; i++)
            {
                var translate = Raymath.MatrixTranslate(
                    (float)Raylib.GetRandomValue(-50000, 50000) / 1000,
                    (float)Raylib.GetRandomValue(-50000, 50000) / 1000,
                    (float)Raylib.GetRandomValue(-50000, 50000) / 1000
                );

                var rotate = Raymath.MatrixRotateXYZ(new Vector3(
                    (float)Raylib.GetRandomValue(-314000, 314000) / 100000,
                    (float)Raylib.GetRandomValue(-314000, 314000) / 100000,
                    (float)Raylib.GetRandomValue(-314000, 314000) / 100000
                ));

                var scale = Raymath.MatrixScale(
                    (float)Raylib.GetRandomValue(100, 2000) / 1000,
                    (float)Raylib.GetRandomValue(100, 2000) / 1000,
                    (float)Raylib.GetRandomValue(100, 2000) / 1000
                );

                _transforms[i] = Raymath.MatrixMultiply(Raymath.MatrixMultiply(scale, rotate), translate);
                _colors[i] = Raylib.ColorFromHSV((float)Raylib.GetRandomValue(0, 360000) / 1000, 1.0f, 1.0f);
            }

            _camera.Position = new Vector3(0, 2, 2);
            _camera.Target = Vector3.Zero;
            _camera.Up = Vector3.UnitY;
            _camera.FovY = 60;

            var light = R3d.CreateLight(LightType.Directional);
            R3d.SetLightDirection(light, new Vector3(0, -1, 0));
            R3d.SetLightActive(light, true);

            Raylib.DisableCursor();
        }

        /// <inheritdoc/>
        public void Update(float deltaTime)
        {
            Raylib.UpdateCamera(ref _camera, CameraMode.Free);
        }

        /// <inheritdoc/>
        public void Render()
        {
            R3d.Begin(_camera);

            R3d.DrawMeshInstancedEx(ref _mesh, ref _material, _transforms, _colors, InstanceCount);

            R3d.End();
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            R3d.UnloadMesh(ref _mesh);
            R3d.UnloadMaterial(ref _material);

            R3d.Close();
        }
    }
}

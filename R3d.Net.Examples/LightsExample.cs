using R3d.Net.Types;
using Raylib_cs;
using System.Numerics;

namespace R3d.Net.Examples
{
    /// <summary>
    /// Many lights example
    /// </summary>
    internal class LightsExample : IExample
    {
        private const int InstanceCount = 10000;
        private const int LightCount = 100;

        private int _height;

        private Types.Mesh _plane;
        private Types.Mesh _sphere;
        private Types.Material _material;

        private Camera3D _camera;

        private readonly Matrix4x4[] _transforms = new Matrix4x4[InstanceCount];
        private readonly uint[] _lights = new uint[LightCount];

        /// <inheritdoc/>
        public void Init(int width, int height)
        {
            _height = height;

            R3d.Init(width, height, ConfigFlag.None);
            Raylib.SetTargetFPS(60);

            _plane = R3d.GenMeshPlane(1000, 1000, 1, 1, true);
            _sphere = R3d.GenMeshSphere(0.35f, 16, 16, true);
            _material = R3d.GetDefaultMaterial();

            _camera.Position = new Vector3(0, 2, 2);
            _camera.Target = Vector3.Zero;
            _camera.Up = Vector3.UnitY;
            _camera.FovY = 60;

            for (var x = -50; x < 50; x++)
            {
                for (var z = -50; z < 50; z++)
                {
                    int index = (z + 50) * 100 + x + 50;
                    _transforms[index] = Raymath.MatrixTranslate(x, 0, z);
                }
            }

            for (var x = -5; x < 5; x++)
            {
                for (var z = -5; z < 5; z++)
                {
                    int index = (z + 5) * 10 + x + 5;

                    _lights[index] = R3d.CreateLight(LightType.Omnidirectional);
                    R3d.SetLightPosition(_lights[index], new Vector3(x * 10, 10, z * 10));
                    R3d.SetLightColor(_lights[index], Raylib.ColorFromHSV((float)index / LightCount * 360, 1.0f, 1.0f));
                    R3d.SetLightRange(_lights[index], 20.0f);
                    R3d.SetLightActive(_lights[index], true);
                }
            }
        }

        /// <inheritdoc/>
        public void Update(float deltaTime)
        {
            Raylib.UpdateCamera(ref _camera, CameraMode.Orbital);
        }

        /// <inheritdoc/>
        public void Render()
        {
            R3d.Begin(_camera);

            R3d.DrawMesh(ref _plane, ref _material, Raymath.MatrixTranslate(0, -0.5f, 0));
            R3d.DrawMeshInstanced(ref _sphere, ref _material, _transforms, InstanceCount);

            R3d.End();

            if (Raylib.IsKeyDown(KeyboardKey.Space))
            {
                Raylib.BeginMode3D(_camera);

                for (var i = 0; i < LightCount; i++)
                {
                    R3d.DrawLightShape(_lights[i]);
                }

                Raylib.EndMode3D();
            }

            Raylib.DrawText("Press SPACE to show the lights", 10, _height - 34, 24, Color.Black);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            R3d.UnloadMesh(ref _plane);
            R3d.UnloadMesh(ref _sphere);
            R3d.UnloadMaterial(ref _material);

            R3d.Close();
        }
    }
}

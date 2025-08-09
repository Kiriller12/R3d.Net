using R3d.Net.Types;
using Raylib_cs;
using System.Numerics;

namespace R3d.Net.Examples
{
    /// <summary>
    /// Directional light example
    /// </summary>
    internal class DirectionalExample : IExample
    {
        private const int InstanceCount = 10000;

        private Types.Mesh _plane;
        private Types.Mesh _sphere;
        private Types.Material _material;

        private Camera3D _camera;

        private readonly Matrix4x4[] _transforms = new Matrix4x4[InstanceCount];

        /// <inheritdoc/>
        public void Init(int width, int height)
        {
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
                    _transforms[index] = Raymath.MatrixTranslate(x * 2, 0, z * 2);
                }
            }

            var light = R3d.CreateLight(LightType.Directional);
            R3d.SetLightDirection(light, new Vector3(0, -1, -1));
            R3d.SetShadowUpdateMode(light, ShadowUpdateMode.Manual);
            R3d.SetShadowBias(light, 0.005f);
            R3d.EnableShadow(light, 4096);
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

            R3d.DrawMesh(ref _plane, ref _material, Raymath.MatrixTranslate(0, -0.5f, 0));
            R3d.DrawMeshInstanced(ref _sphere, ref _material, _transforms, InstanceCount);

            R3d.End();
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

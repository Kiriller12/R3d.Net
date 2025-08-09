using R3d.Net.Types;
using Raylib_cs;
using System.Numerics;

namespace R3d.Net.Examples
{
    /// <summary>
    /// Basic example
    /// </summary>
    internal class BasicExample : IExample
    {
        private Types.Mesh _plane;
        private Types.Mesh _sphere;
        private Types.Material _material;

        private Camera3D _camera;

        /// <inheritdoc/>
        public void Init(int width, int height)
        {
            R3d.Init(width, height, ConfigFlag.None);
            Raylib.SetTargetFPS(60);

            _plane = R3d.GenMeshPlane(1000, 1000, 1, 1, true);
            _sphere = R3d.GenMeshSphere(0.5f, 64, 64, true);
            _material = R3d.GetDefaultMaterial();

            _camera.Position = new Vector3(0, 2, 2);
            _camera.Target = Vector3.Zero;
            _camera.Up = Vector3.UnitY;
            _camera.FovY = 60;

            var light = R3d.CreateLight(LightType.Spot);
            R3d.LightLookAt(light, new Vector3(0, 10, 5), Vector3.Zero);
            R3d.EnableShadow(light, 4096);
            R3d.SetLightActive(light, true);
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
            R3d.DrawMesh(ref _sphere, ref _material, Raymath.MatrixIdentity());

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

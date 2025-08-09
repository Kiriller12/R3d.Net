using R3d.Net.Types;
using Raylib_cs;
using System.Numerics;

namespace R3d.Net.Examples
{
    /// <summary>
    /// Transparency example
    /// </summary>
    internal unsafe class TransparencyExample : IExample
    {
        private Types.Model _cube;
        private Types.Model _plane;
        private Types.Model _sphere;

        private Camera3D _camera;

        /// <inheritdoc/>
        public void Init(int width, int height)
        {
            R3d.Init(width, height, ConfigFlag.None);
            Raylib.SetTargetFPS(60);

            /* --- Load cube model --- */

            var mesh = R3d.GenMeshCube(1, 1, 1, true);
            _cube = R3d.LoadModelFromMesh(ref mesh);

            _cube.Materials[0].Albedo.Color = new Color(100, 100, 255, 100);
            _cube.Materials[0].Orm.Occlusion = 1.0f;
            _cube.Materials[0].Orm.Roughness = 0.2f;
            _cube.Materials[0].Orm.Metalness = 0.2f;

            _cube.Materials[0].BlendMode = BlendMode3D.Alpha;
            _cube.Materials[0].ShadowCastMode = ShadowCastMode.Disabled;

            /* --- Load plane model --- */

            mesh = R3d.GenMeshPlane(1000, 1000, 1, 1, true);
            _plane = R3d.LoadModelFromMesh(ref mesh);

            _plane.Materials[0].Orm.Occlusion = 1.0f;
            _plane.Materials[0].Orm.Roughness = 1.0f;
            _plane.Materials[0].Orm.Metalness = 0.0f;

            /* --- Load sphere model --- */

            mesh = R3d.GenMeshSphere(0.5f, 64, 64, true);
            _sphere = R3d.LoadModelFromMesh(ref mesh);

            _sphere.Materials[0].Orm.Occlusion = 1.0f;
            _sphere.Materials[0].Orm.Roughness = 0.25f;
            _sphere.Materials[0].Orm.Metalness = 0.75f;

            /* --- Configure the camera --- */

            _camera.Position = new Vector3(0, 2, 2);
            _camera.Target = Vector3.Zero;
            _camera.Up = Vector3.UnitY;
            _camera.FovY = 60;

            /* --- Configure lighting --- */

            var light = R3d.CreateLight(LightType.Spot);
            R3d.LightLookAt(light, new Vector3(0, 10, 5), Vector3.Zero);
            R3d.SetLightActive(light, true);
            R3d.EnableShadow(light, 4096);
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

            R3d.DrawModel(ref _plane, new Vector3(0, -0.5f, 0), 1.0f);
            R3d.DrawModel(ref _sphere, Vector3.Zero, 1.0f);
            R3d.DrawModel(ref _cube, Vector3.Zero, 1.0f);

            R3d.End();
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            R3d.UnloadModel(ref _cube, true);
            R3d.UnloadModel(ref _plane, true);
            R3d.UnloadModel(ref _sphere, true);

            R3d.Close();
        }
    }
}

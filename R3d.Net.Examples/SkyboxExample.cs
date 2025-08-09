using R3d.Net.Types;
using Raylib_cs;
using System.Numerics;

namespace R3d.Net.Examples
{
    /// <summary>
    /// Skybox example
    /// </summary>
    internal class SkyboxExample : IExample
    {
        const int Dimension = 7;

        private Types.Mesh _sphere;
        private Skybox _skybox;

        private Camera3D _camera;

        private readonly Types.Material[] _materials = new Types.Material[Dimension * Dimension];

        /// <inheritdoc/>
        public void Init(int width, int height)
        {
            R3d.Init(width, height, ConfigFlag.None);
            Raylib.SetTargetFPS(60);

            _sphere = R3d.GenMeshSphere(0.5f, 64, 64, true);

            for (var x = 0; x < Dimension; x++)
            {
                for (var y = 0; y < Dimension; y++)
                {
                    var i = y * Dimension + x;

                    _materials[i] = R3d.GetDefaultMaterial();
                    _materials[i].Orm.Metalness = (float)x / Dimension;
                    _materials[i].Orm.Roughness = (float)y / Dimension;
                    _materials[i].Albedo.Color = Raylib.ColorFromHSV((float)x / Dimension * 360, 1, 1);
                }
            }

            _skybox = R3d.LoadSkybox("Resources/sky/skybox1.png", CubemapLayout.AutoDetect);
            R3d.EnableSkybox(_skybox);

            _camera.Position = new Vector3(0, 0, 6);
            _camera.Target = Vector3.Zero;
            _camera.Up = Vector3.UnitY;
            _camera.FovY = 60;

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

            for (var x = 0; x < Dimension; x++)
            {
                for (var y = 0; y < Dimension; y++)
                {
                    R3d.DrawMesh(ref _sphere, ref _materials[y * Dimension + x], Raymath.MatrixTranslate(x - 3, y - 3, 0.0f));
                }
            }

            R3d.End();
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            R3d.UnloadMesh(ref _sphere);
            R3d.UnloadSkybox(_skybox);

            R3d.Close();
        }
    }
}

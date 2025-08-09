using R3d.Net.Types;
using Raylib_cs;
using System.Numerics;

namespace R3d.Net.Examples
{
    /// <summary>
    /// PBR car example
    /// </summary>
    internal class PbrCarExample : IExample
    {
        private Types.Model _model;
        private Types.Mesh _ground;
        private Types.Material _groundMat;
        private Skybox _skybox;

        private Camera3D _camera;

        private bool _showSkybox = true;

        /// <inheritdoc/>
        public void Init(int width, int height)
        {
            R3d.Init(width, height, ConfigFlag.TransparentSorting | ConfigFlag.Fxaa);
            Raylib.SetTargetFPS(60);
            Raylib.DisableCursor();

            R3d.SetBackgroundColor(Color.Black);
            R3d.SetAmbientColor(Color.DarkGray);

            R3d.SetSSAO(true);
            R3d.SetSSAORadius(2.0f);
            R3d.SetBloomIntensity(0.1f);
            R3d.SetBloomMode(Bloom.Mix);
            R3d.SetTonemapMode(Tonemap.Aces);

            R3d.SetModelImportScale(0.01f);
            _model = R3d.LoadModel("Resources/PBR/car.glb");
            _ground = R3d.GenMeshPlane(100.0f, 100.0f, 1, 1, true);

            _groundMat = R3d.GetDefaultMaterial();
            _groundMat.Albedo.Color = new Color(0, 31, 7, 255);

            _skybox = R3d.LoadSkybox("Resources/Sky/skybox3.png", CubemapLayout.AutoDetect);
            R3d.EnableSkybox(_skybox);

            _camera.Position = new Vector3(0, 0, 5);
            _camera.Target = Vector3.Zero;
            _camera.Up = Vector3.UnitY;
            _camera.FovY = 60;

            R3d.SetSceneBounds(new BoundingBox(new Vector3(-10, -10, -10), new Vector3(10, 10, 10)));

            var light = R3d.CreateLight(LightType.Directional);
            R3d.SetLightDirection(light, -Vector3.One);
            R3d.EnableShadow(light, 4096);
            R3d.SetLightActive(light, true);
        }

        /// <inheritdoc/>
        public void Update(float deltaTime)
        {
            Raylib.UpdateCamera(ref _camera, CameraMode.Free);

            if (Raylib.IsKeyPressed(KeyboardKey.O))
            {
                R3d.SetSSAO(!R3d.GetSSAO());
            }

            if (Raylib.IsKeyPressed(KeyboardKey.T))
            {
                _showSkybox = !_showSkybox;

                if (_showSkybox)
                {
                    R3d.EnableSkybox(_skybox);
                }
                else
                {
                    R3d.DisableSkybox();
                }
            }
        }

        /// <inheritdoc/>
        public void Render()
        {
            R3d.Begin(_camera);

            R3d.DrawMesh(ref _ground, ref _groundMat, Raymath.MatrixTranslate(0.0f, -0.4f, 0.0f));
            R3d.DrawModel(ref _model, Vector3.Zero, 1.0f);

            R3d.End();

            Raylib.DrawText("Press O to toggle SSAO", 10, 44, 24, Color.Lime);
            Raylib.DrawText("Press T to toggle the skybox", 10, 74, 24, Color.Lime);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            R3d.UnloadModel(ref _model, true);
            R3d.UnloadSkybox(_skybox);
            R3d.UnloadMaterial(ref _groundMat);

            R3d.Close();
        }
    }
}

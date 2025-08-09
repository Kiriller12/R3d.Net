using R3d.Net.Types;
using Raylib_cs;
using System.Numerics;

namespace R3d.Net.Examples
{
    /// <summary>
    /// PBR musket example
    /// </summary>
    internal unsafe class PbrMusketExample : IExample
    {
        private Types.Model _model;
        private Matrix4x4 _modelMatrix;
        private Skybox _skybox;

        private Camera3D _camera;

        private float _modelScale = 1.0f;

        /// <inheritdoc/>
        public void Init(int width, int height)
        {
            R3d.Init(width, height, ConfigFlag.Fxaa);
            Raylib.SetTargetFPS(60);
            Raylib.DisableCursor();

            R3d.SetSSAO(true);
            R3d.SetSSAORadius(4.0f);

            R3d.SetTonemapMode(Tonemap.Aces);
            R3d.SetTonemapExposure(0.75f);
            R3d.SetTonemapWhite(1.25f);

            R3d.SetModelImportScale(0.01f);
            _model = R3d.LoadModel("Resources/PBR/musket.glb");

            var transform = Raymath.MatrixRotateY(MathF.PI / 2);
            for (var i = 0; i < _model.MaterialCount; i++)
            {
                Raylib.SetTextureFilter(_model.Materials[i].Albedo.Texture, TextureFilter.Bilinear);
                Raylib.SetTextureFilter(_model.Materials[i].Orm.Texture, TextureFilter.Bilinear);
            }

            _modelMatrix = Raymath.MatrixIdentity();

            _skybox = R3d.LoadSkybox("Resources/Sky/skybox2.png", CubemapLayout.AutoDetect);
            R3d.EnableSkybox(_skybox);

            _camera.Position = new Vector3(0, 0, 0.5f);
            _camera.Target = Vector3.Zero;
            _camera.Up = Vector3.UnitY;
            _camera.FovY = 60;

            var light = R3d.CreateLight(LightType.Directional);
            R3d.SetLightDirection(light, new Vector3(0, -1, -1));
            R3d.SetLightActive(light, true);
        }

        /// <inheritdoc/>
        public void Update(float deltaTime)
        {
            _modelScale = Raymath.Clamp(_modelScale + Raylib.GetMouseWheelMove() * 0.1f, 0.25f, 2.5f);

            if (Raylib.IsMouseButtonDown(MouseButton.Left))
            {
                var mouseDelta = Raylib.GetMouseDelta();

                float pitch = mouseDelta.Y * 0.005f / _modelScale;
                float yaw = mouseDelta.X * 0.005f / _modelScale;

                var rotate = Raymath.MatrixRotateXYZ(new Vector3(pitch, yaw, 0.0f));
                _modelMatrix = Raymath.MatrixMultiply(_modelMatrix, rotate);
            }
        }

        /// <inheritdoc/>
        public void Render()
        {
            R3d.Begin(_camera);

            var scale = Raymath.MatrixScale(_modelScale, _modelScale, _modelScale);
            var transform = Raymath.MatrixMultiply(_modelMatrix, scale);
            R3d.DrawModelPro(ref _model, transform);

            R3d.End();
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            R3d.UnloadModel(ref _model, true);
            R3d.UnloadSkybox(_skybox);

            R3d.Close();
        }
    }
}

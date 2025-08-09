using R3d.Net.Types;
using Raylib_cs;
using System.Numerics;

namespace R3d.Net.Examples
{
    /// <summary>
    /// Emission example
    /// </summary>
    internal class EmissionExample : IExample
    {
        private Types.Model _model;
        private Types.Mesh _plane;
        private Types.Material _material;
        private uint _light;

        private Camera3D _camera;

        private float rotModel = 0.0f;

        /// <inheritdoc/>
        public void Init(int width, int height)
        {
            R3d.Init(width, height, ConfigFlag.None);
            Raylib.SetTargetFPS(60);

            R3d.SetBackgroundColor(Color.Black);
            R3d.SetAmbientColor(Color.DarkGray);

            R3d.SetTonemapMode(Tonemap.Aces);
            R3d.SetTonemapExposure(0.8f);
            R3d.SetTonemapWhite(2.5f);

            R3d.SetBloomMode(Bloom.Additive);
            R3d.SetBloomSoftThreshold(0.2f);
            R3d.SetBloomIntensity(0.2f);
            R3d.SetBloomThreshold(0.6f);

            R3d.SetModelImportScale(0.01f);

            _model = R3d.LoadModel("Resources/emission.glb");

            _plane = R3d.GenMeshPlane(1000, 1000, 1, 1, true);
            _material = R3d.GetDefaultMaterial();

            _camera.Position = new Vector3(-1.0f, 1.75f, 1.75f);
            _camera.Target = new Vector3(0, 0.5f, 0);
            _camera.Up = new Vector3(0, 1, 0);
            _camera.FovY = 60;

            _light = R3d.CreateLight(LightType.Spot);
            R3d.LightLookAt(_light, new Vector3(0, 10, 5), Vector3.Zero);
            R3d.SetLightOuterCutOff(_light, 45.0f);
            R3d.SetLightInnerCutOff(_light, 22.5f);
            R3d.EnableShadow(_light, 4096);
            R3d.SetLightActive(_light, true);
        }

        /// <inheritdoc/>
        public void Update(float deltaTime)
        {
            if (Raylib.IsKeyPressed(KeyboardKey.Space))
            {
                if (R3d.IsLightActive(_light))
                {
                    R3d.SetLightActive(_light, false);
                    R3d.SetAmbientColor(Color.Black);
                }
                else
                {
                    R3d.SetLightActive(_light, true);
                    R3d.SetAmbientColor(Color.DarkGray);
                }
            }

            if (Raylib.IsMouseButtonDown(MouseButton.Left))
            {
                var mouseDelta = Raylib.GetMouseDelta();

                _camera.Position.Y = Raymath.Clamp(_camera.Position.Y + 0.01f * mouseDelta.Y, 0.25f, 2.5f);
                rotModel += mouseDelta.X;
            }
        }

        /// <inheritdoc/>
        public void Render()
        {
            R3d.Begin(_camera);

            R3d.DrawMesh(ref _plane, ref _material, Raymath.MatrixIdentity());
            R3d.DrawModelEx(ref _model, Vector3.Zero, Vector3.UnitY, rotModel, Vector3.One * 10.0f);

            R3d.End();

            Raylib.DrawText("Press SPACE to toggle the light", 16, 40, 20, Color.Lime);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            R3d.UnloadModel(ref _model, true);
            R3d.UnloadMesh(ref _plane);
            R3d.UnloadMaterial(ref _material);

            R3d.Close();
        }
    }
}

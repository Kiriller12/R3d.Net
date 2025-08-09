using R3d.Net.Types;
using Raylib_cs;
using System.Numerics;

namespace R3d.Net.Examples
{
    /// <summary>
    /// Sponza example
    /// </summary>
    internal class SponzaExample : IExample
    {
        private int _width;
        private int _height;

        private Types.Model _sponza;
        private Skybox _skybox;
        static readonly uint[] _lights = new uint[2];

        private Camera3D _camera;

        static bool sky = false;

        /// <inheritdoc/>
        public void Init(int width, int height)
        {
            _width = width;
            _height = height;

            R3d.Init(width, height, ConfigFlag.None);
            Raylib.SetTargetFPS(60);

            R3d.SetSSAO(true);
            R3d.SetSSAORadius(4.0f);
            R3d.SetBloomMode(Bloom.Mix);

            R3d.SetAmbientColor(Color.Gray);

            _sponza = R3d.LoadModel("Resources/sponza.glb");
            _skybox = R3d.LoadSkybox("Resources/Sky/skybox3.png", CubemapLayout.AutoDetect);

            // Useful if you use directional lights
            R3d.SetSceneBounds(_sponza.Aabb);

            for (var i = 0; i < 2; i++)
            {
                _lights[i] = R3d.CreateLight(LightType.Omnidirectional);

                R3d.SetLightPosition(_lights[i], new Vector3(i > 0 ? -10 : 10, 20, 0));
                R3d.SetLightActive(_lights[i], true);
                R3d.SetLightEnergy(_lights[i], 1.0f);

                R3d.SetShadowUpdateMode(_lights[i], ShadowUpdateMode.Manual);
                R3d.EnableShadow(_lights[i], 4096);
            }

            _camera.Position = new Vector3(0, 0.5f, 0);
            _camera.Target = new Vector3(0, 0, -1);
            _camera.Up = Vector3.UnitY;
            _camera.FovY = 60;

            Raylib.DisableCursor();
        }

        /// <inheritdoc/>
        public void Update(float deltaTime)
        {
            Raylib.UpdateCamera(ref _camera, CameraMode.Free);

            if (Raylib.IsKeyPressed(KeyboardKey.T))
            {
                if (sky)
                {
                    R3d.DisableSkybox();
                }
                else
                {
                    R3d.EnableSkybox(_skybox);
                }

                sky = !sky;
            }

            if (Raylib.IsKeyPressed(KeyboardKey.F))
            {
                bool fxaa = R3d.HasState(ConfigFlag.Fxaa);
                if (fxaa)
                {
                    R3d.ClearState(ConfigFlag.Fxaa);
                }
                else
                {
                    R3d.SetState(ConfigFlag.Fxaa);
                }
            }

            if (Raylib.IsKeyPressed(KeyboardKey.O))
            {
                R3d.SetSSAO(!R3d.GetSSAO());
            }

            if (Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                var tonemap = R3d.GetTonemapMode();
                R3d.SetTonemapMode((Tonemap)(((int)tonemap + (int)Tonemap.Count - 1) % (int)Tonemap.Count));
            }

            if (Raylib.IsMouseButtonPressed(MouseButton.Right))
            {
                var tonemap = R3d.GetTonemapMode();
                R3d.SetTonemapMode((Tonemap)((int)(tonemap + 1) % (int)Tonemap.Count));
            }
        }

        /// <inheritdoc/>
        public void Render()
        {
            R3d.Begin(_camera);
            R3d.DrawModel(ref _sponza, Vector3.Zero, 1.0f);
            R3d.End();

            Raylib.BeginMode3D(_camera);
            Raylib.DrawSphere(R3d.GetLightPosition(_lights[0]), 0.5f, Color.White);
            Raylib.DrawSphere(R3d.GetLightPosition(_lights[1]), 0.5f, Color.White);
            Raylib.EndMode3D();

            var tonemap = R3d.GetTonemapMode();
            var text = $"< TONEMAP {tonemap.ToString().ToUpper()} >";
            var textWidth = Raylib.MeasureText(text, 20);

            Raylib.DrawText(text, _width - textWidth - 10, 10, 20, Color.Lime);

            Raylib.DrawText("Press O to toggle SSAO", 10, 44, 20, Color.Lime);
            Raylib.DrawText("Press T to toggle the skybox", 10, 74, 20, Color.Lime);
            Raylib.DrawText("Press F to toggle the FXAA", 10, 104, 20, Color.Lime);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            R3d.UnloadModel(ref _sponza, true);
            R3d.UnloadSkybox(_skybox);

            R3d.Close();
        }
    }
}

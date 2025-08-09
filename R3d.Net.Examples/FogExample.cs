using R3d.Net.Types;
using Raylib_cs;
using System.Numerics;

namespace R3d.Net.Examples
{
    /// <summary>
    /// Fog example
    /// </summary>
    internal class FogExample : IExample
    {
        private Types.Model _sponza;

        private Camera3D _camera;

        /// <inheritdoc/>
        public void Init(int width, int height)
        {
            R3d.Init(width, height, ConfigFlag.None);
            Raylib.SetTargetFPS(60);

            _sponza = R3d.LoadModel("Resources/sponza.glb");

            R3d.SetFogMode(Fog.Exp);

            var light = R3d.CreateLight(LightType.Directional);
            R3d.SetLightDirection(light, new Vector3(0, -1, 0));
            R3d.SetLightActive(light, true);

            _camera.Position = Vector3.Zero;
            _camera.Target = new Vector3(0, 0, -1);
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

            R3d.DrawModel(ref _sponza, Vector3.Zero, 1.0f);

            R3d.End();
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            R3d.UnloadModel(ref _sponza, true);

            R3d.Close();
        }
    }
}

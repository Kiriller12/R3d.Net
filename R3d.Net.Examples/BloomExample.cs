using R3d.Net.Types;
using Raylib_cs;
using System.Numerics;

namespace R3d.Net.Examples
{
    /// <summary>
    /// Bloom example
    /// </summary>
    internal class BloomExample : IExample
    {
        private int _width;
        private int _height;

        private Types.Mesh _cube;
        private Types.Material _material;
        private Camera3D _camera;

        private float _hueCube = 0.0f;

        /// <inheritdoc/>
        public void Init(int width, int height)
        {
            _width = width;
            _height = height;

            R3d.Init(width, height, ConfigFlag.None);
            Raylib.SetTargetFPS(60);

            R3d.SetTonemapMode(Tonemap.Aces);
            R3d.SetBloomMode(Bloom.Mix);
            R3d.SetBackgroundColor(Color.Black);

            _cube = R3d.GenMeshCube(1.0f, 1.0f, 1.0f, true);
            _material = R3d.GetDefaultMaterial();

            _material.Emission.Color = Raylib.ColorFromHSV(_hueCube, 1.0f, 1.0f);
            _material.Emission.Energy = 1.0f;
            _material.Albedo.Color = Color.Black;

            _camera.Position = new Vector3(0, 3.5f, 5);
            _camera.Target = Vector3.Zero;
            _camera.Up = Vector3.UnitY;
            _camera.FovY = 60;
        }

        /// <inheritdoc/>
        public void Update(float deltaTime)
        {
            Raylib.UpdateCamera(ref _camera, CameraMode.Orbital);

            int hueDir = Raylib.IsMouseButtonDown(MouseButton.Right) - Raylib.IsMouseButtonDown(MouseButton.Left);
            if (hueDir != 0)
            {
                _hueCube = Raymath.Wrap(_hueCube + hueDir * 90.0f * deltaTime, 0, 360);
                _material.Emission.Color = Raylib.ColorFromHSV(_hueCube, 1.0f, 1.0f);
            }

            int intensityDir = (CBool)(Raylib.IsKeyPressedRepeat(KeyboardKey.Right) || Raylib.IsKeyPressed(KeyboardKey.Right)) -
                               (CBool)(Raylib.IsKeyPressedRepeat(KeyboardKey.Left) || Raylib.IsKeyPressed(KeyboardKey.Left));

            if (intensityDir != 0)
            {
                var intensivity = R3d.GetBloomIntensity();
                R3d.SetBloomIntensity(intensivity + intensityDir * 0.01f);
            }

            int radiusDir = (CBool)(Raylib.IsKeyPressedRepeat(KeyboardKey.Up) || Raylib.IsKeyPressed(KeyboardKey.Up)) -
                             (CBool)(Raylib.IsKeyPressedRepeat(KeyboardKey.Down) || Raylib.IsKeyPressed(KeyboardKey.Down));

            if (radiusDir != 0)
            {
                var radius = R3d.GetBloomFilterRadius();
                R3d.SetBloomFilterRadius(radius + radiusDir);
            }

            if (Raylib.IsKeyPressed(KeyboardKey.Space))
            {
                var mode = R3d.GetBloomMode();
                R3d.SetBloomMode((Bloom)((int)(mode + 1) % (int)(Bloom.Screen + 1)));
            }
        }

        /// <inheritdoc/>
        public void Render()
        {
            R3d.Begin(_camera);
            R3d.DrawMesh(ref _cube, ref _material, Raymath.MatrixIdentity());
            R3d.End();

            R3d.DrawBufferEmission(10, 10, 100, 100);
            R3d.DrawBufferBloom(120, 10, 100, 100);

            var mode = R3d.GetBloomMode();
            var intensivity = R3d.GetBloomIntensity();
            var radius = R3d.GetBloomFilterRadius();

            var infoStr = $"Mode: {mode}";
            var infoLen = Raylib.MeasureText(infoStr, 20);
            Raylib.DrawText(infoStr, _width - infoLen - 10, 10, 20, Color.Lime);

            infoStr = $"Intensity: {intensivity:0.00}";
            infoLen = Raylib.MeasureText(infoStr, 20);
            Raylib.DrawText(infoStr, _width - infoLen - 10, 40, 20, Color.Lime);

            infoStr = $"Filter Radius: {radius}";
            infoLen = Raylib.MeasureText(infoStr, 20);
            Raylib.DrawText(infoStr, _width - infoLen - 10, 70, 20, Color.Lime);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            R3d.UnloadMesh(ref _cube);
            R3d.UnloadMaterial(ref _material);

            R3d.Close();
        }
    }
}

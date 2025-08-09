using R3d.Net.Types;
using Raylib_cs;
using System.Numerics;

namespace R3d.Net.Examples
{
    /// <summary>
    /// Resize example
    /// </summary>
    internal class ResizeExample : IExample
    {
        private const int SphereCount = 5;

        private Types.Mesh _sphere;
        private readonly Types.Material[] _materials = new Types.Material[SphereCount];

        private Camera3D _camera;

        /// <inheritdoc/>
        public void Init(int width, int height)
        {
            R3d.Init(width, height, ConfigFlag.None);
            Raylib.SetWindowState(ConfigFlags.ResizableWindow);
            Raylib.SetTargetFPS(60);

            _sphere = R3d.GenMeshSphere(0.5f, 64, 64, true);

            for (var i = 0; i < SphereCount; i++)
            {
                _materials[i] = R3d.GetDefaultMaterial();
                _materials[i].Albedo.Color = Raylib.ColorFromHSV((float)i / SphereCount * 330, 1.0f, 1.0f);
            }

            _camera.Position = new Vector3(0, 2, 2);
            _camera.Target = Vector3.Zero;
            _camera.Up = Vector3.UnitY;
            _camera.FovY = 60;

            var light = R3d.CreateLight(LightType.Directional);
            R3d.SetLightDirection(light, new Vector3(0, 0, -1));
            R3d.SetLightActive(light, true);
        }

        /// <inheritdoc/>
        public void Update(float deltaTime)
        {
            Raylib.UpdateCamera(ref _camera, CameraMode.Orbital);

            if (Raylib.IsKeyPressed(KeyboardKey.R))
            {
                bool keep = R3d.HasState(ConfigFlag.AspectKeep);
                if (keep)
                {
                    R3d.ClearState(ConfigFlag.AspectKeep);
                }
                else
                {
                    R3d.SetState(ConfigFlag.AspectKeep);
                }
            }

            if (Raylib.IsKeyPressed(KeyboardKey.F))
            {
                bool linear = R3d.HasState(ConfigFlag.Linear);
                if (linear)
                {
                    R3d.ClearState(ConfigFlag.Linear);
                }
                else
                {
                    R3d.SetState(ConfigFlag.Linear);
                }
            }
        }

        /// <inheritdoc/>
        public void Render()
        {
            bool keep = R3d.HasState(ConfigFlag.AspectKeep);
            bool linear = R3d.HasState(ConfigFlag.Linear);

            if (keep)
            {
                Raylib.ClearBackground(Color.Black);
            }

            R3d.Begin(_camera);
            Rlgl.PushMatrix();

            for (int i = 0; i < SphereCount; i++)
            {
                R3d.DrawMesh(ref _sphere, ref _materials[i], Raymath.MatrixTranslate(i - 2, 0, 0));
            }

            Rlgl.PopMatrix();
            R3d.End();

            Raylib.DrawText($"Resize mode: {(keep ? "KEEP" : "EXPAND")}", 10, 44, 20, Color.Black);
            Raylib.DrawText($"Filter mode: {(linear ? "LINEAR" : "NEAREST")}", 10, 74, 20, Color.Black);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            R3d.UnloadMesh(ref _sphere);

            for (var i = 0; i < SphereCount; i++)
            {
                R3d.UnloadMaterial(ref _materials[i]);
            }

            R3d.Close();
        }
    }
}

using R3d.Net.Types;
using Raylib_cs;
using System.Numerics;

namespace R3d.Net.Examples
{
    /// <summary>
    /// Instanced sprites example
    /// </summary>
    internal class InstancedSpritesExample : IExample
    {
        const int InstanceCount = 512;

        private Types.Mesh _plane;
        private Types.Material _material;

        private Texture2D _texture;
        private Sprite _sprite;

        private Camera3D _camera;

        static readonly Matrix4x4[] _transforms = new Matrix4x4[InstanceCount];

        /// <inheritdoc/>
        public void Init(int width, int height)
        {
            R3d.Init(width, height, ConfigFlag.None);
            Raylib.SetTargetFPS(60);
            Raylib.DisableCursor();

            R3d.SetBackgroundColor(Color.SkyBlue);

            _plane = R3d.GenMeshPlane(1000, 1000, 1, 1, true);
            _material = R3d.GetDefaultMaterial();
            _material.Albedo.Color = Color.Green;

            _texture = Raylib.LoadTexture("Resources/tree.png");
            _sprite = R3d.LoadSprite(_texture, 1, 1);

            for (var i = 0; i < InstanceCount; i++)
            {
                var scaleFactor = Raylib.GetRandomValue(50, 100) / 10.0f;
                var scale = Raymath.MatrixScale(scaleFactor, scaleFactor, 1.0f);

                var randomX = Raylib.GetRandomValue(-500, 500);
                var randomz = Raylib.GetRandomValue(-500, 500);
                var translate = Raymath.MatrixTranslate(randomX, scaleFactor, randomz);

                _transforms[i] = Raymath.MatrixMultiply(scale, translate);
            }

            _camera.Position = new Vector3(0, 5, 0);
            _camera.Target = new Vector3(0, 5, -1);
            _camera.Up = Vector3.UnitY;
            _camera.FovY = 60;

            var light = R3d.CreateLight(LightType.Spot);
            R3d.SetLightPosition(light, new Vector3(0, 10, 10));
            R3d.SetLightActive(light, true);
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

            R3d.DrawMesh(ref _plane, ref _material, Raymath.MatrixTranslate(0, 0, 0));
            R3d.DrawSpriteInstanced(ref _sprite, _transforms, InstanceCount);

            R3d.End();
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            R3d.UnloadMesh(ref _plane);
            R3d.UnloadSprite(ref _sprite);
            Raylib.UnloadTexture(_texture);
            R3d.UnloadMaterial(ref _material);

            R3d.Close();
        }
    }
}
